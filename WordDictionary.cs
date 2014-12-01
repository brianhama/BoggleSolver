using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BoggleSolver
{
    public sealed class WordDictionary : SortedSet<String>
    {
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
                    dictionary.Add(reader.ReadLine().ToLower());
                }
                reader.Close();
            }

            return dictionary;
        }

        public LookupResult LookupWord(string word)
        {
            if (Contains(word))
                return LookupResult.Match;

            if (this.Where(a => a.Length >= word.Length).Select(b => b.Substring(0, word.Length)).Contains(word))
                return LookupResult.PartialMatch;

            return LookupResult.NoMatch;
        }
    }
}