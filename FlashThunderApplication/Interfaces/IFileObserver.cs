using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashThunderApplication
{
    public interface IFileObserver
    {
        void OnNextFile(string message);

        void OnDone(string message);
    }
}
