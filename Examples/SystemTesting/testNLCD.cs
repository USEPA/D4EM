using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using D4EM.Data;
using D4EM.Data.Source;
using D4EM.Geo;

namespace D4EMSystemTesting
{
    public class testNLCD
    {
        public bool testingNLCD(string aProjectFolder, double aNorth, double aSouth, double aEast, double aWest, D4EM.Data.LayerSpecification dt)
        {
            bool pass = false;
            string aProjectFolderNLCD = System.IO.Path.Combine(aProjectFolder, "NLCD");
            string aCacheFolderNLCD = System.IO.Path.Combine(aProjectFolderNLCD, "Cache");
            DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
            D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
            D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNLCD, aProjectFolderNLCD, aRegion, true, true);
            string aSaveFolder = dt.Tag + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNLCD, aSaveFolder);
            try
            {
                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, dt);
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
                else
                {
                    pass = false;
                }
            }
            return pass;
        }
        
    }
}
