using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TccSpeech.Api.Model;
using TccSpeech.Api.Templates;

namespace TccSpeech.Api.Controllers
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
        public async Task<ActionResult<IEnumerable<TextAnalisys>>> AnalyzeSpeechAsync([FromBody]IEnumerable<SpeechAnalisys> speech)
        {
            // TODO - Call Google api
            return new List<TextAnalisys>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Ok</response>
        /// <returns></returns>
        [HttpPost("analyze-to-pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<byte[]>> AnalyzeSpeechToPdfFileAsync([FromBody]IEnumerable<SpeechAnalisys> speech)
        {
            // TODO - Call Google api
            var result = new List<TextAnalisys>();
            var textAnalisysToPdf = new List<TextAnalisysToPdf>(); // Map from result 

            var template = new PatientIntakeFormTemplate();
            foreach (var item in textAnalisysToPdf)
                template.Model.Add(item.Key, item.Value);

            var printTask = Task.Run(() => template.Print());

            return File(await printTask, "application/pdf", $"Patient_Intake_Form_{DateTime.Now.Ticks}.pdf");
        }
    }
}
