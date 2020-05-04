using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClinicalInterview.Api.Model;

namespace ClinicalInterview.Api.Helper
{
    public static class ListExtensions
    {
        public static List<T> AddIfNotNull<T>(this List<T> list, T element)
        {
            if (element != null)
            {
                list.Add(element);
            }
            return list;
        }

        public static List<T> AddIfNotNull<T>(this List<T> list, List<T> elements)
        {
            if (elements != null)
            {
                list.AddRange(elements.Where(x => x != null));
            }
            return list;
        }

        public static List<string> AddIfNotNull(this List<string> list, ReversedToken element)
        {
            if (element != null)
            {
                list.Add(element.OriginalToken.Text.Content);
            }
            return list;
        }

        public static List<string> AddIfNotNull(this List<string> list, List<ReversedToken> elements)
        {
            if (elements != null)
            {
                list.AddRange(elements.Where(x => x != null).Select(x => x.OriginalToken.Text.Content));
            }
            return list;
        }

        public static string GetPhraseText(this IEnumerable<ReversedToken> list)
        {
            if (list != null)
            {
                var builder = new StringBuilder();
                ReversedToken lastToken = null;
                foreach (var token in list)
                {
                    var firstChar = token.OriginalToken.Text.Content[0];
                    switch (firstChar)
                    {
                        case '-':
                        case '!':
                        case '?':
                        case '\'':
                        case '.':
                        case ',':
                            builder.Append(token.OriginalToken.Text.Content);
                            break;
                        default:
                            if (builder.Length > 0 && lastToken != null && lastToken.OriginalToken.Text.Content != "-")
                            {
                                builder.Append(" ");
                            }
                            builder.Append(token.OriginalToken.Text.Content);
                            break;
                    }
                    lastToken = token;
                }
                return builder.ToString();
            }
            return null;
        }
    }
}
