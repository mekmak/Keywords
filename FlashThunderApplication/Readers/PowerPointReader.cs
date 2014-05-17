using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace FlashThunderApplication
{
    class PowerPointReader : IReader
    {
        const uint RPC_E_SERVERCALL_RETRYLATER = 0x8001010A;
        const uint VBA_E_IGNORE = 0x800AC472;

        private static PowerPoint._Application powerPointApp;
        private string fileName;

        private PowerPointReader(string fileName) 
        {
            this.fileName = fileName;
        }

        public static IReader GetInstance(string fileName)
        {
            if (powerPointApp == null)
            {
                try
                {
                    powerPointApp = new PowerPoint.Application();
                }
                catch (COMException)
                {
                    return null;
                }
            }

            return new PowerPointReader(fileName);
        }
        
        public String GetContents()
        {
            string presentation_text = "";

            try
            {
                PowerPoint.Presentations presentations = powerPointApp.Presentations;
                PowerPoint.Presentation presentation = presentations.Open(fileName, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
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
                if (isRetry(e))
                {
                    Thread.Sleep(100);
                    this.GetContents();
                }
            }

            return presentation_text;
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
            if (powerPointApp != null)
            {
                GC.Collect(2);
                GC.WaitForPendingFinalizers();
                powerPointApp.Quit();
                powerPointApp = null;
            }
        }
    }
}
