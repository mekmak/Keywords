using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FlashThunderApplication.Readers
{
    class TextFileReader : IReader
    {
        private string fileName;

        private TextFileReader(string fileName)
        {
            this.fileName = fileName;
        }

        public static IReader GetInstance(string fileName)
        {
            return new TextFileReader(fileName);
        }

        public string GetContents()
        {
            string text;

            try
            {
                var streamReader = new StreamReader(fileName);
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
