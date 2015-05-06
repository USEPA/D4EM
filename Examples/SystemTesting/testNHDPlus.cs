using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EMSystemTesting
{
    class testNHDPlus
    {
        public bool testingNHDPlus(string aProjectFolder, string aHUC, D4EM.Data.LayerSpecification dt)
        {
            bool pass;
            string aProjectFolderNHDPlus = System.IO.Path.Combine(aProjectFolder, "NHDPlus");
            string aCacheFolderNHDPlus = System.IO.Path.Combine(aProjectFolderNHDPlus, "Cache");
            DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.AlbersProjection();
            D4EM.Data.Region aRegion = new D4EM.Data.Region(0,0,0,0, aDesiredProjection);
            D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNHDPlus, aProjectFolderNHDPlus, aRegion, false, false);
            string aSaveFolder = aHUC + " " + dt.Tag;
            try
            {
                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC, true, dt);
            }
            catch (Exception ex)
            {
            }
            int numFiles = 0;
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNHDPlus, aSaveFolder);
            if (Directory.Exists(aSubFolder))
            {
                string[] filesinDirectory = Directory.GetFiles(aSubFolder, "", SearchOption.AllDirectories);
                numFiles = filesinDirectory.Length;
                string[] subdirectoryEntries = Directory.GetDirectories(aSubFolder);

                foreach (string subdirectory in subdirectoryEntries)
                {
                    filesinDirectory = Directory.GetFiles(subdirectory);
                    numFiles = numFiles + filesinDirectory.Length;
                    subdirectoryEntries = Directory.GetDirectories(subdirectory);
                    foreach (string subDirectory in subdirectoryEntries)
                    {
                        filesinDirectory = Directory.GetFiles(subDirectory);
                        numFiles = numFiles + filesinDirectory.Length;
                    }
                }
            }

            if ((Directory.Exists(aSubFolder)) && (numFiles >= 1))
            {
                pass = true;
            }
            else
            {
                pass = false;
            }
            return pass;
        }
    }
}
