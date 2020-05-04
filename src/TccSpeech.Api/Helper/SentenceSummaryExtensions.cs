using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TccSpeech.Api.Model;

namespace TccSpeech.Api.Helper
{
    public static class SentenceSummaryExtensions
    {
        public static SentenceSummary GetLastQuestion(this List<SentenceSummary> list)
        {
            if (list != null)
            {
                return list.LastOrDefault(x => x.SpecialQuestion != null || x.GeneralQuestion != null);
            }
            return null;
        }
    }
}
