using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace D4EM.Model.FAMoS
{
    /// <summary>
    /// This class is a wrapper for a NHD+ Flowline Segment - maps to a Segment node in the XML
    /// </summary>
    public class Segment
    {        

        public string SegmentID { get; set; }

        public string HUC8 { get; set; }
        public string WSAEcoRegion { get; set; }
        public double DrainageArea { get; set; }
        public double Slope { get; set; }
        public double MaxElevRaw { get; set; }
        public double MinElevRaw { get; set; }
        public double Precip { get; set; }

        public Segment(string segmentID)
        {
            SegmentID = segmentID;                            
        }

        /// <summary>
        /// Get a new segment from an element
        /// </summary>
        /// <param name="xElement"></param>
        public Segment(XElement xElement)
        {
            try
            {
                if (xElement == null)
                    return;

                XAttribute xAtt = xElement.Attribute("id");
                if (xAtt == null)
                    return;

                SegmentID = xAtt.Value;

                XElement xEcoRegion = xElement.Element("WSAEcoRegion");
                if (xEcoRegion != null)
                    WSAEcoRegion = xEcoRegion.Value;

                XElement xHUC8 = xElement.Element("HUC8");
                if (xHUC8 != null)
                    HUC8 = xHUC8.Value;

                XElement xDrainageArea = xElement.Element("DrainageArea");
                if (xDrainageArea != null)
                    DrainageArea  = Convert.ToDouble(xDrainageArea.Value);

                XElement xMaxElevRaw = xElement.Element("MaxElevRaw");
                if (xMaxElevRaw != null)
                    MaxElevRaw = Convert.ToDouble(xMaxElevRaw.Value);

                XElement xMinElevRaw = xElement.Element("MinElevRaw");
                if (xMinElevRaw != null)
                    MinElevRaw = Convert.ToDouble(xMinElevRaw.Value);

                XElement xSlope = xElement.Element("Slope");
                if (xSlope != null)
                    Slope = Convert.ToDouble(xSlope.Value);

                XElement xPrecip = xElement.Element("Precip");
                if (xPrecip  != null)
                    Precip = Convert.ToDouble(xPrecip.Value);

                //var fish = from x in xElement.Element("FishAssemblage").Elements()
                //               select x;                

            }
            catch (Exception ex)
            {
                //Not sure what to do here
            }
            
        }

        public XElement ToElement()
        {            
            XElement xElement = new XElement ("Segment", new XAttribute("id",SegmentID));

            XElement xRegion = new XElement("WSAEcoRegion", WSAEcoRegion);
            XElement xHUC8 = new XElement("HUC8", HUC8);
            XElement xDrainageArea = new XElement("DrainageArea", DrainageArea);
            XElement xSlope = new XElement("Slope", Slope);
            XElement xMaxElevRaw = new XElement("MaxElevRaw", MaxElevRaw);
            XElement xMinElevRaw = new XElement("MinElevRaw", MinElevRaw);
            XElement xPrecip = new XElement("Precip", Precip);            

            xElement.Add(xRegion);
            xElement.Add(xHUC8);
            xElement.Add(xDrainageArea);
            xElement.Add(xSlope);
            xElement.Add(xMaxElevRaw);
            xElement.Add(xMinElevRaw);
            xElement.Add(xPrecip);
            
            return xElement;

        }
    }

    public class SegmentCollection
    {
        private Dictionary<string, Segment> _dctSegments;
        public Dictionary<string,Segment> Segments
        {
            get { return _dctSegments; }
        }

        public SegmentCollection()
        {
            //_dctSegments = new Dictionary<string, Segment>();            
        }

        public SegmentCollection(XElement xElement)
        {
            _dctSegments = new Dictionary<string, Segment>();

            foreach (XElement xElmt in xElement.Descendants("Segment"))
            {
                Segment sa = new Segment(xElmt);                
                _dctSegments.Add(sa.SegmentID, sa);
            }
        }

        public XElement ToXElement()
        {
            XElement xElement = new XElement("Segment");

            foreach (Segment sa in _dctSegments.Values)
                xElement.Add(sa.ToElement());            

            return xElement;
        }

    }

}
