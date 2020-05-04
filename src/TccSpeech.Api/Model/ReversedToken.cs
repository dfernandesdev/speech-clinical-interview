using Google.Cloud.Language.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace TccSpeech.Api.Model
{
    public class ReversedToken
    {
        public Token OriginalToken { get; set; }
        public List<Token> Relations { get; set; }
    }
}
