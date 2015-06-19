using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EM.Model.HE2RMES
{
    public class HE2RMESLog
    {
        string _sFilePath = null;

        public HE2RMESLog(string sFilePath)
        {
            _sFilePath = sFilePath;
            DateTime dt = DateTime.Now;
            string sDateTime = dt.ToString("yyyy/MM/dd HH:mm");
            // Create a file to write to. 
            if (File.Exists(_sFilePath))
            {
                File.Delete(_sFilePath);
            }

            using (StreamWriter sw = File.AppendText(_sFilePath))
            {
                sw.WriteLine("HE2RMES Log");
                sw.WriteLine("Date: " + sDateTime);
            }
        }

        public void WriteLine(string sText)
        {
            using (StreamWriter sw = File.AppendText(_sFilePath))
            {
                sw.WriteLine(sText);
            }
        }

    }
}
