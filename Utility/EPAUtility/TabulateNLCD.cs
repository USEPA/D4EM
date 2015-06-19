using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DotSpatial.Data;

namespace EPAUtility
{
    public class TabulateNLCD
    {
        public TabulateNLCD()
        {
        }

        public DataTable tabulateNLCD(double totalArea, IRaster rl, int year)
        {
            int count = rl.NumColumns * rl.NumRows;

            DataTable percentagesNLCD = new DataTable();

            EPAUtility.NLCDLegend nlcdlegend = new EPAUtility.NLCDLegend(year);
            DataTable dtLegend = nlcdlegend.NLCDLegendTable;
            int countLegendRows = dtLegend.Rows.Count;


            DataTable tallyTable = new DataTable();
            tallyTable.Columns.Add("Code");
            tallyTable.Columns.Add("Tally");

            for (int i = 0; i < countLegendRows; i++)
            {
                string code = dtLegend.Rows[i][0].ToString();
                tallyTable.Rows.Add(code, 0);
            }
            /*
            foreach (double ind in sampleList)
            {
                int indx = Convert.ToInt32(ind);
                string index = indx.ToString();
                if (indx >= 1)
                {
                    tallyTable.Rows[indx - 1][1] = Convert.ToInt32(tallyTable.Rows[indx - 1][1]) + 1;
                }
            }
             * */
            for (int i = 0; i < rl.NumRows; i++)
            {
                for (int j = 0; j < rl.NumColumns; j++)
                {
                    double value = rl.Value[i, j];
                    int indx = Convert.ToInt32(value);
                    if (indx >= 1)
                    {
                        try
                        {
                            tallyTable.Rows[indx - 1][1] = Convert.ToInt32(tallyTable.Rows[indx - 1][1]) + 1;
                        }
                        catch(Exception ex)
                        {
                            break;
                        }
                    }
                }
            }
            percentagesNLCD.Columns.Add("Code");
            percentagesNLCD.Columns.Add("LandCoverType");
            percentagesNLCD.Columns.Add("Percentage");
            percentagesNLCD.Columns.Add("Area (acres)");

            double totalpercent = 0;

            for (int j = 0; j < tallyTable.Rows.Count; j++)
            {
                string index = tallyTable.Rows[j][0].ToString();
                int tally = Convert.ToInt32(tallyTable.Rows[j][1]);

                double percentage = Convert.ToDouble(tally) / Convert.ToDouble(count) * 100;
                double area = percentage * totalArea / 100;

                totalpercent = totalpercent + percentage;
                try
                {
                    DataRow rowLegend = dtLegend.Rows[j];
                    string code = rowLegend[0].ToString(); ;
                    string landCoverType = rowLegend[1].ToString();
                    percentage = Math.Round(percentage, 2);
                    area = Math.Round(area, 2);
                    if (tally != 0)
                    {
                        percentagesNLCD.Rows.Add(code, landCoverType, percentage, area);

                    }
                }
                catch (Exception exc)
                {
                }

            }

            return percentagesNLCD;
        }
    }
}
