using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EMSystemTesting
{
    public class testBasins
    {
        public bool testingBasins(string aProjectFolder, string aHUC, D4EM.Data.LayerSpecification dt)
        {
            bool pass = false;
            string aProjectFolderBasins = System.IO.Path.Combine(aProjectFolder, "Basins");
            string aCacheFolderBasins = System.IO.Path.Combine(aProjectFolderBasins, "Cache");
            
            DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
            D4EM.Data.Region aRegion = new D4EM.Data.Region(0,0,0,0, aDesiredProjection);
            D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderBasins, aProjectFolderBasins, aRegion, false, true);
            
            string aSaveFolder = dt.Tag + "_" + aHUC;
            try
            {
                D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC, dt);
            }
            catch (Exception ex)
            {
            }
            string aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
            if (Directory.Exists(aSubFolder))
            {
                string[] filesinDirectory = Directory.GetFiles(aSubFolder);
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
