using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Keywords
{
    class Program
    {
        const String FILE_NAME = @"C:\Users\Tomek\Documents\70-160\keywords.txt";
        const String SOURCE_DIRECTORY = @"C:\Users\Tomek\Documents\70-160\Lectures";

        static void Main(string[] args)
        {
            go();   
        }

        static void go()
        {
            var dictionary = new Dictionary<String, HashSet<String>>();
            var wordLists = new Dictionary<String, ArrayList>();

            if (!readSlides(dictionary, wordLists))
            {
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Done.");

            var keys = dictionary.Keys;
            String command = Console.ReadLine();

            while (!command.Equals("quit"))
            {
                Console.WriteLine();

                var common = new Dictionary<String, int>();// HashSet<String>();
                var seen = new HashSet<String>();

                var tokens = parseCommand(command.Trim());

                if (tokens.Count <= 1)
                {
                    if (isOneWord(command))
                    {
                        if (keys.Contains(command))
                        {
                            printList(dictionary[command]);
                        }
                        else
                        {
                            Console.WriteLine("Not found.");
                        }
                    }
                    else
                    {
                        foreach (string token in tokens)
                        {
                            var lectures = findPhrase(token, wordLists);
                            if (lectures.Count > 0)
                            {
                                printList(lectures);
                            }
                            else
                            {
                                Console.WriteLine("Not found.");
                            }
                        }   
                    }
                }
                else
                {
                    foreach (string token in tokens)
                    {
                        if (isOneWord(token))
                        {
                            if (keys.Contains(token))
                            {
                                var currentLectures = dictionary[token];
                                foreach (string lecture in currentLectures)
                                {
                                    if (seen.Contains(lecture))
                                    {
                                        if (common.Keys.Contains(lecture))
                                        {
                                            int count = common[lecture];
                                            common[lecture] = count+1;
                                        }
                                        else
                                        {
                                            common.Add(lecture, 2);
                                        }
                                    }
                                    else
                                    {
                                        seen.Add(lecture);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var lectures = findPhrase(token, wordLists);

                            foreach (string lecture in lectures)
                            {
                                if (seen.Contains(lecture))
                                {
                                    if (common.Keys.Contains(lecture))
                                    {
                                        int count = common[lecture];
                                        common[lecture] = count + 1;
                                    }
                                    else
                                    {
                                        common.Add(lecture, 2);
                                    }
                                }
                                else
                                {
                                    seen.Add(lecture);
                                }
                            }
                        }
                    }

                    if (common.Count > 0)
                    {
                        var commonAmongAll = new HashSet<String>();
                        foreach (string lecture in common.Keys)
                        {
                            if (common[lecture] == tokens.Count)
                            {
                                commonAmongAll.Add(lecture);
                            }
                        }

                        if (commonAmongAll.Count > 0)
                        {
                            printList(commonAmongAll);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not found.");
                    }
                }

                Console.WriteLine();
                command = Console.ReadLine();
            }
        }
        
        static HashSet<String> parseCommand(String command)
        {
            String[] tokens = Regex.Split(command, " ");
            var keys = new HashSet<String>();

            if (tokens.Length <= 1)
            {
                keys.Add(tokens[0]);
            }
            else
            {
                for (int i = 0; i < tokens.Length; i++)
                {
                    String token = tokens[i];
                    if (token.ElementAt(0).Equals('"'))
                    {
                        String aggregate = token.Substring(1) + " ";
                        i++;
                        if (i == tokens.Length)
                        {
                            return null;
                        }

                        token = tokens[i];

                        while (!token.Last().Equals('"'))
                        {
                            aggregate += token + " ";
                            i++;
                            if (i == tokens.Length)
                            {
                                return null;
                            }

                            token = tokens[i];
                        }

                        aggregate += token.Substring(0, token.Length - 1);
                        keys.Add(aggregate);
                    }
                    else
                    {
                        keys.Add(token);
                    }
                }

            }

            return keys;
        }

        static bool readSlides(Dictionary<String, HashSet<String>> dictionary, Dictionary<String, ArrayList> wordLists)
        {
            var txtFiles = Directory.EnumerateFiles(SOURCE_DIRECTORY, "*.pptx");
            var ppr = new PowerpointReader();

            foreach (string currentFile in txtFiles)
            {
                Console.WriteLine("Reading " + currentFile + "...");
                String contents = null;
                try
                {
                    contents = ppr.getText(currentFile);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

                if (contents == null)
                {
                    Console.WriteLine("Error reading the file");
                    return false;
                }

                String[] tokens = Regex.Split(contents, "[,\r\t\v ]");
                int length = tokens.Length;

                var wordList = new ArrayList();
                wordLists.Add(currentFile, wordList);

                for (int i = 0; i < length; i++)
                {
                    String token = tokens[i].ToLowerInvariant().Trim();

                    if (token.Equals("") || token.Equals("-"))
                    {
                        continue;
                    }

                    if (!dictionary.ContainsKey(token))
                    {
                        dictionary.Add(token, new HashSet<string>());
                    }

                    dictionary[token].Add(currentFile);
                    wordLists[currentFile].Add(token);
                }
                
            }

            ppr.Dispose();

            return true;
        }

        static void printList(HashSet<String> set)
        {
            foreach (String s in set)
            {
                Console.WriteLine(s);
            }
        }

        static HashSet<String> findPhrase(String phrase, Dictionary<String, ArrayList> wordLists)
        {
            var lectures = new HashSet<String>();

            var allLectures = wordLists.Keys;
            ArrayList currentWords;

            foreach (string lecture in allLectures)
            {
                currentWords = wordLists[lecture];
                if (containsPhrase(phrase, currentWords))
                {
                    lectures.Add(lecture);
                }
            }

            return lectures;
        }

        static bool containsPhrase(String phrase, ArrayList wordListAL)
        {
            String[] tokens = phrase.Split(' ');
            String[] wordList = (String []) wordListAL.ToArray(Type.GetType("System.String"));

            for (int i = 0; i < wordList.Length; i++)
            {
                String currentWord = wordList[i];
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

        static bool isOneWord(String s)
        {
            return !s.Contains(" ");
        }

        static bool constructDictionary(Dictionary<String, HashSet<String>> dictionary)
        {
            String contents = null;
            FileStream fs;
            StreamReader sr;

            try
            {
                fs = File.Open(FILE_NAME, FileMode.Open);
                sr = new StreamReader(fs);

                contents = sr.ReadToEnd();

                sr.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            if (contents == null)
            {
                Console.WriteLine("Error reading the file");
                return false;
            }

            String[] tokens = Regex.Split(contents, "\r\n");
            int length = tokens.Length;
            String currentLecture = null;

            for (int i = 0; i < length; i++)
            {
                String token = tokens[i].ToLowerInvariant().Trim();

                if (token.Equals("lecture"))
                {
                    currentLecture = tokens[i+1];
                    i++;
                }
                else
                {
                    if (currentLecture == null)
                    {
                        Console.WriteLine("File improperly formated");
                        return false;
                    }

                    if (!dictionary.ContainsKey(token))
                    {
                        dictionary.Add(token, new HashSet<string>());
                    }

                    dictionary[token].Add(currentLecture);
                }
            }

            return true;
        }
    }
}
