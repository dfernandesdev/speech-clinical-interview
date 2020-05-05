using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicalInterview.Api.Helper
{
    public static class StringExtensions
    {
        public static int GetDeterministicHashCode(this string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = (hash1 << 5) + hash1 ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = (hash2 << 5) + hash2 ^ str[i + 1];
                }

                return hash1 + hash2 * 1566083941;
            }
        }

        public static StringBuilder AppendWithSpace(this StringBuilder builder, string text)
        {
            if (builder.Length > 0)
            {
                builder.Append(" ");
            }
            builder.Append(text);
            return builder;
        }

        public static bool CaseInsensitiveEquals(this string word, string otherWord)
        {
            return word.Equals(otherWord, System.StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool CaseInsensitiveContains(this string word, string otherWord)
        {
            return word.Contains(otherWord, System.StringComparison.InvariantCultureIgnoreCase);
        }
        public static bool CaseInsensitiveContains(this string word, IEnumerable<string> words)
        {
            return words.Any(x => word.Contains(x, System.StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool CaseInsensitiveStartsWith(this string word, IEnumerable<string> words)
        {
            return words.Any(x => word.StartsWith(x, System.StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool CaseInsensitiveContains(this IEnumerable<string> collection, string word)
        {
            return collection.Any(element => element.CaseInsensitiveEquals(word));
        }

        public static bool CaseInsensitiveContains(this IEnumerable<string> collection, IEnumerable<string> words)
        {
            return collection.Any(element => words.CaseInsensitiveContains(element));
        }
    }
}
