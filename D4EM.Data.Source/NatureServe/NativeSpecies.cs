using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace D4EM.Data.Source
{
    public class NativeSpecies
    {
        public string csvFile;
        string tempFile = @"C:\Temp\NatureServeFishTemp";

         public NativeSpecies(string aProjectFolderNatureServe, string aHUC)
         {
             csvFile = System.IO.Path.Combine(aProjectFolderNatureServe, aHUC + " NativeSpecies.csv");
             writeTempFile(aHUC);     
             writeCSVFile(aProjectFolderNatureServe, aHUC);
             File.Delete(tempFile);
         }

         private void writeCSVFile(string aProjectFolder, string aHuc)
         {
             string aProjectFolderNatureServe = aProjectFolder;
             string tableFile = System.IO.Path.Combine(aProjectFolder, aHuc + " NativeSpecies.csv");
             csvFile = tableFile;

             TextReader read = new StreamReader(tempFile);
             TextWriter write = new StreamWriter(tableFile);

             int counter = 0;
             string line;

             char[] sep = new char[2];
             sep[0] = '>';
             sep[1] = '<';

             write.Write("Scientific Name,Common Name,Occurrence Status,");

             while ((line = read.ReadLine()) != null)
             {
                 if (line.Contains("<tr>"))
                 {
                     while ((line = read.ReadLine()) != null)
                     {
                         if (line.Contains("<td"))
                         {
                             string[] sites = line.Split(sep, 15);
                             if (sites.Length >= 6)
                             {
                                 write.WriteLine();
                                 string speciesname = sites[6];
                                 write.Write(sites[6] + ",");
                             }
                             else
                             {
                                 write.Write(sites[2] + ",");
                             }
                         }
                     }
                 }
                 counter++;
             }
             write.Close();

             read.Close();
         }


         private void writeTempFile(string aHuc)
         {
             TextWriter tw = new StreamWriter(tempFile);
             HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.natureserve.org/getData/dataSets/watershedHucs/hucTable.jsp?huc=" + aHuc);

             // used to build entire input
             StringBuilder sb = new StringBuilder();

             // used on each read operation
             byte[] buf = new byte[8192];

             // execute the request
             HttpWebResponse response = (HttpWebResponse)
                 request.GetResponse();

             // we will read data via the response stream
             Stream resStream = response.GetResponseStream();

             string tempString = null;
             int count = 0;

             do
             {
                 // fill the buffer with data
                 count = resStream.Read(buf, 0, buf.Length);

                 // make sure we read some data
                 if (count != 0)
                 {
                     // translate from bytes to ASCII text
                     tempString = Encoding.ASCII.GetString(buf, 0, count);

                     // continue building the string
                     sb.Append(tempString);
                 }
             }
             while (count > 0); // any more data to read?

             tw.WriteLine(sb);
             tw.Close();
         }

    }
}
