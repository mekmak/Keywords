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
        private enum FileType
        {
            Unknown,
            PowerPoint,
            Pdf,
            Word,
            Text,
            Config,
            Xml
        }

        private static Dictionary<string, FileType> RegexFileTypeMap = new Dictionary<string, FileType>
        {
            {@".*\.config", FileType.Config},
            {@".*\.pdf", FileType.Pdf},
            {@".*\.ppt", FileType.PowerPoint},
            {@".*\.pptx", FileType.PowerPoint},
            {@".*\.doc", FileType.Word},
            {@".*\.docx", FileType.Word},
            {@".*\.txt", FileType.Text},
            {@".*\.xml", FileType.Xml}
        };

        private ReaderFactory() { }

        public static IReader GetReader(string fileName)
        {
            FileType fileType = GetFileType(fileName);

            switch(fileType)
            {
                case FileType.Xml:
                    return XmlReader.GetInstance(fileName);
                case FileType.Config:
                    return ConfigReader.GetInstance(fileName);
                case FileType.Pdf:
                    return PdfReader.GetInstance(fileName);
                case FileType.PowerPoint:
                    return PowerPointReader.GetInstance(fileName);
                case FileType.Text:
                    return TextFileReader.GetInstance(fileName);
                case FileType.Word:
                    return WordDocumentReader.GetInstance(fileName);
                case FileType.Unknown:
                default:
                    return null;
            }
        }

        public static void CleanUp()
        {
            PowerPointReader.CleanUp();
            WordDocumentReader.CleanUp();
        }

        private static FileType GetFileType(string fileName)
        {
            foreach(string regex in RegexFileTypeMap.Keys)
            {

                if (Regex.IsMatch(fileName, regex))
                {
                    return RegexFileTypeMap[regex];
                }
            }

            return FileType.Unknown;
        }

        
    }
}
