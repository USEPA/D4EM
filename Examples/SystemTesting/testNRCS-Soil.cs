using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EMSystemTesting
{
    public class testNRCS_Soil
    {
        public bool testingNRCSSoil(string aProjectFolder, double aLatitude, double aLongitude, double aRadiusInitial, double aRadiusMax, double aRadiusIncrement)
        {
            bool pass = false;
            string aProjectFolderNRCSSoil = System.IO.Path.Combine(aProjectFolder, "NRCS-Soil");
            string aCacheFolderNRCSSoil = System.IO.Path.Combine(aProjectFolderNRCSSoil, "Cache");
            
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNRCSSoil, "Lat" + aLatitude.ToString() + "Lng" + aLongitude.ToString());
            string csvFile = System.IO.Path.Combine(aSubFolder, "Lat" + aLatitude.ToString() + "Lng" + aLongitude.ToString() + ".csv");
            try
            {
                List<D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil> soils = new List<D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil>();
                soils = D4EM.Data.Source.NRCS_Soil.SoilLocation.FindSoils(aLatitude, aLongitude, aRadiusInitial, aRadiusMax, aRadiusIncrement);
                EPAUtility.NRCS_SoilFileSupport soilfilesupport = new EPAUtility.NRCS_SoilFileSupport(soils, aProjectFolderNRCSSoil, aCacheFolderNRCSSoil, aSubFolder, aLatitude, aLongitude, aRadiusInitial, aRadiusMax, aRadiusIncrement);
                soilfilesupport.WriteSoilFiles();
            }
            catch (Exception ex)
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
