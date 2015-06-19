using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Net;
using Ionic;
using D4EM.Data;

namespace D4EM.Data.Source
{
    public class NatureServe
    {
        public NatureServe()
        {
        }

        public class LayerSpecifications
        {
            public class Birds
            {
                public static LayerSpecification Accipitridae = new LayerSpecification(
                Tag: "Accipitridae", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(NatureServe));
            }

            public static LayerSpecification Calypte_anna = new LayerSpecification(
                Tag: "Calypte_anna", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(NatureServe));

            public static LayerSpecification Papilio_glaucus = new LayerSpecification(
                Tag: "Papilio_glaucus", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(NatureServe));

            public static LayerSpecification Lintneria_eremitus = new LayerSpecification(
                Tag: "Lintneria_eremitus", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(NatureServe));

            public static LayerSpecification Bombus_affinis = new LayerSpecification(
                Tag: "Bombus_affinis", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(NatureServe));

            public static LayerSpecification Habropoda_laboriosa = new LayerSpecification(
                Tag: "Habropoda_laboriosa", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(NatureServe));
        }

        public static bool getData(string aProjectFolder, string aCacheFolder, D4EM.Data.LayerSpecification pollinatorType)
        {
            string zipFileNameDownload = "";
            string htmFileNameDownload = "";           
            string _pollinator = pollinatorType.Tag;
            bool filesExist = false;
            switch (_pollinator)
            {
                case "Calypte_anna":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/calypte_anna_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Anna's_Hummingbird_(Calypte_anna).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, "Nature Serve Annas Hummingbird (Calypte anna)", aProjectFolder, aCacheFolder);
                    break;
                case "Papilio_glaucus":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/papilio_glaucus_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Eastern_Tiger_Swallowtail_(Papilio_glaucus).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, "Nature Serve Eastern Tiger Swallowtail (Papilio glaucus)", aProjectFolder, aCacheFolder);
                    break;
                case "Lintneria_eremitus":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/Lintneria_eremitus_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Hermit_Sphinx_(Lintneria_eremitus).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, "Nature Serve Hermit Sphinx (Lintneria eremitus)", aProjectFolder, aCacheFolder);
                    break;
                case "Bombus_affinis":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/Bombus_affinis_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Rusty-patched_bumblebee_(Bombus_affinis).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, "Nature Serve Rusty-patched Bumble Bee (Bombus affinis)", aProjectFolder, aCacheFolder);
                    break;
                case "Habropoda_laboriosa":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/Habropoda_laboriosa_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Southeastern_Blueberry_Bee_(Habropoda_laboriosa).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, "Nature Serve Southeastern Blueberry Bee (Habropoda laboriosa)", aProjectFolder, aCacheFolder);
                    break;
            }
            return filesExist;
        }

        public static bool getPollinatorDataForMap(string aProjectFolder, string aCacheFolder, D4EM.Data.LayerSpecification pollinatorType, TextWriter fileShp)
        {
            bool filesExist = false;
            string zipFileNameDownload = "";
            string htmFileNameDownload = "";
            string _pollinator = pollinatorType.Tag;
            switch (_pollinator)
            {
                case "Calypte_anna":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/calypte_anna_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Southeastern_Blueberry_Bee_(Habropoda_laboriosa).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, _pollinator, aProjectFolder, aCacheFolder);
                    WriteFilePaths(aProjectFolder, _pollinator, fileShp);
                    WritePrjFile(aProjectFolder, _pollinator);
                    break;
                case "Papilio_glaucus":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/papilio_glaucus_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Eastern_Tiger_Swallowtail_(Papilio_glaucus).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, _pollinator, aProjectFolder, aCacheFolder);
                    WriteFilePaths(aProjectFolder, _pollinator, fileShp);
                    WritePrjFile(aProjectFolder, _pollinator);
                    break;
                case "Lintneria_eremitus":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/Lintneria_eremitus_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Hermit_Sphinx_(Lintneria_eremitus).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, _pollinator, aProjectFolder, aCacheFolder);
                    WriteFilePaths(aProjectFolder, _pollinator, fileShp);
                    WritePrjFile(aProjectFolder, _pollinator);
                    break;
                case "Bombus_affinis":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/Bombus_affinis_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Rusty-patched_bumblebee_(Bombus_affinis).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, _pollinator, aProjectFolder, aCacheFolder);
                    WriteFilePaths(aProjectFolder, _pollinator, fileShp);
                    WritePrjFile(aProjectFolder, _pollinator);
                    break;
                case "Habropoda_laboriosa":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/pollinatorData/Habropoda_laboriosa_shapefiles.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/metadata/Pollinators/Metadata_Southeastern_Blueberry_Bee_(Habropoda_laboriosa).htm";
                    filesExist = DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, _pollinator, aProjectFolder, aCacheFolder);
                    WriteFilePaths(aProjectFolder, _pollinator, fileShp);
                    WritePrjFile(aProjectFolder, _pollinator);
                    break;
            }
            return filesExist;
        }

        public static void getBirdDataForMap(string aProjectFolder, string aCacheFolder, D4EM.Data.LayerSpecification birdType, TextWriter fileShp)
        {
            string zipFileNameDownload = "";
            string htmFileNameDownload = "";
            string _bird = birdType.Tag;
            switch (_bird)
            {
                case "Accipitridae":
                    zipFileNameDownload = @"http://www.natureserve.org/getData/dataSets/birdMapData/Accipitridae.zip";
                    htmFileNameDownload = @"http://www.natureserve.org/getData/Metadata_Birds_ver_3.0_Oct_07.pdf";
                    DownloadOrCheckCacheThenUnzip(zipFileNameDownload, htmFileNameDownload, _bird, aProjectFolder, aCacheFolder);
                    WriteFilePaths(aProjectFolder, _bird, fileShp);
                    WritePrjFile(aProjectFolder, _bird);
                    break;
                
            }
        }

        private static void WritePrjFile(string aProjectFolder, string pollinator)
        {
            string subFolder = System.IO.Path.Combine(aProjectFolder, pollinator);
            string[] files = Directory.GetFiles(subFolder, "*.shp", SearchOption.TopDirectoryOnly);
            int i = 0;
            while (i < files.Length)
            {
                string _file = files[i];
                if (File.Exists(_file) == true)
                {
                    string name = Path.GetFileNameWithoutExtension(_file);
                    string prjFile = System.IO.Path.Combine(subFolder, name + ".prj");
                    TextWriter prj = new StreamWriter(prjFile);
                    prj.WriteLine(@"GEOGCS[""GCS_WGS_1984"",DATUM[""D_WGS_1984"",SPHEROID[""WGS_1984"",6378137.0,298.257223563]],PRIMEM[""Greenwich"",0.0],UNIT[""Degree"",0.0174532925199433]]");
                    prj.Close();

                }
                i++;
            }
        }
        
        private static void WriteFilePaths(string aProjectFolder, string layer, TextWriter fileShp) 
        {
           // TextWriter fileShp = new StreamWriter(@"C:\Temp\DownloadedFilePathNatureServe");
            string aSubFolder = System.IO.Path.Combine(aProjectFolder, layer);
            string filePath = System.IO.Path.Combine(aProjectFolder, aSubFolder);
            if (Directory.Exists(filePath))
            {
                string[] shp = Directory.GetFiles(filePath, "*.shp", SearchOption.TopDirectoryOnly);
                int i = 0;
                while (i < shp.Length)
                {
                    if (File.Exists(shp[i]))
                    {
                        fileShp.WriteLine(shp[i]);
                    }
                    i++;
                }
            }
            
        }

        private static bool DownloadOrCheckCacheThenUnzip(string zipFileDownloadString, string htmFileDownloadString, string layer, string aProjectFolder, string aCacheFolder)
        {
            bool fileExists = false;
            WebClient client = new WebClient();
            client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0)" +
                " (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            string aProjectFolderNatureServe = aProjectFolder;
           
            Directory.CreateDirectory(aProjectFolderNatureServe);
            Directory.CreateDirectory(aCacheFolder);
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, layer);
            Directory.CreateDirectory(aSubFolder);
            string aSaveAsHtm = System.IO.Path.Combine(aSubFolder, layer + ".htm");
            string aSaveAsZip = System.IO.Path.Combine(aCacheFolder, layer + ".zip");
            if (File.Exists(aSaveAsZip) == true)
            {
                client.DownloadFile(htmFileDownloadString, aSaveAsHtm);
                using (var zf = Ionic.Zip.ZipFile.Read(aSaveAsZip))
                {
                    zf.ToList().ForEach(entry =>
                    {
                        if ((System.IO.Path.GetFileName(entry.FileName) != null) && (System.IO.Path.GetFileName(entry.FileName) != ""))
                        {
                            entry.FileName = System.IO.Path.GetFileName(entry.FileName);
                            entry.Extract(aSubFolder, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);
                            fileExists = true;
                        }
                    });
                }
            }
            else
            {
                if (Directory.Exists(aSubFolder) == true)
                {
                    client.DownloadFile(zipFileDownloadString, aSaveAsZip);
                    client.DownloadFile(htmFileDownloadString, aSaveAsHtm);
                    using (var zf = Ionic.Zip.ZipFile.Read(aSaveAsZip))
                    {
                        zf.ToList().ForEach(entry =>
                        {
                            if ((System.IO.Path.GetFileName(entry.FileName) != null) && (System.IO.Path.GetFileName(entry.FileName) != ""))
                            {
                                entry.FileName = System.IO.Path.GetFileName(entry.FileName);
                                entry.Extract(aSubFolder, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);
                                fileExists = true;
                            }
                        });
                    }
                }
            }
            return fileExists;

        }

    }
}