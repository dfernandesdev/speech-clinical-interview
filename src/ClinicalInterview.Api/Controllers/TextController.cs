using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicalInterview.Api.Model;
using ClinicalInterview.Api.Templates;
using ClinicalInterview.Api.Helper;
using System.Linq;

namespace ClinicalInterview.Api.Controllers
{
    [ApiController]
    [Route("text")]
    public class TextController : ControllerBase
    {
        public TextController()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Ok</response>
        /// <returns></returns>
        [HttpPost("analyze")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<TextAnalisysFormField> AnalyzeSpeechAsync([FromBody]IEnumerable<SpeechAnalisys> speech)
        {
            var text = string.Join(" ",
                speech.Select(x =>
                {
                    var text = x.Sentence.Replace("St.", "Street");
                    if (!text.EndsWith(".") && !text.EndsWith("!") && !text.EndsWith("?"))
                    {
                        text += "!";
                    }
                    return text;
                }));
            var result = AnalyzeSyntaxHelper.AnalyzeSyntax(text);

            var reverseTokens = TextAnalysisUtils.ReverseRelations(result.Tokens.ToList());

            var sentences = new List<List<ReversedToken>>();
            var nextSentenceStart = 0;
            while (nextSentenceStart < text.Length)
            {
                var sentence = TextAnalysisUtils.FindNextSentence(reverseTokens, text, nextSentenceStart);
                var lastToken = sentence.Last();
                nextSentenceStart = lastToken.OriginalToken.Text.BeginOffset + 1;
                sentences.Add(sentence);
            }

            var summaries = new List<SentenceSummary>();
            foreach (var item in sentences.Select((sentence, index) => new { sentence, index }))
            {
                summaries.AddIfNotNull(TextAnalysisLogic.SummarizeFacts(item.sentence, item.index, summaries));
            }

            var extraInfo = new List<SentenceSummary>();
            foreach (var question in summaries.Where(x => x.GeneralQuestion != null || x.SpecialQuestion != null))
            {
                TextAnalysisLogic.AssignAnswer(question, summaries, extraInfo);
            }
            var analysis = new TextAnalysis()
            {
                Summaries = summaries,
                Extras = extraInfo
            };

            return TextAnalysisLogic.PlaceAnswers(analysis); // Map from result 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Ok</response>
        /// <returns></returns>
        [HttpPost("analyze-to-pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<byte[]>> AnalyzeSpeechToPdfFileAsync([FromBody]IEnumerable<TextAnalisysFormField> fields)
        {
            var template = new PatientIntakeFormTemplate();
            foreach (var item in fields)
                template.Model.Add(item.Key, item.Value);

            var printTask = Task.Run(() => template.Print());

            return File(await printTask, "application/pdf", $"Patient_Intake_Form_{DateTime.Now.Ticks}.pdf");
        }
    }
}
