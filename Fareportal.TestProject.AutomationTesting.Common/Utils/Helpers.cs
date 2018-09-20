using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding;

namespace Fareportal.TestProject.AutomationTesting.Common.Utils
{
    public static class Helpers
    {
        /// <summary>
        /// Send the DELETE request for all posted objects
        /// </summary>
        public static void CleanUp()
        {
            Dictionary<Type, List<ObjectToPostId>> fullStorage = DataManager.DataManager.Builder.Storage.GetAllItems();
            foreach (var concreteStorage in fullStorage)
            {
                foreach (var postedObjec in concreteStorage.Value)
                {
                    DataManager.DataManager.Destructor.Delete(postedObjec.PostId, (IDataGeneratable)concreteStorage.Key);
                }               
            }
        }

        /// <summary>
        /// Return the path to the file in assembly directory by provided fileName
        /// </summary>
        public static string GetPathToFileinAssemblyDirectory(string fileName)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.Combine(Path.GetDirectoryName(path), fileName);
        }

        /// <summary>
        /// Resolve the parameters based on following: 1) is parameters null; 2) is parameters needed for current builder/receiver 
        /// </summary>
        /// <param name="parameters">Dictionary of parameters</param>
        /// <param name="isParamsNeeded">Is parameters used in current builder/receiver</param>
        public static void CheckParameters(Dictionary<string, string> parameters, bool isParamsNeeded)
        {
            string callingClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            bool isParamsEmpty = parameters.IsNullOrEmpty();

            if (isParamsNeeded && isParamsEmpty)
                throw new ArgumentException($"{callingClassName} requires the additional parameters.");

            if (!isParamsNeeded && !isParamsEmpty)
                throw new ArgumentException($"{callingClassName} doesn't use parameters.\r\n" +
                                            $"But parameters are not empty.\r\n" +
                                            string.Join("\r\n", parameters.Select(el => $"Key/value: {el.Key} / {el.Value}")));
        }

        /// <summary>
        /// Compare the two byte arrays
        /// </summary>
        public static bool CompareByteArrays(byte[] first, byte[] second)
        {
            if (first.Length == second.Length)
            {
                int i = 0;
                while (i < first.Length && (first[i] == second[i]))
                {
                    i++;
                }
                if (i == first.Length)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get path to files by provided format 
        /// </summary>
        /// <param name="fileType">Type of file</param>
        /// <returns>Array of paths for all founded files</returns>
        public static string[] GetPathToFiles(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.PNG:
                    string pathToImage = GetPathToFileinAssemblyDirectory("PermanentTestData\\Images");
                    return Directory.GetFiles(pathToImage);

                default:
                    throw new NotImplementedException($"No implementation for fileType: {fileType}");
            }
        }
    }

    public enum FileType
    {
        PNG
    }
}