using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EMSystemTesting
{
    public class testNASS
    {
        public bool testingNASS(string aProjectFolder, int year, double north, double south, double east, double west)
        {
            bool pass = false;
            string aProjectFolderNASS = System.IO.Path.Combine(aProjectFolder, "NASS");
            string aCacheFolderNASS = System.IO.Path.Combine(aProjectFolderNASS, "Cache");
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNASS, year.ToString() + ";N" + north + ";S" + south + ";E" + east + ";W" + west);
            try
            {
                D4EM.Data.Source.NASS.getData(aSubFolder, aCacheFolderNASS, "", year, north, south, east, west);
            }
            catch (Exception)
            {
            }
            if (Directory.Exists(aSubFolder))
            {
                string[] filesinDirectory = Directory.GetFiles(aSubFolder);
                int numFiles = filesinDirectory.Length;
                if (numFiles >= 1)
                {
                    pass = true;
                }
            }
            return pass;
        }
    }
}
