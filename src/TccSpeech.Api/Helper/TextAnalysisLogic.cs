using ExtracTccSpeech.Api.HelperaoInformacao;
using Google.Cloud.Language.V1;
using InterviewProcessingApi.Dominio.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSpeech.Api.Model;

namespace TccSpeech.Api.Helper
{
    public static class TextAnalysisLogic
    {
        public static SentenceSummary SummarizeFacts(List<ReversedToken> reverseTokens, int index, List<SentenceSummary> summaries)
        {
            SentenceSummary summary = null;
            var phrase = reverseTokens.GetPhraseText();
            if (Constants.AnyQuestionWords.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(reverseTokens.First())))
            {
                if (Constants.FollowUpQuestionWords.Any(x => phrase.StartsWith(x, StringComparison.InvariantCultureIgnoreCase)))
                {
                    if (summaries.GetLastQuestion()?.GeneralQuestion != null)
                    {
                        summary = SummarizeGeneralQuestion(reverseTokens);
                    }
                    else if (summaries.GetLastQuestion()?.SpecialQuestion != null)
                    {
                        summary = SummarizeSpecialQuestion(reverseTokens);
                    }
                }
                else if (Constants.SpecialQuestionWords.Contains(reverseTokens.First().OriginalToken.Text.Content))
                {
                    summary = SummarizeSpecialQuestion(reverseTokens);
                }
                else if (Constants.GeneralQuestionWords.Contains(reverseTokens.First().OriginalToken.Text.Content))
                {
                    summary = SummarizeGeneralQuestion(reverseTokens);
                }
            }
            else
            {
                summary = SummarizeAnswer(reverseTokens);
            }

            if (summary != null)
            {
                summary.Position = index;
            }

            return summary;
        }

        public static SentenceSummary SummarizeSpecialQuestion(List<ReversedToken> reverseTokens)
        {
            var root = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Root);
            var rootRedirection = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Prep && root.Relations.Any(rel => rel == x.OriginalToken));
            var type = root.Relations.FirstOrDefault(x => x.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.Verb || x.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.Adv || x.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.Det);
            var subject = reverseTokens.FirstOrDefault(x => x.OriginalToken.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.Pron && x.Relations.Any(rel => rel == root.OriginalToken));
            if (subject == null)
            {
                subject = reverseTokens.FirstOrDefault(x => x.OriginalToken.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.Noun && root.Relations.Any(rel => rel == x.OriginalToken));
                if (subject == null)
                {
                    subject = root;
                }
            }
            else if (rootRedirection != null && subject.OriginalToken.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.Pron)
            {
                subject = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Pobj && rootRedirection.Relations.Any(rel => rel == x.OriginalToken));
            }
            var subjectSpecificationPointer = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Prep && subject.Relations.Any(rel => rel == x.OriginalToken));
            ReversedToken subjectSpecification = null;
            if (subjectSpecificationPointer != null)
            {
                subjectSpecification = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Pobj && subjectSpecificationPointer.Relations.Any(rel => rel == x.OriginalToken));
            }
            else
            {
                subjectSpecification = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Nn && subject.Relations.Any(rel => rel == x.OriginalToken));
            }
            if (subjectSpecification == null)
            {
                subjectSpecification = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Amod && subject.Relations.Any(rel => rel == x.OriginalToken));
            }

            var subjectOwner = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Poss && subject.Relations.Any(rel => rel == x.OriginalToken));
            ReversedToken subjectSpecificationOwner = null;
            if (subjectSpecification != null)
            {
                subjectSpecificationOwner = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Nn && subjectSpecification.Relations.Any(rel => rel == x.OriginalToken));
            }
            var ownerAlternatives = new List<ReversedToken>() { subjectOwner, subjectSpecificationOwner };
            var ownerCanBeEitherAlternative = false;
            if (subjectOwner != null)
            {
                ownerAlternatives.AddRange(reverseTokens.Where(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Conj && subjectOwner.Relations.Any(rel => rel == x.OriginalToken)));
                if (ownerAlternatives.Any())
                {
                    ownerCanBeEitherAlternative = reverseTokens.Any(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Cc && x.OriginalToken.Text.Content.Equals("or", StringComparison.InvariantCultureIgnoreCase) && subjectOwner.Relations.Any(rel => rel == x.OriginalToken));
                }
            }
            if (subjectSpecificationOwner != null)
            {
                ownerAlternatives.AddRange(reverseTokens.Where(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Conj && subjectSpecificationOwner.Relations.Any(rel => rel == x.OriginalToken)));
                if (ownerAlternatives.Any())
                {
                    ownerCanBeEitherAlternative = reverseTokens.Any(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Cc && x.OriginalToken.Text.Content.Equals("or", StringComparison.InvariantCultureIgnoreCase) && subjectSpecificationOwner.Relations.Any(rel => rel == x.OriginalToken));
                }
            }
            var alternativeModifierText = ownerAlternatives.Count > 1 ? (ownerCanBeEitherAlternative ? " any" : " all") : "";

            var specialQuestion = new SpecialQuestion
            {
                Subject = subject,
                SubjectSpecification = subjectSpecification,
                ResponseOwnershipRestrictionAlternatives = ownerAlternatives,
                ResponseOwnershipRestrictionAnyAlternative = ownerCanBeEitherAlternative,
            };

            return new SentenceSummary
            {
                KeyWords = new List<string>()
                    .AddIfNotNull(root)
                    .AddIfNotNull(subject)
                    .AddIfNotNull(subjectSpecification)
                    .AddIfNotNull(ownerAlternatives),
                Phrase = reverseTokens,
                Names = TextAnalysisUtils.FindNames(reverseTokens),
                Type = SentenceType.SpecialQuestion,
                SpecialQuestion = specialQuestion
            };
        }

        public static SentenceSummary SummarizeGeneralQuestion(List<ReversedToken> reverseTokens)
        {
            var root = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Root);
            var rootRedirection = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Prep && root.Relations.Any(rel => rel == x.OriginalToken));

            var subject = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Dobj || x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Rcmod);
            if (subject == null && root.OriginalToken.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.Noun)
            {
                subject = root;
            }
            else if (rootRedirection != null)
            {
                subject = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Pobj && rootRedirection.Relations.Any(rel => rel == x.OriginalToken));
            }
            var modifiers = new List<ReversedToken>();
            var clarification = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Det && subject.Relations.Any(rel => rel == x.OriginalToken));
            modifiers.Add(clarification);

            var subjectModifier = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Amod && subject.Relations.Any(rel => rel == x.OriginalToken));
            modifiers.Add(subjectModifier);

            var subjectSpecificationPointer = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Prep && subject.Relations.Any(rel => rel == x.OriginalToken));
            ReversedToken subjectSpecification = null;
            if (subjectSpecificationPointer != null)
            {
                subjectSpecification = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Pobj && subjectSpecificationPointer.Relations.Any(rel => rel == x.OriginalToken));
            }
            if (subjectSpecification == null)
            {
                subjectSpecification = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Dobj && root.Relations.Any(rel => rel == x.OriginalToken));
            }

            var condition = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Advmod && root.Relations.Any(rel => rel == x.OriginalToken));
            var conditionModifiers = new List<ReversedToken>();
            if (condition == null)
            {
                var conditionPointer = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Prep && root.Relations.Any(rel => rel == x.OriginalToken));

                if (conditionPointer != null)
                {
                    condition = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Pobj && conditionPointer.Relations.Any(rel => rel == x.OriginalToken));
                }
            }
            if (condition != null)
            {
                var conditionModifier = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Amod && condition.Relations.Any(rel => rel == x.OriginalToken));
                conditionModifiers.Add(conditionModifier);
            }
            //if(condition == subjectSpecification)
            //{
            //    condition = null;
            //}

            var subjectOwner = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Nn && subject.Relations.Any(rel => rel == x.OriginalToken));
            ReversedToken subjectSpecificationOwner = null;
            if (subjectSpecification != null)
            {
                subjectSpecificationOwner = reverseTokens.FirstOrDefault(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Nn && subjectSpecification.Relations.Any(rel => rel == x.OriginalToken));
            }
            var ownerAlternatives = new List<ReversedToken>() { subjectOwner, subjectSpecificationOwner };
            var ownerCanBeEitherAlternative = false;
            if (subjectOwner != null)
            {
                ownerAlternatives.AddRange(reverseTokens.Where(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Conj && subjectOwner.Relations.Any(rel => rel == x.OriginalToken)));
                if (ownerAlternatives.Any())
                {
                    ownerCanBeEitherAlternative = reverseTokens.Any(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Cc && x.OriginalToken.Text.Content.Equals("or", StringComparison.InvariantCultureIgnoreCase) && subjectOwner.Relations.Any(rel => rel == x.OriginalToken));
                }
            }
            if (subjectSpecificationOwner != null)
            {
                ownerAlternatives.AddRange(reverseTokens.Where(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Conj && subjectSpecificationOwner.Relations.Any(rel => rel == x.OriginalToken)));
                if (ownerAlternatives.Any())
                {
                    ownerCanBeEitherAlternative = reverseTokens.Any(x => x.OriginalToken.DependencyEdge.Label == DependencyEdge.Types.Label.Cc && x.OriginalToken.Text.Content.Equals("or", StringComparison.InvariantCultureIgnoreCase) && subjectSpecificationOwner.Relations.Any(rel => rel == x.OriginalToken));
                }
            }
            var alternativeModifierText = ownerAlternatives.Count > 1 ? (ownerCanBeEitherAlternative ? " any" : " all") : "";

            var generalQuestionDto = new GeneralQuestion
            {
                Subject = subject,
                SubjectModifiers = modifiers,
                SubjectSpecification = subjectSpecification,
                ResponseRestriction = condition,
                ResponseRestrictionModifiers = conditionModifiers,
                ResponseOwnershipRestrictionAlternatives = ownerAlternatives,
                ResponseOwnershipRestrictionAnyAlternative = ownerCanBeEitherAlternative,
            };

            return new SentenceSummary
            {
                KeyWords = new List<string>()
                    .AddIfNotNull(root)
                    .AddIfNotNull(subject)
                    .AddIfNotNull(subjectSpecification)
                    .AddIfNotNull(ownerAlternatives),
                Phrase = reverseTokens,
                Names = TextAnalysisUtils.FindNames(reverseTokens),
                Type = SentenceType.GeneralQuestion,
                GeneralQuestion = generalQuestionDto
            };
        }

        public static SentenceSummary SummarizeAnswer(List<ReversedToken> reversedTokens)
        {
            var summary = new SentenceSummary()
            {
                Phrase = reversedTokens,
                Names = TextAnalysisUtils.FindNames(reversedTokens),
                Type = SentenceType.Answer,
                KeyWords = new List<string>()
                    .AddIfNotNull(reversedTokens.Where(x => x.OriginalToken.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.X).ToList()),
                Answer = new Answer()
                {
                    HasAffirmation = reversedTokens.Any(x => Constants.AffirmativeAnswerWords.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x))),
                    Numbers = TextAnalysisUtils.FindNumbers(reversedTokens),
                }
            };

            return summary;
        }

        public static void AssignAnswer(SentenceSummary question, List<SentenceSummary> summaries, List<SentenceSummary> extras)
        {
            var questionPhrase = question.Phrase.GetPhraseText();
            if (question.KeyWords.CaseInsensitiveContains(Constants.PhoneKeywords))
            {
                AssignPhoneNumberAnswer(question, summaries);
            }
            else if (question.KeyWords.CaseInsensitiveContains(Constants.SimpleQuestionKeywords))
            {
                AssignSimpleAnswer(question, summaries);
            }
            else if (question.KeyWords.CaseInsensitiveContains(Constants.AppointmentKeywords))
            {
                AssignFreeformAnswer(question, summaries);
            }
            else if (question.KeyWords.CaseInsensitiveContains(Constants.AddressKeywords))
            {
                extras.AddIfNotNull(AssignAddressAnswer(question, summaries));
            }
            else if (question.KeyWords.CaseInsensitiveContains(Constants.CityKeywords) || questionPhrase.CaseInsensitiveStartsWith(Constants.AddressSpecificationKeywords))
            {
                extras.AddIfNotNull(AssignCityAnswer(question, summaries));
            }
            else if (question.KeyWords.CaseInsensitiveContains(Constants.StateKeywords))
            {
                AssignSimpleAnswer(question, summaries);
            }
            else if (question.KeyWords.CaseInsensitiveContains(Constants.RelationshipKeywords))
            {
                AssignRelationshipAnswer(question, summaries);
            }
            else if (question.KeyWords.CaseInsensitiveContains(Constants.ContactKeywords))
            {
                extras.AddIfNotNull(AssignEmergencyContactInfoAnswer(question, summaries));
            }
            else if (question.KeyWords.CaseInsensitiveContains(Constants.GeneralQuestionKeywords))
            {
                extras.AddIfNotNull(AssignGeneralQuestionAnswer(question, summaries));
            }
        }

        public static void AssignSimpleAnswer(SentenceSummary question, List<SentenceSummary> summaries)
        {
            var answer = summaries[question.Position + 1];

            question.CombinedAnswerText = answer.Phrase.GetPhraseText().Substring(0, answer.Phrase.GetPhraseText().Length-1);
        }

        public static void AssignRelationshipAnswer(SentenceSummary question, List<SentenceSummary> summaries)
        {
            var answer = summaries[question.Position + 1];

            var fullPhrase = answer.Phrase.GetPhraseText();
            foreach (var relationship in Constants.RelationshipTypes)
            {
                if (fullPhrase.CaseInsensitiveContains(relationship))
                {
                    question.CombinedAnswerText = relationship;
                }
            }
            if (string.IsNullOrEmpty(question.CombinedAnswerText))
            {
                question.CombinedAnswerText = answer.Phrase.GetPhraseText();
            }
        }

        public static void AssignPhoneNumberAnswer(SentenceSummary question, List<SentenceSummary> summaries)
        {
            var answer = summaries[question.Position + 1];
            var number = answer.Phrase.GetPhraseText().Replace("-", "").Replace("dash", "").Replace(".", "");

            if (int.TryParse(number, out int parsedNumber))
            {
                try
                {
                    number = parsedNumber.ToString(@"000\-000\-0000\");
                }
                catch (Exception)
                {

                }
            }

            question.CombinedAnswerText = number;
        }

        public static void AssignFreeformAnswer(SentenceSummary question, List<SentenceSummary> summaries)
        {
            var answer = summaries
                .Skip(question.Position + 1)
                .TakeWhile(x => !Constants.InitialWordsToIgnore.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x.Phrase.First()))
                    && x.Answer != null);

            var fullAnswer = new StringBuilder();
            foreach (var summary in answer)
            {
                fullAnswer.Append(summary.Phrase.GetPhraseText());
            }

            question.CombinedAnswerText = fullAnswer.ToString();
        }

        public static List<SentenceSummary> AssignAddressAnswer(SentenceSummary question, List<SentenceSummary> summaries)
        {
            var answer = summaries
                .Skip(question.Position + 1)
                .TakeWhile(x => !Constants.InitialWordsToIgnore.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x.Phrase.First()))
                    && x.Answer != null);
            var extras = new List<SentenceSummary>();

            var fullAnswerText = answer.First().Phrase.GetPhraseText();
            var lastCharPosition = fullAnswerText.Length - 1;
            var firstComma = fullAnswerText.IndexOf(',');
            var secondComma = firstComma == -1 ? -1 : fullAnswerText.IndexOf(',', firstComma);

            var address = fullAnswerText.Substring(0, firstComma == -1 ? lastCharPosition : firstComma);
            var city = null as string;
            var state = null as string;
            if (firstComma != -1)
            {
                city = fullAnswerText.Substring(firstComma + 1, secondComma == -1 ? lastCharPosition - firstComma : secondComma);
            }
            if (secondComma != -1)
            {
                state = fullAnswerText.Substring(secondComma + 1, lastCharPosition - secondComma);
            }

            if (city != null)
            {
                extras.Add(new SentenceSummary() { KeyWords = new List<string>() { "city" }, CombinedAnswerText = city, Position = question.Position });
            }
            if (state != null)
            {
                extras.Add(new SentenceSummary() { KeyWords = new List<string>() { "state" }, CombinedAnswerText = state, Position = question.Position });
            }

            question.CombinedAnswerText = address;

            return extras;
        }

        public static List<SentenceSummary> AssignCityAnswer(SentenceSummary question, List<SentenceSummary> summaries)
        {
            var answer = summaries
                .Skip(question.Position + 1)
                .TakeWhile(x => !Constants.InitialWordsToIgnore.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x.Phrase.First()))
                    && x.Answer != null);
            var extras = new List<SentenceSummary>();

            var fullAnswerText = answer.First().Phrase.GetPhraseText();
            var lastCharPosition = fullAnswerText.Length - 1;
            var firstComma = fullAnswerText.IndexOf(',');

            var cityPart = answer.First().Phrase.TakeWhile(x => !TextAnalysisUtils.GetTokenText(x).CaseInsensitiveEquals(","));
            var city = TextAnalysisUtils.FindNames(cityPart.ToList()).First();
            var state = null as string;
            if (firstComma != -1)
            {
                state = fullAnswerText.Substring(firstComma + 1, lastCharPosition - firstComma - 1);
            }

            if (state != null)
            {
                extras.Add(new SentenceSummary() { KeyWords = new List<string>() { "state" }, CombinedAnswerText = state, Position = question.Position });
            }

            question.CombinedAnswerText = city;
            if (!question.KeyWords.CaseInsensitiveContains("city"))
            {
                question.KeyWords.Add("city");
            }

            return extras;
        }

        public static List<SentenceSummary> AssignEmergencyContactInfoAnswer(SentenceSummary question, List<SentenceSummary> summaries)
        {
            var name = summaries[question.Position + 1].Names.FirstOrDefault();
            var phrases = summaries
                .Skip(question.Position + 1)
                .TakeWhile(x => !Constants.InitialWordsToIgnore.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x.Phrase.First()))
                    && x.Answer != null);

            var extras = new List<SentenceSummary>();

            var relationshipPhrase = phrases.FirstOrDefault(x => Constants.RelationshipTypes.CaseInsensitiveContains(x.Phrase.Select(x => x.OriginalToken.Text.Content)));
            if (relationshipPhrase != null)
            {
                var fullPhrase = relationshipPhrase.Phrase.GetPhraseText();
                foreach (var relationship in Constants.RelationshipTypes)
                {
                    if (fullPhrase.CaseInsensitiveContains(relationship))
                    {
                        extras.Add(new SentenceSummary() { KeyWords = new List<string>() { "relationship" }, CombinedAnswerText = relationship, Position = question.Position });
                    }
                }
            }

            question.CombinedAnswerText = name ?? "Not disclosed";
            return extras;
        }

        public static List<SentenceSummary> AssignGeneralQuestionAnswer(SentenceSummary question, List<SentenceSummary> summaries)
        {
            var answer = summaries[question.Position + 1];
            var answerIsYes = answer.Phrase.Any(x => Constants.AffirmativeAnswerWords.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x)));
            var answerIsNo = answer.Phrase.Any(x => Constants.NegativeAnswerWords.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x)));
            var extras = new List<SentenceSummary>();

            if (answerIsYes || (!answerIsYes && !answerIsNo))
            {
                var phrases = summaries
                .Skip(question.Position + 1)
                .TakeWhile(x => !Constants.InitialWordsToIgnore.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x.Phrase.First()))
                    && x.Answer != null);

                var fullAnswer = new StringBuilder();
                foreach (var summary in phrases)
                {
                    fullAnswer.Append(summary.Phrase.SkipWhile(x =>
                                                    Constants.AffirmativeAnswerWords.CaseInsensitiveContains(TextAnalysisUtils.GetTokenText(x))
                                                    || TextAnalysisUtils.GetTokenText(x).CaseInsensitiveEquals(" ")
                                                    || TextAnalysisUtils.GetTokenText(x).CaseInsensitiveEquals(",")
                                                    || TextAnalysisUtils.GetTokenText(x).CaseInsensitiveEquals("."))
                        .GetPhraseText()
                    );
                }
                fullAnswer.Replace("was n't", "wasn't").Replace("do n't", "don't");

                var keywords = question.KeyWords;
                if (!answerIsYes && !answerIsNo)
                {
                    keywords.Add("indecision");
                }

                if (fullAnswer.Length > 0 && (answerIsYes || !keywords.CaseInsensitiveContains("indecision")))
                {
                    extras.Add(new SentenceSummary() { KeyWords = keywords, CombinedAnswerText = fullAnswer.ToString(), Position = question.Position });
                }
            }

            question.CombinedAnswerText = answer.Phrase.GetPhraseText();
            return extras;
        }

        public static List<TextAnalisysFormField> PlaceAnswers(TextAnalysis analysis)
        {
            var conversion = new List<TextAnalisysFormField>();

            foreach (var question in analysis.Summaries.Where(x => x.Answer == null))
            {
                if (Constants.NameKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "bio-fullname", Value = question.CombinedAnswerText });
                }
                else if (Constants.AddressKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "bio-address", Value = question.CombinedAnswerText });
                }
                else if (Constants.CityKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "bio-city", Value = question.CombinedAnswerText });
                }
                else if (Constants.StateKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "bio-state", Value = question.CombinedAnswerText });
                }
                else if (Constants.PhoneKeywords.CaseInsensitiveContains(question.KeyWords) && (Constants.RelationshipTypes.CaseInsensitiveContains(question.KeyWords) || question.Names.Any()))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "emergency-phone", Value = question.CombinedAnswerText });
                }
                else if (Constants.PhoneKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "bio-phone", Value = question.CombinedAnswerText });
                }
                else if (Constants.DateKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "bio-dob", Value = question.CombinedAnswerText });
                }
                else if (Constants.ContactKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "emergency-name", Value = question.CombinedAnswerText });
                }
                else if (Constants.RelationshipKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "emergency-relationship", Value = question.CombinedAnswerText });
                }
                else if (Constants.MedicineQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "medical-medications", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.HeadacheQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "health-headaches", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.CancerQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "health-cancer", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.HeartQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "health-heart", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.NumbnessQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "health-numbness", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.DiabetesQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "health-diabetes", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.PressureQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "health-pressure", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.AllergyQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "health-allergy", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.SpineInjuriesQuestionKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "health-neckback", Value = GetGeneralQuestionAnswer(question.CombinedAnswerText) });
                }
                else if (Constants.AppointmentKeywords.CaseInsensitiveContains(question.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "reason-appointment", Value = question.CombinedAnswerText });
                }
            }

            var medicalDetails = new TextAnalisysFormField() { Key = "medical-details" };
            var medicalDetailsText = new StringBuilder();
            foreach (var extra in analysis.Extras)
            {
                if (Constants.CityKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "bio-city", Value = extra.CombinedAnswerText });
                }
                else if (Constants.StateKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "bio-state", Value = extra.CombinedAnswerText });
                }
                else if (Constants.RelationshipKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    conversion.Add(new TextAnalisysFormField() { Key = "emergency-relationship", Value = extra.CombinedAnswerText });
                }
                else if (Constants.MedicineQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Medicine: " + extra.CombinedAnswerText);
                }
                else if (Constants.HeadacheQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Headache: " + extra.CombinedAnswerText);
                }
                else if (Constants.CancerQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Cancer: " + extra.CombinedAnswerText);
                }
                else if (Constants.HeartQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Heart/Circulation: " + extra.CombinedAnswerText);
                }
                else if (Constants.NumbnessQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Numbness: " + extra.CombinedAnswerText);
                }
                else if (Constants.DiabetesQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Diabetes: " + extra.CombinedAnswerText);
                }
                else if (Constants.PressureQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Blood Pressure: " + extra.CombinedAnswerText);
                }
                else if (Constants.AllergyQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Allergies: " + extra.CombinedAnswerText);
                }
                else if (Constants.SpineInjuriesQuestionKeywords.CaseInsensitiveContains(extra.KeyWords))
                {
                    medicalDetailsText.AppendWithSpace("Neck/Back: " + extra.CombinedAnswerText);
                }
            }
            medicalDetails.Value = medicalDetailsText.ToString();
            conversion.Add(medicalDetails);

            return conversion;
        }

        public static string GetGeneralQuestionAnswer(string answerText)
        {
            if (answerText.CaseInsensitiveContains(Constants.AffirmativeAnswerWords))
            {
                return "true";
            }
            else if (answerText.CaseInsensitiveContains(Constants.NegativeAnswerWords))
            {
                return "false";
            }
            else
            {
                return "indecision";
            }
        }
    }
}
