using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D4EM.Data.Source;
using System.Collections;
using System.IO;
using System.IO.Compression;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using System.Data;
using DotSpatial.Analysis;
using System.Drawing;
using D4EM.Data.Source.NRCS_Soil;

namespace EPAUtility
{
    public class NRCS_SoilFileSupport
    {
        private List<D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil> _soils = new List<D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil>();
        private string _aProjectFolderSoils;
        private string _aSubFolder;
        private string _aCSVfile;
        string _metaDataFile;
        private string _latitude;
        private string _longitude;
        private string _shapefileName;
        private string _aCacheFolder;
        private string _radiusInitial;
        private string _radiusIncrement;
        private string _radiusMax;
        public List<string> FileNames = new List<string>();

        public NRCS_SoilFileSupport(List<D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil> soils, string aProjectFolderSoils, string aCacheFolder, string aSubFolder, double aLatitude, double aLongitude, double aRadiusInitial, double aRadiusMax, double aRadiusIncrement)
        {
            _soils = soils;
            _aProjectFolderSoils = aProjectFolderSoils;
            _aSubFolder = aSubFolder;
            _aCacheFolder = aCacheFolder;            
            _latitude = aLatitude.ToString();
            _longitude = aLongitude.ToString();
            _radiusIncrement = aRadiusIncrement.ToString();
            _radiusInitial = aRadiusInitial.ToString();
            _radiusMax = aRadiusMax.ToString();
            _aCSVfile = System.IO.Path.Combine(aSubFolder, "Lat" + _latitude + "Lng" + _longitude + "(" + _radiusInitial + _radiusMax + _radiusIncrement + ").csv");
            _shapefileName = System.IO.Path.Combine(_aSubFolder, "Soils-" + _latitude + ";" + _longitude + "(" + _radiusInitial + _radiusMax + _radiusIncrement + ").shp");
            _metaDataFile = System.IO.Path.Combine(aSubFolder, "Metadata_" + Path.GetFileNameWithoutExtension(_aCSVfile) + ".txt");
        }

        public void WriteSoilFiles()
        {
            FileNames.Clear();
            string csvFileCache = System.IO.Path.Combine(_aCacheFolder, Path.GetFileName(_aCSVfile));
            string shapeFileCache = System.IO.Path.Combine(_aCacheFolder, Path.GetFileName(_shapefileName));
            string metadataFileCache = System.IO.Path.Combine(_aCacheFolder, "Metadata_" + Path.GetFileNameWithoutExtension(csvFileCache) + ".txt");
            bool csvFileExists = checkCache(csvFileCache);
            bool shapeFileExists = checkCache(shapeFileCache);
            bool metadataFileExists = checkCache(metadataFileCache);
            Directory.CreateDirectory(_aSubFolder);
            Directory.CreateDirectory(_aCacheFolder);

            string dbfcache = System.IO.Path.Combine(_aCacheFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".dbf");
            string prjcache = System.IO.Path.Combine(_aCacheFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".prj");
            string shxcache = System.IO.Path.Combine(_aCacheFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".shx");
            string dbf = System.IO.Path.Combine(_aSubFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".dbf");
            string prj = System.IO.Path.Combine(_aSubFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".prj");
            string shx = System.IO.Path.Combine(_aSubFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".shx");
            if ((csvFileExists == false) || (shapeFileExists == false) || (metadataFileExists == false))
            {
                WriteFiles(csvFileCache, shapeFileCache, metadataFileCache);
                if (File.Exists(_aCSVfile) == false)
                {
                    File.Copy(csvFileCache, _aCSVfile);
                }
                FileNames.Add(_aCSVfile);
                if (File.Exists(_shapefileName) == false)
                {
                    File.Copy(shapeFileCache, _shapefileName);
                    File.Copy(dbfcache, dbf);
                    File.Copy(prjcache, prj);
                    File.Copy(shxcache, shx);
                }
                if (File.Exists(_metaDataFile) == false)
                {
                    File.Copy(metadataFileCache, _metaDataFile);
                }
                FileNames.Add(_shapefileName);
                FileNames.Add(_metaDataFile);
            }
            else
            {
                if (File.Exists(_aCSVfile) == false)
                {
                    File.Copy(csvFileCache, _aCSVfile);
                }
                FileNames.Add(_aCSVfile);
                if (File.Exists(_shapefileName) == false)
                {
                    File.Copy(shapeFileCache, _shapefileName);
                    File.Copy(dbfcache, dbf);
                    File.Copy(prjcache, prj);
                    File.Copy(shxcache, shx);
                }
                if (File.Exists(_metaDataFile) == false)
                {
                    File.Copy(metadataFileCache, _metaDataFile);
                }
                FileNames.Add(_shapefileName);
                FileNames.Add(_metaDataFile);
            } 
        }

        private void WriteFiles(string csvfile, string shapefile, string metadataFile)
        {
            Directory.CreateDirectory(_aSubFolder);

            StreamWriter sw = new StreamWriter(csvfile);
            sw.WriteLine("Key,Symbol,Area,HSG");

            foreach (D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil soil in _soils)
            {
                string area = soil.AreaSymbol;
                string hsg = soil.HSG;
                string key = soil.MuKey;
                string symbol = soil.MuSym;

                sw.WriteLine(key + "," + symbol + "," + area + "," + hsg);
            }
            D4EM.Data.Source.NRCS_Soil.SoilLocation.SaveShapefile(_soils, shapefile, KnownCoordinateSystems.Geographic.World.WGS1984);
            sw.Close();

            writeMetaDataFile(metadataFile);
        }

        private static bool checkCache(string aSaveAsCache)
        {
            bool fileExists = false;

            if (File.Exists(aSaveAsCache))
            {
                fileExists = true;
            }

            return fileExists;
        }

        private void writeMetaDataFile(string metadataFileName)
        {
            TextWriter tw = new StreamWriter(metadataFileName);

            tw.WriteLine("Download Time = " + DateTime.Now);
            tw.WriteLine("");
            tw.WriteLine("Area of Interest:");
            tw.WriteLine("Latitude: " + _latitude);
            tw.WriteLine("Longitude: " + _longitude);
            tw.WriteLine("Radius Initial: " + _radiusInitial);
            tw.WriteLine("Radius Max: " + _radiusMax);
            tw.WriteLine("Radius Increment: " + _radiusIncrement);
            tw.WriteLine("");
            tw.WriteLine("NRCS-Soil data was written into files:");
            tw.WriteLine("CSV File: " + _aCSVfile);
            tw.WriteLine("Shapefile: " + _shapefileName);

            tw.Close();
        }
    }
}
