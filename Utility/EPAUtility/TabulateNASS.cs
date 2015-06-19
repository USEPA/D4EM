using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using DotSpatial.Data;

namespace EPAUtility
{
    public class TabulateNASS
    {
        public TabulateNASS()
        {
        }

        public DataTable tabulateNASS(double totalArea, IRaster rl)
        {
            int count = rl.NumColumns*rl.NumRows;

            DataTable percentagesNASS = new DataTable();

            EPAUtility.NASSLegend nasslegend = new EPAUtility.NASSLegend();
            DataTable dtLegend = nasslegend.NASSLegendTable;
            int countLegendRows = dtLegend.Rows.Count;

            DataTable tallyTable = new DataTable();
            tallyTable.Columns.Add("Code");
            tallyTable.Columns.Add("Tally");

            for (int i = 0; i < countLegendRows; i++)
            {
                string code = dtLegend.Rows[i][0].ToString();
                tallyTable.Rows.Add(code, 0);
            }
            
            for (int i = 0; i < rl.NumRows; i++)
            {
                for (int j = 0; j < rl.NumColumns; j++)
                {
                    double value = rl.Value[i, j];
                    int indx = Convert.ToInt32(value);                    
                    if (indx >= 1)
                    {
                        tallyTable.Rows[indx - 1][1] = Convert.ToInt32(tallyTable.Rows[indx - 1][1]) + 1;
                    }
                }
            }
            
            percentagesNASS.Columns.Add("Code");
            percentagesNASS.Columns.Add("Commodity");            
            percentagesNASS.Columns.Add("Percentage");
            percentagesNASS.Columns.Add("Area (acres)");

            TextWriter tw = new StreamWriter(@"C:\Temp\NASSTestTemp.txt");

            double totalpercent = 0;

            for (int j = 0; j < tallyTable.Rows.Count; j++)
            {
                string index = tallyTable.Rows[j][0].ToString();
                int tally = Convert.ToInt32(tallyTable.Rows[j][1]);

                double percentage = Convert.ToDouble(tally) / Convert.ToDouble(count) * 100;
                double area = percentage*totalArea/100;
                totalpercent = totalpercent + percentage;
                try
                {
                    DataRow rowLegend = dtLegend.Rows[j];
                    string code = rowLegend[0].ToString(); ;
                    string cmdty = rowLegend[1].ToString();
                    percentage = Math.Round(percentage, 2);
                    area = Math.Round(area, 2);
                    if (tally != 0)
                    {
                        percentagesNASS.Rows.Add(code, cmdty, percentage, area);
                        tw.WriteLine(code + " " + percentage);
                    }
                }
                catch (Exception exc)
                {
                }

            }
            tw.WriteLine(totalpercent);
            tw.Close();

            return percentagesNASS;
        }


    }
}
