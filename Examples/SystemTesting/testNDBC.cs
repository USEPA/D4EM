using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EMSystemTesting
{
    public class testNDBC
    {
        public bool testingNDBC(string aProjectFolder, double lat, double lng, double radius)
        {
            bool pass = false;

            string aProjectFolderNDBC = System.IO.Path.Combine(aProjectFolder, "NDBC");
            
            string aSaveFolder = "Lat" + lat + ";Lng" + lng + ";Radius" + radius;
            try
            {
                D4EM.Data.Source.NDBC ndbc = new D4EM.Data.Source.NDBC(aProjectFolderNDBC, aSaveFolder, lat, lng, radius);
            }
            catch (Exception ex)
            {
            }
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNDBC, aSaveFolder);
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
