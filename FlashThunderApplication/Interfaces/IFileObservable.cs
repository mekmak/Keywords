using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashThunderApplication
{
    public interface IFileObservable
    {
        void Subscribe(IFileObserver observer);

        void NotifyOnNextFile(string message);

        void NotifyOnDone(string message);
    }
}
