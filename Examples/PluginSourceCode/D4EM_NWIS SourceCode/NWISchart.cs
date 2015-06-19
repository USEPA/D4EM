using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace D4EM_NWIS
{
    public partial class NWISchart : Form
    {
        public DataTable dt = new DataTable();

        public NWISchart(DataTable _dt)
        {
            dt = _dt;
            InitializeComponent();
        }

        private void NWISchart_Load(object sender, EventArgs e)
        {
            string[] dates = new string[dt.Rows.Count];
            double[] values = new double[dt.Rows.Count];

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string date = dr["Date"].ToString();
                double value = 0;
                string string_value = dr["Discharge"].ToString();
                if (string_value != "")
                {
                    try
                    {
                        value = Convert.ToDouble(string_value);
                        values[i] = value;
                    }
                    catch (Exception ex)
                    {
                    }
                }

                dates[i] = date;

                i++;
            }
            chart1.Series[0].Points.DataBindXY(dates, values);
        }
    }
}
