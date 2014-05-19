using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashThunderApplication.Context
{
    public class AppContext
    {
        public string VersionNumber { get; private set; }

        public AppContext(string versionNumber)
        {
            VersionNumber = versionNumber;
        }
    }
}
