using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;

namespace EPAUtility
{
    public class NASSLegend
    {

        private DataTable legendTable = new DataTable();

        public NASSLegend()
        {
            writeTempFile();
            legendTable = createDataTable();            
        }

        private static DataTable createDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Code");
            dt.Columns.Add("Cmdty");

            string legendFile = Path.GetFullPath(@"..\Bin\NASSLegend.txt");            

            TextReader read = new StreamReader(legendFile);

            string line;

            char[] sep = new char[1];
            sep[0] = ' ';

            int i = 0;
            string[] rowArray = new string[2];
            while ((line = read.ReadLine()) != null)
            {
                string[] sites = line.Split(sep, 2);
                string code = sites[0].Trim();
                string cmdty = sites[1].Trim();   
                dt.Rows.Add(code, cmdty);                 
            }
            read.Close();            

            return dt;
        }

        /*
        private static DataTable createDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Code");
            dt.Columns.Add("Cmdty");
            dt.Columns.Add("Cmdty_desc");
            dt.Columns.Add("Cmdty_desc2");

            string tempFile = @"C:\Temp\NASSLegendTemp";

            TextReader read = new StreamReader(tempFile);           

            string line;

            char[] sep = new char[1];
            sep[0] = '>';

            int i = 0;
            string[] rowArray = new string[4];
            while ((line = read.ReadLine()) != null)
            {
                if (line.Contains("<FONT"))
                {
                    line = read.ReadLine();
                    string[] sites = line.Split(sep, 2);       
                    string value = sites[0].Trim();
                    if((value != "cmdty") && (value != "cmdty_desc") && (value != "cmdty_desc2"))
                    {
                        value = value.Replace("&amp", "");
                        rowArray[i] = value.Trim();
                        i++;
                        if (i == 4)
                        {
                            dt.Rows.Add(rowArray);                            
                            i = 0;                                
                        }
                    }     
                }
            }            
            read.Close();
            File.Delete(tempFile);

            return dt;
        }
        */
        private static void writeTempFile()
        {
            string tempFile = @"C:\Temp\NASSLegendTemp";

            TextWriter tw = new StreamWriter(tempFile);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.nass.usda.gov/Data_and_Statistics/County_Data_Files/Frequently_Asked_Questions/commcodes.html");
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0);

            tw.WriteLine(sb);
            tw.Close();
        }

        public DataTable NASSLegendTable
        {
            get { return legendTable; }
            set { legendTable = null; }
        }
    }
}
