using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;


namespace EPAUtility
{
    

    public class NLCDLegend
    {

        private DataTable legendTable = new DataTable();

       
        public NLCDLegend(int year)
        {
            
            legendTable = createDataTable(year);            
        }

        private static DataTable createDataTable(int year)
        {
            string legendFile = "";  
            switch (year)
            {
                case 1992:
                    legendFile = Path.GetFullPath(@"..\Bin\NLCDLandCover1992Legend.txt");
                    break;
                case 2001:
                    legendFile = Path.GetFullPath(@"..\Bin\NLCDLandCover2001and2006Legend.txt");
                    break;
                case 2006:
                    legendFile = Path.GetFullPath(@"..\Bin\NLCDLandCover2001and2006Legend.txt");
                    break;
            }

            DataTable dt = new DataTable();

            dt.Columns.Add("Code");
            dt.Columns.Add("LandCoverType");

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
                string landCoverType = sites[1].Trim();
                dt.Rows.Add(code, landCoverType);
            }
            read.Close();

            return dt;
        }

        public DataTable NLCDLegendTable
        {
            get { return legendTable; }
            set { legendTable = null; }
        }
    }
}
