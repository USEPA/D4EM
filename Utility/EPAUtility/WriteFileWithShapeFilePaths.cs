using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EPAUtility
{
    public class WriteFileWithShapeFilePaths
    {
        public WriteFileWithShapeFilePaths(TextWriter fileShpTif, string aProjectFolder, string aSaveFolder)
        {
            
            string filePath = System.IO.Path.Combine(aProjectFolder, aSaveFolder);
            if (Directory.Exists(filePath))
            {
                string[] shp = Directory.GetFiles(filePath, "*.shp", SearchOption.AllDirectories);
                int i = 0;
                while (i < shp.Length)
                {
                    if (File.Exists(shp[i]))
                    {
                        fileShpTif.WriteLine(shp[i]);
                    }
                    i++;
                }
            }
        }
    }
}
