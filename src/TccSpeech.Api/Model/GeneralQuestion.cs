using System.Collections.Generic;

namespace TccSpeech.Api.Model
{
    public class GeneralQuestion
    {
        public ReversedToken Subject { get; set; }
        public List<ReversedToken> SubjectModifiers { get; set; }
        public ReversedToken SubjectSpecification { get; set; }
        public ReversedToken ResponseRestriction { get; set; }
        public List<ReversedToken> ResponseRestrictionModifiers { get; set; }
        public List<ReversedToken> ResponseOwnershipRestrictionAlternatives { get; set; }
        public bool ResponseOwnershipRestrictionAnyAlternative { get; set; }
    }
}
