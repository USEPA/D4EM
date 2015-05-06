using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Data;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using DotSpatial.Data;
using DotSpatial.Topology;

namespace D4EM.Model.FAMoS
{
    public class FAMoSTool
    {
        //public FAMoSTool()
        //{

        //}

        public static void BuildFAMoSInput(D4EM.Data.Project aProject,
                                    D4EM.Data.Layer aCatchmentsLayer,
                                    D4EM.Data.Layer aFlowlinesLayer)
        {
            string pInputPath = aProject.ProjectFolder;
            string pOutputPath = aProject.ProjectFolder;
            string pSubbasinLayerName = aCatchmentsLayer.FileName;
            string pStreamLayerName = aFlowlinesLayer.FileName;


            string loc = Assembly.GetExecutingAssembly().Location;
            string dir = Path.GetDirectoryName(loc);
            string WSAEcoRegFile = Path.Combine(dir, "FAMoS_Support", "WSAEcoRegions.shp");

            if (File.Exists(WSAEcoRegFile) == false)
                throw new Exception("Unable to find the FAMoS WSAEcoRegion shape file.");

            FeatureSet fsWSAEcoReg = FeatureSet.OpenFile(WSAEcoRegFile) as FeatureSet;
            if (fsWSAEcoReg == null)
                throw new Exception("Unable to load the FAMoS WSAEcoRegion shape file.");
            
            FeatureSet fsFlowlines = aFlowlinesLayer.DataSet as FeatureSet;
            if (fsFlowlines == null)
                throw new Exception("Unable to find the flowlines data.");

            FeatureSet fsCatchments = aCatchmentsLayer.DataSet as FeatureSet;
            if (fsCatchments == null)
                throw new Exception("Unable to find the catchments data.");

            SegmentCollection segCollection = new SegmentCollection();
            
            //Loop over the flowines
            foreach (IFeature ftrFlowline in fsFlowlines.Features)
            {
                //Loop over the eco regions
                foreach (IFeature ftrRegion in fsWSAEcoReg.Features)
                {
                    if (ftrRegion.Intersects(ftrFlowline))
                    {
                        //COMID ID of the flowline we are working with
                        int comID = Convert.ToInt32(ftrFlowline.DataRow["COMID"].ToString());
                        string sComID = comID.ToString("D8");
                        string region = ftrRegion.DataRow["Region"].ToString();
                        string huc8 = ftrFlowline.DataRow["REACHCODE"].ToString();
                        huc8 = huc8.Substring(0, 8);
                        //Gather up the variables needed for stream width regression

                        //Cumulative drainage area in square km
                        double dCumDrng = Convert.ToDouble(ftrFlowline.DataRow["CUMDRAINAG"].ToString());
                        double dSlope = Convert.ToDouble(ftrFlowline.DataRow["SLOPE"].ToString());
                        
                        //Elevation in meters
                        double dMaxElev = Convert.ToDouble(ftrFlowline.DataRow["MAXELEVSMO"].ToString());
                        double dMinElev = Convert.ToDouble(ftrFlowline.DataRow["MINELEVSMO"].ToString());

                        //Get the record corresponding to the flowline comid
                        DataRow[] dr = fsCatchments.DataTable.Select("COMID = " + sComID);
                        if (dr == null || dr.Length < 1)
                            throw new Exception("Could not find the corresponding COMID in the catchment layer: " + comID.ToString("D8"));


                        double dPrecip = Convert.ToDouble(dr[0]["PRECIP"].ToString());

                        string WSARegion = ftrRegion.DataRow["Region"].ToString();

                        Segment segment = new Segment(sComID);
                        segment.WSAEcoRegion = WSARegion;
                        segment.HUC8 = huc8;
                        segment.DrainageArea = dCumDrng;
                        segment.MaxElevRaw = dMaxElev;
                        segment.MinElevRaw = dMinElev;
                        segment.Precip = dPrecip;
                        segment.Slope = dSlope;

                        segCollection.Segments.Add(sComID, segment);

                        break;
                    }

                }
            }

            string modelPath = Path.Combine(aProject.ProjectFolder, "FAMoS");
            if (!Directory.Exists(modelPath))
                Directory.CreateDirectory(modelPath);

            string filePath = Path.Combine(modelPath, "SegmentProperties.xml");

            XElement xElement = segCollection.ToXElement();
            xElement.Save(filePath);

            
        }
    }
}
