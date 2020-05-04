using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicalInterview.Api.Model
{
    public class Answer
    {
        public bool HasAffirmation { get; set; }
        public List<string> Numbers { get; set; }
    }
}
