using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D4EM.Data;

///Parameters for running HSPF - modeled after SDMParameters.vb in SDMProjectBuilder.sln
///and modDownloadD4EM.vb
///created by Nick Pope, 27 March 2013

namespace D4EMInterface
{
    class HSPFParameters
    {
        public double aNorth = 33;
        public double aSouth = 32;
        public double aWest = -84;
        public double aEast = -83;

        //public bool SetupHSPF;
        public double MinCatchmentKM2;
        public double MinFlowlineKM = 0.0;
        public string ProjectsPath = "";
        public string ProjectFileFullPath = "";
        public string CacheFolder = "";
        public DotSpatial.Projections.ProjectionInfo DesiredProjection = D4EM.Data.Globals.AlbersProjection();
        public bool ClipCatchments;
        public List<string> SelectedKeys = new List<string>();
        public List<string> Catchments = new List<string>();
        public string CatchmentsMethod = "";
        public string SelectionLayer = "";
        public string SoilSource = "";
        public double LandUseIgnoreBelowFraction = 0.0;
        public double LandUseIgnoreBelowAbsolute = 0.0;
        public int SimulationStartYear = 0;
        public int SimulationEndYear = 0;
        public D4EM.Model.HSPF.modModelSetup.HspfOutputInterval HspfOutputInterval = D4EM.Model.HSPF.modModelSetup.HspfOutputInterval.Daily; //default for testing
        public int HspfSnowOption = 0; //assuming no snow for testing
        public bool HspfBacterialOption = false; //assuming no bacterial model for testing
        public D4EM.Data.LayerSpecification ElevationGrid = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid;
        public List<string> WQConstituents = new List<string>();
        public List<string> BasinsMetConstituents = new List<string>();
        public bool CreateArcSWATFiles = false;

        //Public NLDASconstituents As New Generic.List(Of String)
        public List<string> NLDASconstituents = new List<string>();

        //Public Sub New(ByVal aFilename As String)
        //    Me.ReadParametersTextFile(aFilename)
        //End Sub

        //gets parameters from SDMParameters.txt
        public void ReadParametersTextFile(string aFilename)
        {
            if (System.IO.File.Exists(aFilename))
            {
                foreach (string line in atcUtility.modFile.LinesInFile(aFilename))
                {
                    if (line != null && line != "" && !line.StartsWith("#"))
                    {
                        int lCommaPos = line.IndexOf(",");
                        if (lCommaPos > 0)
                        {
                            string lParameter = line.Substring(0, lCommaPos);
                            string lValue = line.Substring(lCommaPos + 1);
                        }
                    }
                }
            }
        }

        public string xmlText { get; set; } //questionable
        public string XML()
        {
            //get
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("ProjectsPath," + ProjectsPath);
            sb.AppendLine("<StreamCatchmentProcessing>");
            sb.AppendLine("    <MinimumStreamLength>" + MinFlowlineKM + "</MinimumStreamLength>");
            sb.AppendLine("    <MinimumCatchmentArea>" + MinCatchmentKM2 + "</MinimumCatchmentArea>");
            sb.AppendLine("    <MinumumLandUsePercent>" + LandUseIgnoreBelowFraction * 100 + "</MinumumLandUsePercent>");
            sb.AppendLine("</StreamCatchmentProcessing>");

            sb.AppendLine("<SimulationStartYear>" + SimulationStartYear + "</SimulationStartYear>");
            sb.AppendLine("<SimulationEndYear>" + SimulationEndYear + "</SimulationEndYear>");

            //sb.AppendLine("<HSPF SetupHSPF=" + SetupHSPF + ">");
            sb.AppendLine("    <HspfSnowOption>" + HspfSnowOption + "</HspfSnowOption>");
            sb.AppendLine("    <HspfBacterialOption>" + HspfBacterialOption + "</HspfBacterialOption>");
            //sb.AppendLine("    <HspfOutputInterval>" + [Enum].GetName(HspfOutputInterval.GetType, HspfOutputInterval) + "</HspfOutputInterval>");
            sb.AppendLine("HspfOutputInterval," + HspfOutputInterval);
            sb.AppendLine("</HSPF>");

            //AppendList("<NASSYears>", NASSYears, sb)
            //sb.AppendLine("<NASSStatistics>" + NASSStatistics + "</NASSStatistics>")

            //AppendList("<BasinsMetConstituents>", BasinsMetConstituents, sb)
            BasinsMetConstituents.ForEach(item => sb.AppendLine("BasinsMetConstituents," + item));

            //AppendList("<NCDCconstituents>", NCDCconstituents, sb)
            //If D4EM.Data.Source.NCDC.HasToken Then sb.AppendLine("NCDCtoken," + D4EM.Data.Source.NCDC.token)

            sb.AppendLine("<SoilSource>" + SoilSource + "</SoilSource>");
            sb.AppendLine("<CatchmentsMethod>" + CatchmentsMethod + "</CatchmentsMethod>");
            sb.AppendLine("<ElevationGrid>" + ElevationGrid.Name + "<ElevationGrid>");

            sb.AppendLine("<Project>");
            sb.AppendLine("  <ProjectFileFullPath>" + ProjectFileFullPath + "</ProjectFileFullPath>");

            if (!string.IsNullOrEmpty(SelectionLayer))
            {
                sb.AppendLine("SelectionLayer," + SelectionLayer);
                //AppendList("  <SelectedKeys>", SelectedKeys, sb)
                SelectedKeys.ForEach(item => sb.AppendLine("SelectedKeys," + item));
                //AppendList("  <Catchments>", Catchments, sb)
                Catchments.ForEach(item => sb.AppendLine("Catchments," + item));
                sb.AppendLine("</Project>");
            }
            xmlText = sb.ToString();
            return xmlText;

            //set
            string[] lEol;
            foreach (string line in xmlText.Split(lEol, StringSplitOptions.RemoveEmptyEntries))
            {
                string aLine = line;
                char[] chars = {Convert.ToChar(" "), Convert.ToChar(System.Environment.NewLine),
                                Convert.ToChar("\r"), Convert.ToChar("\n")};
                aLine = line.TrimStart(chars);
                aLine = line.TrimEnd(chars);
                //If Not line.StartsWith("#") Then
                if (!aLine.StartsWith("#"))
                {
                    string lParameter = null;
                    string lValue = null;
                    if (aLine.StartsWith("<"))
                    {
                        //'TODO: parse variable, value from line if it contains them
                    }
                    else
                    {
                        int lFirstComma = aLine.IndexOf(",");
                        if (lFirstComma > 0)
                        {
                            lParameter = line.Substring(0, lFirstComma);
                            lValue = line.Substring(lFirstComma + 1);
                        }
                    }
                    if (lParameter != null)
                    {
                        SetParameter(lParameter, lValue);
                    }
                }
            }
        }

        private void SetParameter(string aParameter, string aValue)
        {
            switch (aParameter)
            {
                //case "CreateArcSWATFiles": CreateArcSWATFiles = Convert.ToBoolean(aValue);
                //    break;
                case "DefaultUnit": SelectedKeys.Clear();
                    SelectedKeys.Add(aValue);
                    break;
                case "ProjectsPath": ProjectsPath = aValue;
                    break;
                case "ProjectFileFullPath": ProjectFileFullPath = aValue;
                    break;
                //case "SWAT2005Database": SWATDatabaseName = aValue;
                //    break;
                case "SoilSource": SoilSource = aValue;
                    break;
                case "MinimumStreamLength": MinFlowlineKM = Convert.ToDouble(aValue);
                    break;
                case "MinimumCatchmentArea": MinCatchmentKM2 = Convert.ToDouble(aValue);
                    break;
                case "MinumumLandUsePercent" : LandUseIgnoreBelowFraction = Convert.ToDouble(aValue) / 100;
                    break;
                case "NationalHuc8" : D4EM.Data.National.set_ShapeFilename(D4EM.Data.National.LayerSpecifications.huc8, aValue);
                    break;
                case "NationalCounty": D4EM.Data.National.set_ShapeFilename(D4EM.Data.National.LayerSpecifications.county, aValue);
                    break;
                case "NationalState" : D4EM.Data.National.set_ShapeFilename(D4EM.Data.National.LayerSpecifications.state, aValue);
                    break;
                case "SimulationStartYear" : SimulationStartYear = Convert.ToInt32(aValue);
                    break;
                case "SimulationEndYear" : SimulationEndYear = Convert.ToInt32(aValue);
                    break;
                //Case "SelectedKeys", "HucList" : SelectedKeys = New Generic.List(Of String)(aValue.Split(" "))
                case "SelectedKeys": case "HucList": 
                    SelectedKeys = new List<string>(aValue.Split(' '));
                    break;
                //case "OverwriteProject" : OverwriteProject = Convert.ToBoolean(aValue);
                case "CatchmentsMethod" : CatchmentsMethod = aValue;
                    break;
                //Case "SelectionLayer", "HucLayer" : SelectionLayer = aValue
                case "SelectionLayer": case "HucLayer":
                    SelectionLayer = aValue;
                    break;
                //Case "HspfOutputInterval"
                //[Enum].TryParse(aValue, HspfOutputInterval)
                case "HspfOutputInterval": HspfOutputInterval = D4EM.Model.HSPF.modModelSetup.HspfOutputInterval.Daily;
                    break;
                case "HspfSnowOption": HspfSnowOption = Convert.ToInt32(aValue);
                    break;
                case "HspfBacterialOption": HspfBacterialOption = Convert.ToBoolean(aValue);
                    break;
                //Case "BasinsMetConstituents" : BasinsMetConstituents = New Generic.List(Of String)(aValue.Split(" "))
                case "BasinsMetConstituents": BasinsMetConstituents = new List<string>();
                    //BasinsMetConstituents.Add(aValue.Split(" "));
                    break;
                case "WQConstituents":
                    WQConstituents = new List<string>();
                    string[] lRawItems = aValue.Split(','); //single quotes!
                    foreach (string lConstituent in lRawItems)
                    {
                        WQConstituents.Add(lConstituent.Replace(""+"","").Trim()); //what?
                    }
                    break;
                case "ElevationGrid": ElevationGrid = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid;
                    break;
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            string lFilename;
            lFilename = D4EM.Data.National.get_ShapeFilename(D4EM.Data.National.LayerSpecifications.huc8);
            if (System.IO.File.Exists(lFilename)) sb.AppendLine("NationalHuc8," + lFilename);
            lFilename = D4EM.Data.National.get_ShapeFilename(D4EM.Data.National.LayerSpecifications.county);
            if (System.IO.File.Exists(lFilename)) sb.AppendLine("NationalCounty," + lFilename);
            lFilename = D4EM.Data.National.get_ShapeFilename(D4EM.Data.National.LayerSpecifications.state);
            if (System.IO.File.Exists(lFilename)) sb.AppendLine("NationalState," + lFilename);

            sb.AppendLine("ProjectsPath," + ProjectsPath);
            sb.AppendLine("ProjectFileFullPath," + ProjectFileFullPath);
            sb.AppendLine("SoilSource," + SoilSource);
            sb.AppendLine("MinimumStreamLength," + MinFlowlineKM);
            sb.AppendLine("MinimumCatchmentArea," + MinCatchmentKM2);
            sb.AppendLine("MinimumLandUsePercent," + LandUseIgnoreBelowFraction * 100);
            sb.AppendLine("SimulationStartYear," + SimulationStartYear);
            sb.AppendLine("SimulationEndYear," + SimulationEndYear);
            //sb.AppendLine("SetupHSPF," + SetupHSPF);
            sb.AppendLine("HspfSnowOption," + HspfSnowOption);
            sb.AppendLine("HspfBacterialOption," + HspfBacterialOption);
            sb.AppendLine("HspfOutputInterval," + HspfOutputInterval); //[Enum].GetName(HspfOutputInterval.GetType, HspfOutputInterval)
            //AppendList("NASSYears", NASSYears, sb);
            //sb.AppendLine("NASSStatistics," + NASSStatistics);
            //("BasinsMetConstituents", BasinsMetConstituents, sb);
            BasinsMetConstituents.ForEach(item => sb.AppendLine("BasinsMetConstituents," + item)); //???
            //AppendList("NCDCconstituents", NCDCconstituents, sb)
            //AppendList("SelectedKeys", SelectedKeys, sb)
            SelectedKeys.ForEach(item => sb.AppendLine("SelectedKeys," + item));
            //AppendList("Catchments", Catchments, sb)
            Catchments.ForEach(item => sb.AppendLine("Catchments," + item));
            sb.AppendLine("CatchmentsMethod," + CatchmentsMethod);
            sb.AppendLine("ElevationGrid," + ElevationGrid.Name);
            if (!string.IsNullOrEmpty(SelectionLayer))
            {
                sb.AppendLine("SelectionLayer," + SelectionLayer);
            }

            return sb.ToString();
        }

        public void WriteParametersTextFile(string aFilename)
        {
            System.IO.File.WriteAllText(aFilename, ToString());
            System.IO.File.WriteAllText(System.IO.Path.ChangeExtension(aFilename, ".xml"), XML());
        }

        //Private Sub AppendList(ByVal VariableName As String, ByVal Values As Generic.List(Of String), ByVal sb As System.Text.StringBuilder)
        //    If Values IsNot Nothing AndAlso Values.Count > 0 Then
        //        sb.AppendLine(VariableName + "," + String.Join(" ", Values.ToArray))
        //    End If
        //End Sub
    }
}
