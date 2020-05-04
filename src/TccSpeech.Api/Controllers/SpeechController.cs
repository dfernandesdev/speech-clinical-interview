using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TccSpeech.Api.Helper;
using TccSpeech.Api.Model;

namespace TccSpeech.Api.Controllers
{
    [ApiController]
    [Route("speech")]
    public class SpeechController : ControllerBase
    {
        private const string SUBSCRIPTION_KEY = "873bc5f662354ab6b1234323a4b55909";
        private const string SUBSCRIPTION_REGION = "eastus";

        public SpeechController()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <param name="file">Arquivo SSML</param>
        /// <returns></returns>
        [HttpPost("synthesize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<byte[]>> SynthesizeSsmlToAudioFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Um arquivo SSML é necessário!");
            if (file.ContentType != "text/xml")
                return BadRequest("Permitido somente formato xml!");

            var config = SpeechConfig.FromSubscription(SUBSCRIPTION_KEY, SUBSCRIPTION_REGION);
            config.SetProperty("SpeechServiceResponse_Synthesis_WordBoundaryEnabled", "false");

            string text = null;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                text = await reader.ReadToEndAsync();
            }

            byte[] audioData = null;
            using (var synthesizer = new SpeechSynthesizer(config, null))
            {
                using (var result = await synthesizer.SpeakSsmlAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        audioData = result.AudioData;
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        if (cancellation.Reason == CancellationReason.Error)
                            return BadRequest(cancellation.ErrorDetails);
                    }
                }
            }

            return File(audioData, "audio/wav", file.FileName.Replace("xml", "wav"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <param name="file">Arquivo WAV</param>
        /// <returns></returns>
        [HttpPost("recognize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SpeechAnalisys>>> RecognizeSpeechAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Um arquivo de audio é necessário!");
            if (file.ContentType != "audio/wav")
                return BadRequest("Permitido somente formato wav!");

            var config = SpeechConfig.FromSubscription(SUBSCRIPTION_KEY, SUBSCRIPTION_REGION);
            var result = new List<SpeechAnalisys>();
            var stopRecognition = new TaskCompletionSource<int>();

            using (var audioInput = AudioConfig.FromStreamInput(new PullAudioInputStream(new BinaryAudioStreamReader(
                                                                new BinaryReader(file.OpenReadStream())))))
            {
                using (var recognizer = new SpeechRecognizer(config, audioInput))
                {
                    recognizer.Recognized += (s, e) =>
                    {
                        if (e.Result.Reason == ResultReason.RecognizedSpeech)
                            result.Add(new SpeechAnalisys { Sentence = e.Result.Text });
                    };

                    recognizer.Canceled += (s, e) => stopRecognition.TrySetResult(0);

                    recognizer.SessionStopped += (s, e) => stopRecognition.TrySetResult(0);

                    await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                    Task.WaitAny(new[] { stopRecognition.Task });

                    await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                }
            }

            return Ok(result);
        }
    }
}
