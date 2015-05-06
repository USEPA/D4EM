using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EMSystemTesting
{
    public class testStoret
    {
        public bool testingStoret(string aProjectFolder, double nlat, double slat, double elong, double wlong)
        {
            bool pass = false;
            string aProjectFolderStoret = System.IO.Path.Combine(aProjectFolder, "Storet");
            string aCacheFolderStoret = System.IO.Path.Combine(aProjectFolderStoret, "Cache");
            string bboxVal = "bBox=" + wlong + "," + slat + "," + elong + "," + nlat;
            string subFolder = System.IO.Path.Combine(aProjectFolderStoret, "N" + nlat + ";S" + slat + ";E" + elong + ";W" + wlong);
            Directory.CreateDirectory(subFolder);
            string stationsFile = System.IO.Path.Combine(subFolder, "Stations");
            string resultsFile = System.IO.Path.Combine(subFolder, "Results");
            string aParamList = bboxVal;
            DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
            D4EM.Data.Region aRegion = new D4EM.Data.Region(nlat, slat, wlong, elong, aDesiredProjection);
            try
            {
                bool downloadcsv = D4EM.Data.Source.Storet.GetStations(aRegion, stationsFile, aParamList, "csv");
                bool downloadxml = D4EM.Data.Source.Storet.GetStations(aRegion, stationsFile, aParamList, "xml");
                //   D4EM.Data.Source.Storet.GetResults(aRegion, resultsFile, aParamList, aFileExt);
                if ((downloadcsv == true) && (downloadxml == true))
                {
                    EPAUtility.StoretFileSupport storetfilesupport = new EPAUtility.StoretFileSupport();
                    storetfilesupport.WriteStoretFiles(stationsFile, subFolder, nlat, slat, elong, wlong);
                }
            }
            catch (Exception)
            {
            }
            if (Directory.Exists(subFolder))
            {
                string[] filesinDirectory = Directory.GetFiles(subFolder);
                int numFiles = filesinDirectory.Length;
                if (numFiles >= 1)
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                }
            }
            return pass;
        }
    }
}
