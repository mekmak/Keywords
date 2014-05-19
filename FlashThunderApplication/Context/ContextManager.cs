using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashThunderApplication.Context
{
    public class ContextManager
    {
        private static ContextManager _instance;
        public static ContextManager Instance
        {
            get { return _instance ?? (_instance = new ContextManager()); }
        }

        private ContextManager() 
        {
            LoadContexts();
        }

        private void LoadContexts()
        {
            Version version = typeof(ContextManager).Assembly.GetName().Version;
            string versionNumber = string.Join(".", new string[] 
            {
                version.Major.ToString(), 
                version.Minor.ToString(), 
                version.MajorRevision.ToString(), 
                version.MinorRevision.ToString()
            });

            AppContext = new AppContext(versionNumber);
        }

        public AppContext AppContext { get; private set; }
    }
}
