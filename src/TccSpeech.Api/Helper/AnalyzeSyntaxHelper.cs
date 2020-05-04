using Google.Apis.Auth.OAuth2;
using Grpc.Core;
using Google.Cloud.Language.V1;
using Grpc.Auth;

namespace TccSpeech.Api.Helper
{
    public static class AnalyzeSyntaxHelper
    {
        private const string JSON_PATH = "./My First Project-a79bccc393cc.json";
        public static AnalyzeSyntaxResponse AnalyzeSyntax(string texto)
        {
            GoogleCredential credential = GoogleCredential.FromFile(JSON_PATH);
            ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            var builder = new LanguageServiceClientBuilder();
            builder.ChannelCredentials = channelCredentials;
            var client = builder.Build();


            var syntax = client.AnalyzeSyntax(new Document()
            {
                Content = texto,
                Type = Document.Types.Type.PlainText,
                Language = "en-US"
            });

            return syntax;
        }
    }
}
