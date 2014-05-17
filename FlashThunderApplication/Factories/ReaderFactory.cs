using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using FlashThunderApplication.Readers;

namespace FlashThunderApplication
{
    class ReaderFactory
    {
        private ReaderFactory() { }

        public static IReader GetReader(string fileName)
        {
            if (isPowerPoint(fileName))
            {
                return PowerPointReader.GetInstance(fileName);
            }

            if (isPDF(fileName))
            {
                return PdfReader.GetInstance(fileName);
            }

            if (isWordDocument(fileName))
            {
                return WordDocumentReader.GetInstance(fileName);
            }

            if (isTextFile(fileName))
            {
                return TextFileReader.GetInstance(fileName);
            }

            return null;
        }

        public static void CleanUp()
        {
            PowerPointReader.CleanUp();
            WordDocumentReader.CleanUp();
        }

        private static bool isPDF(string fileName)
        {
            return Regex.IsMatch(fileName, ".*pdf");
        }

        private static bool isPowerPoint(string fileName)
        {
            return (Regex.IsMatch(fileName, ".*ppt") || Regex.IsMatch(fileName, ".*pptx"));
        }

        private static bool isWordDocument(string fileName)
        {
            return (Regex.IsMatch(fileName, ".*doc") || Regex.IsMatch(fileName, ".*docx"));
        }

        private static bool isTextFile(string fileName)
        {
            return Regex.IsMatch(fileName, ".*txt");
        }
    }
}
