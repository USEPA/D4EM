using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using atcUtility;

namespace SDP_Project_Builder_Batch
{
    public class SDPBatchParameters
    {

        private string _sBatchName;
        private string _sBatchDescription;
        private List<string> _lstProjectFiles = new List<string>();


        public SDPBatchParameters()
        {
        }

        public SDPBatchParameters(string sBatchFileName)
        {            
        
        }

        public string BatchName
        {
            get { return _sBatchName; }
            set { _sBatchName = value; }
        }

        public string BatchDescription
        {
            get { return _sBatchDescription; }
            set { _sBatchDescription = value; }
        }

        public List<string> ProjectFiles
        {
            get { return _lstProjectFiles; }
            set { _lstProjectFiles = value; }
        }

        public void ReadParametersTextFile(string sFileName)
        {
            if  (File.Exists(sFileName)) 
            {
                foreach (string line in atcUtility.modFile.LinesInFile(sFileName))
                {
                    if ((line != null) && (!String.IsNullOrEmpty(line)) && (!line.StartsWith("#")))
                    {
                        string[] items = line.Split(',');
                        if (items.Length > 1)
                        {
                            switch (items[0])
                            {
                                case "BatchName":
                                    BatchName = items[1];
                                    break;
                                case "BatchDescription":
                                    BatchDescription = items[1];
                                    break;
                                case "ProjectFiles":
                                    ProjectFiles = new List<string>(items[1].Split('|'));
                                    break;
                                default:
                                    MapWinUtility.Logger.Dbg("Unused line in HE2RMES Batch parameter file: '" + line + "' in file '" + sFileName + "'");
                                    break;
                            }
                        }
                        else
                        {
                            MapWinUtility.Logger.Dbg("Found " + items.Length + " but expected 2 comma-separated values in line '" + line + "' in file '" + sFileName + "'");
                        }
                    }
                }
            }
        }

        public void WriteParametersTextFile(string sFilename)
        {
            File.WriteAllText(sFilename, ParametersToString());
        }

        private void AppendList(String VariableName, List<String> Values,  StringBuilder sb )
        {
            if ((Values != null) && (Values.Count > 0))
            {
                sb.AppendLine(VariableName + "," + String.Join("|", Values.ToArray()));
            }
        }

        public string ParametersToString()
        {

            StringBuilder sb = new StringBuilder();    

            //Dim lFilename As String
            //lFilename = D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.huc8)
            //If IO.File.Exists(lFilename) Then sb.AppendLine("NationalHuc8," & lFilename)
            //lFilename = D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.county)
            //If IO.File.Exists(lFilename) Then sb.AppendLine("NationalCounty," & lFilename)
            //lFilename = D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.state)
            //If IO.File.Exists(lFilename) Then sb.AppendLine("NationalState," & lFilename)

            sb.AppendLine("BatchName," + BatchName);
            sb.AppendLine("BatchDescription," + BatchDescription);   
            AppendList("ProjectFiles", ProjectFiles, sb);

       
            return sb.ToString();
        }

    }
}
