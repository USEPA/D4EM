using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace D4EM_NDBC
{
    public partial class NDBCchart2 : Form
    {
        public DataTable dt = new DataTable();

        public NDBCchart2(DataTable _dt)
        {
            dt = _dt;
            InitializeComponent();
        }

        private void NDBCchart2_Load(object sender, EventArgs e)
        {
            chartWSPD.Visible = false;
            chartPRES.Visible = false;
            chartATMP.Visible = true;
            chartWDIR.Visible = false;
            chartGST.Visible = false;
            chartWVHT.Visible = false;
            chartDPD.Visible = false;
            chartAPD.Visible = false;
            chartMWD.Visible = false;
            chartWTMP.Visible = false;
            chartDEWP.Visible = false;
            chartVIS.Visible = false;

            string[] times = new string[dt.Rows.Count];
            double[] wspdValues = new double[dt.Rows.Count];
            double[] pressureValues = new double[dt.Rows.Count];
            double[] atmpValues = new double[dt.Rows.Count];
            double[] wtmpValues = new double[dt.Rows.Count];
            double[] gstValues = new double[dt.Rows.Count];
            double[] wvhtValues = new double[dt.Rows.Count];
            double[] dpdValues = new double[dt.Rows.Count];
            double[] apdValues = new double[dt.Rows.Count];
            double[] dewpValues = new double[dt.Rows.Count];

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    int yr = Convert.ToInt32(dr["YY"].ToString());

                    if (yr > 12)
                    {
                        yr = 1900 + yr;
                    }
                    else
                    {
                        yr = 2000 + yr;
                    }
                    int month = Convert.ToInt32(dr["MM"].ToString());
                    int day = Convert.ToInt32(dr["DD"].ToString());
                    int hr = Convert.ToInt32(dr["hh"].ToString());

                    // double daysInMonth = 31;

                    //    double time = yr + (month - 1 + (day - 1 + (hr + min / 60) / 24) / daysInMonth) / 12;
                    times[i] = month.ToString() + "/" + day.ToString() + "/" + yr.ToString();

                    double wspd = Convert.ToDouble(dr["WSPD"].ToString());
                    double pressure = Convert.ToDouble(dr["BAR"].ToString());
                    double atmp = Convert.ToDouble(dr["ATMP"].ToString());
                    double wtmp = Convert.ToDouble(dr["WTMP"].ToString());
                    double gst = Convert.ToDouble(dr["GST"].ToString());
                    double wvht = Convert.ToDouble(dr["WVHT"].ToString());
                    double dpd = Convert.ToDouble(dr["DPD"].ToString());
                    double apd = Convert.ToDouble(dr["APD"].ToString());
                    //  double mwd = Convert.ToDouble(dr["MWD (deg)"].ToString());
                    double dewp = Convert.ToDouble(dr["DEWP"].ToString());
                    //  double vis = Convert.ToDouble(dr["VIS (nmi)"].ToString());
                    //  double tide = Convert.ToDouble(dr["TIDE (ft)"].ToString());


                    if ((wspd != 99.0) && (wspd != 0.0))
                    {
                        wspdValues[i] = wspd;
                    }
                    if ((pressure != 9999.0) && (pressure != 0.0))
                    {
                        pressureValues[i] = pressure;
                    }
                    if ((atmp != 999.0) && (atmp != 0.0))
                    {
                        atmpValues[i] = atmp;
                    }
                    if ((wtmp != 999.0) && (wtmp != 0.0))
                    {
                        wtmpValues[i] = wtmp;
                    }
                    if ((gst != 99.0) && (gst != 0.0))
                    {
                        gstValues[i] = gst;
                    }
                    if (wvht != 99.0)
                    {
                        wvhtValues[i] = wvht;
                    }
                    if (dpd != 99.0)
                    {
                        dpdValues[i] = dpd;
                    }
                    if (apd != 99.0)
                    {
                        apdValues[i] = apd;
                    }

                    if (dewp != 999.0)
                    {
                        dewpValues[i] = dewp;
                    }
                }
                catch (Exception ex)
                {
                }
                i++;
            }

            chartWSPD.Series[0].Points.DataBindXY(times, wspdValues);
            chartPRES.Series[0].Points.DataBindXY(times, pressureValues);
            chartATMP.Series[0].Points.DataBindXY(times, atmpValues);
            chartGST.Series[0].Points.DataBindXY(times, gstValues);
            chartWVHT.Series[0].Points.DataBindXY(times, wvhtValues);
            chartDPD.Series[0].Points.DataBindXY(times, dpdValues);
            chartAPD.Series[0].Points.DataBindXY(times, apdValues);
            chartWTMP.Series[0].Points.DataBindXY(times, wtmpValues);
            chartDEWP.Series[0].Points.DataBindXY(times, dewpValues);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartWSPD.Visible = false;
            chartPRES.Visible = false;
            chartATMP.Visible = false;
            chartWDIR.Visible = false;
            chartGST.Visible = false;
            chartWVHT.Visible = false;
            chartDPD.Visible = false;
            chartAPD.Visible = false;
            chartMWD.Visible = false;
            chartWTMP.Visible = false;
            chartDEWP.Visible = false;
            chartVIS.Visible = false;

            string selectedValue = comboBox1.SelectedItem.ToString();

            if (selectedValue == "Wind Speed (m/s)")
            {
                chartWSPD.Visible = true;
            }
            if (selectedValue == "Gust Speed (m/s)")
            {
                chartGST.Visible = true;
            }
            if (selectedValue == "Wave Height (m)")
            {
                chartWVHT.Visible = true;
            }
            if (selectedValue == "Dominant Wave Period (sec)")
            {
                chartDPD.Visible = true;
            }
            if (selectedValue == "Average Wave Period (sec)")
            {
                chartAPD.Visible = true;
            }
            if (selectedValue == "Direction of Dominant Waves (deg)")
            {
                chartAPD.Visible = true;
            }
            if (selectedValue == "Sea Level Pressure (hPa)")
            {
                chartPRES.Visible = true;
            }
            if (selectedValue == "Air Temperature (degC)")
            {
                chartATMP.Visible = true;
            }
            if (selectedValue == "Sea Surface Temperature (degC)")
            {
                chartWTMP.Visible = true;
            }
            if (selectedValue == "Dewpoint Temperature (degC)")
            {
                chartDEWP.Visible = true;
            }
        }
    }
}
