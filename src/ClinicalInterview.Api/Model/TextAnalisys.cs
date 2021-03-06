﻿using System.Collections.Generic;

namespace ClinicalInterview.Api.Model
{
    public class TextAnalysis
    {
        public IEnumerable<SentenceSummary> Summaries { get; set; }
        public IEnumerable<SentenceSummary> Extras { get; set; }
    }
}