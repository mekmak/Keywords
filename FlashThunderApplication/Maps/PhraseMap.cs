using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace FlashThunderApplication
{
    class PhraseMap : IMap
    {
        private Dictionary<string, ArrayList> phraseMap;

        public PhraseMap()
        {
            phraseMap = new Dictionary<string, ArrayList>();
        }

        public void Add(string word, string fileName)
        {
            if (!phraseMap.ContainsKey(fileName))
            {
                phraseMap.Add(fileName, new ArrayList());
            }

            phraseMap[fileName].Add(word);
        }

        public HashSet<string> Query(string query)
        {
            var allSets = new HashSet<HashSet<string>>();            
            HashSet<string> phrases = getPhrases(query);

            if (phrases.Count == 0)
            {
                return null;
            }

            foreach (string phrase in phrases)
            {
                allSets.Add(queryPhrase(phrase));
            }

            return FlashThunder.IntersectAllSets(allSets);
        }

        private HashSet<string> queryPhrase(string query)
        {
            var lectures = new HashSet<string>();

            var allLectures = phraseMap.Keys;
            ArrayList currentWords;

            foreach (string lecture in allLectures)
            {
                currentWords = phraseMap[lecture];
                if (containsPhrase(query, currentWords))
                {
                    lectures.Add(lecture);
                }
            }

            return lectures;
        }

        private bool containsPhrase(string phrase, ArrayList wordListAL)
        {
            string[] tokens = phrase.Split(' ');
            string[] wordList = (string[])wordListAL.ToArray(Type.GetType("System.String"));

            for (int i = 0; i < wordList.Length; i++)
            {
                string currentWord = wordList[i];
                if (tokens[0].Equals(currentWord))
                {
                    bool phraseFound = true;
                    for (int j = 1; j < tokens.Length; j++)
                    {
                        if ((i + j) == wordList.Length)
                        {
                            phraseFound = false;
                            break;
                        }

                        if (!tokens[j].Equals(wordList[i + j]))
                        {
                            phraseFound = false;
                            break;
                        }
                    }

                    if (phraseFound)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private HashSet<string> getPhrases(string query)
        {
            string[] tokens = Regex.Split(query, " ");
            var keys = new HashSet<string>();
            int length = tokens.Length;

            for (int i = 0; i < length; i++)
            {
                string token = tokens[i];
                // Checking to see if it's the start of a phrase
                if (token.ElementAt(0).Equals('"'))
                {
                    string aggregate = token.Substring(1) + " ";

                    i++;
                    if (i == length)
                    {
                        return new HashSet<string>();
                    }

                    token = tokens[i];

                    while (!token.Last().Equals('"'))
                    {
                        aggregate += token + " ";
                        i++;

                        if (i == length)
                        {
                            return new HashSet<string>();
                        }

                        token = tokens[i];
                    }

                    aggregate += token.Substring(0, token.Length - 1);
                    keys.Add(aggregate);
                }
            }

            return keys;
        }
    }
}
