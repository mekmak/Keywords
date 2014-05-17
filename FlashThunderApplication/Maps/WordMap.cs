using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FlashThunderApplication
{
    class WordMap : IMap
    {
        private Dictionary<string, HashSet<string>> dictionary;

        public WordMap()
        {
            dictionary = new Dictionary<string, HashSet<string>>();
        }

        public void Add(string word, string fileName)
        {
            if (!dictionary.ContainsKey(word))
            {
                dictionary.Add(word, new HashSet<string>());
            }

            dictionary[word].Add(fileName);
        }

        public HashSet<string> Query(string query)
        {
            var allSets = new HashSet<HashSet<string>>();
            HashSet<string> words = getIndividualWords(query);

            if (words.Count == 0)
            {
                return null;
            }

            foreach (string word in words)
            {
                allSets.Add(queryWord(word));
            }

            return FlashThunder.IntersectAllSets(allSets);
        }

        private HashSet<string> queryWord(string query)
        {
            if (dictionary.ContainsKey(query))
            {
                return dictionary[query];
            }
            else
            {
                return new HashSet<string>();
            }
        }

        private HashSet<string> getIndividualWords(string query)
        {
            string[] tokens = Regex.Split(query, " ");
            var keys = new HashSet<string>();
            bool lookingAtPhrase = false;

            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];
                // Checking to see if it's the start of a phrase
                if (token.ElementAt(0).Equals('"'))
                {
                    lookingAtPhrase = true;
                    continue;
                }

                // If we're looking at a phrase, check to see if it's the end
                if (lookingAtPhrase && token.Last<char>().Equals('"'))
                {
                    lookingAtPhrase = false;
                    continue;
                }

                // If we're looking at a phrase and it's not the end, keep going
                if (lookingAtPhrase)
                {
                    continue;
                }

                keys.Add(token);
            }

            return keys;
        }
    }
}
