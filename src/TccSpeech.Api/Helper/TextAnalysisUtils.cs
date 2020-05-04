using Google.Cloud.Language.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TccSpeech.Api.Model;

namespace TccSpeech.Api.Helper
{
    public static class TextAnalysisUtils
    {
        public static string GetTokenText(Token token)
        {
            return token?.Text?.Content ?? "Unknown";
        }

        public static string GetTokenText(ReversedToken reversedToken)
        {
            return reversedToken?.OriginalToken?.Text?.Content ?? "Unknown";
        }

        public static List<string> FindNames(List<ReversedToken> reversedTokens)
        {
            var names = new List<string>();
            var currentName = new StringBuilder();
            foreach (var token in reversedTokens)
            {
                if (token.OriginalToken.PartOfSpeech.Proper == PartOfSpeech.Types.Proper.Proper)
                {
                    currentName.Append(" ");
                    currentName.Append(token.OriginalToken.Text.Content);
                }
                else
                {
                    if (currentName.Length > 0)
                    {
                        names.Add(currentName.ToString());
                    }
                    currentName = new StringBuilder();
                }
            }
            if (currentName.Length > 0)
            {
                names.Add(currentName.ToString());
            }

            return names;
        }

        public static List<string> FindNumbers(List<ReversedToken> reversedTokens)
        {
            var numbers = new List<string>();
            var currentNumber = new StringBuilder();
            foreach (var token in reversedTokens)
            {
                if (token.OriginalToken.PartOfSpeech.Tag == PartOfSpeech.Types.Tag.Num || token.OriginalToken.Text.Content == "-")
                {
                    currentNumber.Append(token.OriginalToken.Text.Content);
                }
                else
                {
                    if (currentNumber.Length > 0)
                    {
                        numbers.Add(currentNumber.ToString());
                    }
                    currentNumber = new StringBuilder();
                }
            }
            if (currentNumber.Length > 0)
            {
                numbers.Add(currentNumber.ToString());
            }

            return numbers;
        }

        public static string GetModifiersText(List<ReversedToken> tokens)
        {
            var builder = new StringBuilder();

            foreach(var token in tokens.Where(x => x != null))
            {
                builder.Append($"({token.OriginalToken.Text.Content})");
                if(builder.Length > 0)
                {
                    builder.Append(" ");
                }
            }

            return builder.ToString();
        }

        public static string GetAlternativesText(List<ReversedToken> tokens)
        {
            if (!tokens.Any(x => x != null))
            {
                return "Unknown";
            }
            var builder = new List<string>();
            
            foreach (var token in tokens.Where(x => x != null))
            {
                builder.Add($"{token.OriginalToken.Text.Content}");
            }

            return string.Join(", ", builder.ToArray());
        }

        public static List<ReversedToken> ReverseRelations(List<Token> tokens)
        {
            return tokens.Select(token => new ReversedToken() { OriginalToken = token, Relations = tokens.Where(x => tokens[x.DependencyEdge.HeadTokenIndex] == token).ToList() }).ToList();
        }

        public static List<ReversedToken> FindNextSentence(List<ReversedToken> reverseTokens, string originalText, int startingPoint)
        {
            var nextSentences = originalText.Substring(startingPoint);
            var nextSentence = Regex.Match(nextSentences, @"[.?!] *[A-Z0-9]");
            var nextSentenceEnd = originalText.Length;
            if (nextSentence.Success) 
            {
                nextSentenceEnd = nextSentence.Index + 1 + originalText.Length - nextSentences.Length;
                if(originalText[nextSentenceEnd] == ' ')
                {
                    nextSentenceEnd++;
                }
            }
            return reverseTokens
                    .SkipWhile(x => x.OriginalToken.Text.BeginOffset < startingPoint)
                        .Where(x => x.OriginalToken.Text.BeginOffset < nextSentenceEnd).ToList();
        }
    }
}
