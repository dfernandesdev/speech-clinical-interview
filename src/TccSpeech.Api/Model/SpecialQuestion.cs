using System.Collections.Generic;

namespace TccSpeech.Api.Model
{
    public class SpecialQuestion
    {
        public ReversedToken Subject { get; set; }
        public ReversedToken Item { get; set; }
        public ReversedToken SubjectSpecification { get; set; }
        public List<ReversedToken> ResponseRestrictionModifiers { get; set; }
        public List<ReversedToken> ResponseOwnershipRestrictionAlternatives { get; set; }
        public bool ResponseOwnershipRestrictionAnyAlternative { get; set; }
    }
}
