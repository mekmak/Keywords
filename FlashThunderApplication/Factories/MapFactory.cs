using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using FlashThunderApplication.Interfaces;

namespace FlashThunderApplication
{
    class MapFactory
    {
        private DirectoryInfo dirInfo;
        private IFlashThunder flashThunder;

        public MapFactory(DirectoryInfo dirInfo, IFlashThunder flashThunder)
        {
            this.dirInfo = dirInfo;
            this.flashThunder = flashThunder;
            AllMaps = new HashSet<IMap>();
        }

        public HashSet<IMap> AllMaps
        {
            get;

            private set;
        }

        public bool LoadMaps()
        {
            IMap wordMap = new WordMap();
            IMap phraseMap = new PhraseMap();

            FileInfo[] files = dirInfo.GetFiles();
            int numFiles = files.Length;
            int numLoaded = 0;

            for (int i = 0; i < numFiles; i++)
            {
                flashThunder.NotifyOnNextFile("[" + (i + 1) + " / " + numFiles + "]");

                FileInfo file = files[i];
                IReader reader = ReaderFactory.GetReader(file.FullName);
                if (reader == null)
                {
                    continue;
                }

                string contents;
                try
                {
                    contents = reader.GetContents();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }

                // Split it because of annoying escape character stuff
                string regexFirstPart = @"[\.\?";
                string regexSecondPart = "!;:()\",\r\t\v ]";
                string regex = regexFirstPart + regexSecondPart;

                string[] tokens = Regex.Split(contents, regex);
                int length = tokens.Length;

                for (int j = 0; j < length; j++)
                {
                    string token = tokens[j].ToLowerInvariant().Trim();

                    if (token.Equals("") || token.Equals("-"))
                    {
                        continue;
                    }

                    ((WordMap)wordMap).Add(token, file.FullName);
                    ((PhraseMap)phraseMap).Add(token, file.FullName);
                }

                numLoaded++;
            }

            ReaderFactory.CleanUp();

            flashThunder.NotifyOnDone(numLoaded + " files");

            AllMaps.Add(wordMap);
            AllMaps.Add(phraseMap);

            return true;
        }

        
    }
}
