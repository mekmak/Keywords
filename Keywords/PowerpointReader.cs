using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace Keywords
{
    class PowerpointReader : IDisposable
    {
        const uint RPC_E_SERVERCALL_RETRYLATER = 0x8001010A;
        const uint VBA_E_IGNORE = 0x800AC472;

        private bool disposed = false;
        PowerPoint.Application PowerPoint_App;

        public PowerpointReader()
        {
            PowerPoint_App = new Microsoft.Office.Interop.PowerPoint.Application();
        }

        ~PowerpointReader()
        {
            Dispose(false);
        }

        public String getText(String fileName)
        {
            string presentation_text = "";

            try
            {
                Microsoft.Office.Interop.PowerPoint.Presentation presentation = PowerPoint_App.Presentations.Open(fileName);
      
                for (int i = 0; i < presentation.Slides.Count; i++)
                {
                    foreach (var item in presentation.Slides[i + 1].Shapes)
                    {
                        var shape = (PowerPoint.Shape)item;
                        if (shape.HasTextFrame == MsoTriState.msoTrue)
                        {
                            if (shape.TextFrame.HasText == MsoTriState.msoTrue)
                            {
                                var textRange = shape.TextFrame.TextRange;
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
                    getText(fileName);
                }
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    PowerPoint_App.Quit();
                }
                disposed = true;
            }
        }
    }
}
