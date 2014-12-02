using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace BoggleSolver
{
    public sealed class WordDictionary
    {
        private readonly Dictionary<String, SortedSet<String>> _words = new Dictionary<string, SortedSet<string>>();

        private WordDictionary()
        {
        }

        public enum LookupResult
        {
            Match,
            PartialMatch,
            NoMatch
        }

        public static WordDictionary LoadWordDictionaryFromFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("Dictionary file could not be found.", filename);

            WordDictionary dictionary = new WordDictionary();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string word = reader.ReadLine();
                    if (word.Length > 2)
                        dictionary.AddWordToDictionary(word.ToLower());
                }
                reader.Close();
            }

            return dictionary;
        }

        private void AddWordToDictionary(string word)
        {
            string prefix = word.Substring(0, 2);
            if (_words.ContainsKey(prefix))
                _words[prefix].Add(word);
            else
            {
                SortedSet<String> newBucket = new SortedSet<string>();
                newBucket.Add(word);
                _words.Add(prefix, newBucket);
            }
        }

        public LookupResult LookupWord(string word)
        {
            if (word.Length <= 2)
                return LookupResult.PartialMatch;

            string prefix = word.Substring(0, 2);

            if (!_words.ContainsKey(prefix))
                return LookupResult.NoMatch;

            SortedSet<String> bucket = _words[prefix];

            if (!bucket.Any(a => a.StartsWith(word)))
                return LookupResult.NoMatch;

            if (bucket.Contains(word))
                return LookupResult.Match;

            return LookupResult.PartialMatch;
        }
    }
}