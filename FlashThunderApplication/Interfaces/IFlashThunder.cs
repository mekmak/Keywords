using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashThunderApplication.Interfaces
{
    public interface IFlashThunder : IFileObservable
    {
        void OpenFile(string fileName);

        HashSet<string> Query(string query);

        HashSet<string> LoadDirectory(string directoryName);
    }
}
