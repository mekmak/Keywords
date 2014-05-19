using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FlashThunderApplication.Readers
{
    class ConfigReader : IReader
    {
        private string FileName { get; set; }

        private ConfigReader(string fileName)
        {
            FileName = fileName;
        }

        public static IReader GetInstance(string fileName)
        {
            return new ConfigReader(fileName);
        }

        public string GetContents()
        {
            string text;

            try
            {
                var streamReader = new StreamReader(FileName);
                text = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return text;
        }
    }
}
