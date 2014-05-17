using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using FlashThunderApplication.Interfaces;

namespace FlashThunderApplication
{
    class FlashThunder : IFlashThunder
    {
        private bool mapsLoaded;
        private MapFactory mapFactory;
        private HashSet<IFileObserver> fileObservers;

        public FlashThunder()
        {
            mapsLoaded = false;
            fileObservers = new HashSet<IFileObserver>();
        }

        public HashSet<string> LoadDirectory(string directoryName)
        {
            DirectoryInfo dirInfo;
            mapFactory = null;
            mapsLoaded = false;

            try
            {
                dirInfo = new DirectoryInfo(directoryName);
            }
            catch
            {
                return null;
            }

            if (!dirInfo.Exists)
            {
                return null;
            }

            mapFactory = new MapFactory(dirInfo, this);
            if (!mapFactory.LoadMaps())
            {
                return null;
            }

            mapsLoaded = true;

            return mapFactory.AllFiles;
        }

        public HashSet<string> Query(string query)
        {
            if (!mapsLoaded)
            {
                return null;
            }

            if (query.Equals(""))
            {
                return new HashSet<string>();
            }

            var directorySet = new HashSet<string>();
            query = query.Trim().ToLowerInvariant();
            var allSets = new HashSet<HashSet<string>>();

            HashSet<IMap> allMaps = mapFactory.AllMaps;
            foreach (IMap map in allMaps)
            {
                HashSet<string> queryResult = map.Query(query);
                if (queryResult != null)
                {
                    allSets.Add(queryResult);
                }
            }

            return FlashThunder.IntersectAllSets(allSets);
        }

        public static HashSet<T> IntersectAllSets<T>(HashSet<HashSet<T>> allSets)
        {
            if (allSets == null || allSets.Count == 0)
            {
                return new HashSet<T>();
            }

            var firstSet = allSets.ElementAt(0);

            if (allSets.Count == 1)
            {
                return firstSet;
            }

            foreach (HashSet<T> set in allSets)
            {
                firstSet.IntersectWith(set);
            }

            return firstSet;
        }

        public void OpenFile(string fileName)
        {
            System.Diagnostics.Process.Start(fileName);
        }

        public void Subscribe(IFileObserver observer)
        {
            fileObservers.Add(observer);
        }

        public void NotifyOnDone(string message)
        {
            foreach (IFileObserver observer in fileObservers)
            {
                observer.OnDone(message);
            }
        }

        public void NotifyOnNextFile(string message)
        {
            foreach (IFileObserver observer in fileObservers)
            {
                observer.OnNextFile(message);
            }
        }
    }
}
