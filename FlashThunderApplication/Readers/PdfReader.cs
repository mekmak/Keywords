using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;

namespace FlashThunderApplication
{
    class PdfReader : IReader
    {
        private string fileName;

        private PdfReader(string fileName)
        {
            this.fileName = fileName;
        }

        public static IReader GetInstance(string fileName)
        {
            return new PdfReader(fileName);
        }

        public string GetContents()
        {
            PDDocument doc = PDDocument.load(fileName);
            PDFTextStripper pdfStripper = new PDFTextStripper();
            return pdfStripper.getText(doc);
        }
    }
}
