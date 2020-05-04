using System;
using System.Collections.Generic;
using System.Text;
using TccSpeech.Api.Helper;

namespace TccSpeech.Api.Model
{
    public class SentenceSummary
    {
        public List<string> KeyWords { get; set; }
        public int Position { get; set; }
        public SentenceType Type { get; set; }
        public List<ReversedToken> Phrase { get; set; }
        public List<string> Names { get; set; }
        public GeneralQuestion GeneralQuestion { get; set; }
        public SpecialQuestion SpecialQuestion { get; set; }
        public Answer Answer { get; set; }
        public string CombinedAnswerText { get; set; }
    }
}
