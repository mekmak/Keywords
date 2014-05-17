using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using FlashThunderApplication.Readers;

namespace FlashThunderApplication
{
    static class Driver
    {
        const uint RPC_E_SERVERCALL_RETRYLATER = 0x8001010A;
        const uint VBA_E_IGNORE = 0x800AC472;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //string directoryname = @"C:\Users\Tomek\Documents\Classes\70-160\Lectures";
            //DirectoryInfo dirinfo = new DirectoryInfo(directoryname);
            //foreach (FileInfo file in dirinfo.EnumerateFiles())
            //{
            //    if (isPowerPoint(file.FullName))
            //    {
            //        getContents(file.FullName);
            //    }
            //}

            //GC.Collect(2);
            //GC.WaitForPendingFinalizers();

            //string fileName = @"C:\Users\Tomek\Documents\Useful Info\nova_directions.pdf";
            //string fileName = @"C:\Users\Tomek\Documents\Random Shit\Yoo.doc";
            //IReader reader = DOCReader.GetInstance(fileName);
            //Console.WriteLine(reader.GetContents());

            //System.Diagnostics.Process.Start(fileName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(new FlashThunder()));
        }

        public static string getContents(string fileName)
        {
            string presentation_text = "";
            PowerPoint.Application PowerPoint_App = new PowerPoint.Application();

            try
            {
                PowerPoint.Presentation presentation = PowerPoint_App.Presentations.Open(fileName);
                PowerPoint.Slides slides = presentation.Slides;

                for (int i = 0; i < slides.Count; i++)
                {
                    PowerPoint.Slide slide = slides[i + 1];
                    PowerPoint.Shapes shapes = slide.Shapes;

                    for (int j = 0; j < shapes.Count; j++)
                    {
                        PowerPoint.Shape shape = shapes[j + 1];

                        if (shape.HasTextFrame == MsoTriState.msoTrue)
                        {
                            PowerPoint.TextFrame textFrame = shape.TextFrame;
                            if (textFrame.HasText == MsoTriState.msoTrue)
                            {
                                PowerPoint.TextRange textRange = textFrame.TextRange;
                                var text = textRange.Text;
                                presentation_text += text + " ";
                            }
                        }

                    }
                }

                presentation.Close();
            }
            catch (COMException e)
            {
                if (IsRetry(e))
                {
                    Thread.Sleep(100);
                    getContents(fileName);
                }
            }
            finally
            {
                PowerPoint_App.Quit();
            }

            return presentation_text;
        }

        static bool IsRetry(COMException e)
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

        private static bool isPowerPoint(String fileName)
        {
            return (Regex.IsMatch(fileName, ".*ppt") || Regex.IsMatch(fileName, ".*pptx"));
        }
    }
}
