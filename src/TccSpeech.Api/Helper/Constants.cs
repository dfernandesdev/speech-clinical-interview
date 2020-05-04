using System;
using System.Collections.Generic;
using System.Linq;

namespace TccSpeech.Api.Helper
{
    public class Constants
    {
        public static string[] SpecialQuestionWords = { "What", "Where", "Who", "Your" };
        public static string[] GeneralQuestionWords = { "Do", "Is", "Have", "Any", "May" };
        public static string[] ShortenedGeneralQuestionWords = { "Any" };
        public static string[] FollowUpQuestionWords = { "What about", "And" };

        public static List<string> AnyQuestionWords = new List<string>()
            .Concat(SpecialQuestionWords)
            .Concat(GeneralQuestionWords)
            .Concat(ShortenedGeneralQuestionWords)
            .Concat(FollowUpQuestionWords)
            .ToList();

        public static string[] SimpleQuestionKeywords = { "name", "date", "phone" };

        public static string[] NameKeywords = { "name", "full" };
        public static string[] DateKeywords = { "date", "birth" };
        public static string[] PhoneKeywords = { "phone", "number" };
        public static string[] AppointmentKeywords = { "reason", "appointment" };
        public static string[] AddressKeywords = { "address" };
        public static string[] AddressSpecificationKeywords = { "where", "which" };
        public static string[] CityKeywords = { "city" };
        public static string[] StateKeywords = { "state" };
        public static string[] ContactKeywords = { "contact", "emergency" };
        public static string[] RelationshipKeywords = { "relationship" };

        public static string[] MedicineQuestionKeywords = { "medicine" };
        public static string[] HeadacheQuestionKeywords = { "headache", "headaches" };
        public static string[] CancerQuestionKeywords = { "cancer" };
        public static string[] HeartQuestionKeywords = { "heart" };
        public static string[] CirculationQuestionKeywords = { "circulation" };
        public static string[] NumbnessQuestionKeywords = { "numbness", "numb" };
        public static string[] PressureQuestionKeywords = { "blood", "pressure" };
        public static string[] DiabetesQuestionKeywords = { "diabetes" };
        public static string[] SpineInjuriesQuestionKeywords = { "neck", "back" };
        public static string[] AllergyQuestionKeywords = { "allergy", "allergies" };

        public static List<string> GeneralQuestionKeywords = new List<string>()
            .Concat(MedicineQuestionKeywords)
            .Concat(HeadacheQuestionKeywords)
            .Concat(CancerQuestionKeywords)
            .Concat(HeartQuestionKeywords)
            .Concat(CirculationQuestionKeywords)
            .Concat(NumbnessQuestionKeywords)
            .Concat(PressureQuestionKeywords)
            .Concat(SpineInjuriesQuestionKeywords)
            .Concat(AllergyQuestionKeywords)
            .Concat(DiabetesQuestionKeywords)
            .ToList();

        public static string[] RelationshipTypes = { "wife", "spouse", "husband", "father", "mother", "uncle", "sister", "brother", "aunt", "grandfather", "grandmother",
        "wifes", "spouses", "husbands", "fathers", "mothers", "uncles", "sisters", "brothers", "aunts", "grandfathers", "grandmothers"};

        public static string[] AffirmativeAnswerWords = { "yes", "yeah" };
        public static string[] NegativeAnswerWords = { "no", "none" };

        public static string[] InitialWordsToIgnore = { "thank", "thanks", "OK" };
    }
}
