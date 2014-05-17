using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using Word = Microsoft.Office.Interop.Word;

namespace FlashThunderApplication.Readers
{
    class WordDocumentReader : IReader
    {
        const uint RPC_E_SERVERCALL_RETRYLATER = 0x8001010A;
        const uint VBA_E_IGNORE = 0x800AC472;

        private static Word._Application wordApp;
        private string fileName;

        private WordDocumentReader(string fileName)
        {
            this.fileName = fileName;
        }

        public static IReader GetInstance(string fileName)
        {
            if (wordApp == null)
            {
                try
                {
                    wordApp = new Word.Application();
                }
                catch (COMException)
                {
                    return null;
                }
            }

            return new WordDocumentReader(fileName);
        }

        public string GetContents()
        {
            string text = "";

            try
            {
                Word.Documents documents = wordApp.Documents;
                bool visible = false;
                bool readOnly = false;
                Word._Document document = documents.Open(fileName, visible, readOnly);
                Word.Range range = document.Content;
                text = range.Text;
                document.Close();
            }
            catch (COMException e)
            {
                if (isRetry(e))
                {
                    Thread.Sleep(100);
                    this.GetContents();
                }
            }

            return text;
        }

        private static bool isRetry(COMException e)
        {
            uint errorCode = (uint)e.ErrorCode;
            switch (errorCode)
            {
                case RPC_E_SERVERCALL_RETRYLATER:
                case VBA_E_IGNORE:
                    return true;
                default:
                    return false;
            }
        }

        public static void CleanUp()
        {
            if (wordApp != null)
            {
                GC.Collect(2);
                GC.WaitForPendingFinalizers();
                wordApp.Quit();
                wordApp = null;
            }
        }
    }
}
