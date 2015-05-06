using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EMSystemTesting
{
    public class testWDNR
    {
        public bool testingWDNRStateWide(string aProjectFolder, string animal)
        {
            bool pass = false;
            string aProjectFolderWDNR = System.IO.Path.Combine(aProjectFolder, "WDNR", "StateWide", animal);
            string aCacheFolderWDNR = System.IO.Path.Combine(aProjectFolderWDNR, "Cache");
            D4EM.Data.LayerSpecification _animal = new D4EM.Data.LayerSpecification();
            switch (animal)
            {
                case "Beef":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                    break;
                case "Chickens":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                    break;
                case "Dairy":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                    break;
                case "Swine":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                    break;
                case "Turkeys":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                    break;
            }
            D4EM.Data.Source.WDNR wdnr = new D4EM.Data.Source.WDNR();
            try
            {
                wdnr.getData(_animal, aProjectFolderWDNR, aCacheFolderWDNR);
            }
            catch (Exception ex)
            {
            }
            if (Directory.Exists(aProjectFolderWDNR))
            {
                string[] filesinDirectory = Directory.GetFiles(aProjectFolderWDNR, "", SearchOption.AllDirectories);
                int numFiles = filesinDirectory.Length;
                string[] subdirectoryEntries = Directory.GetDirectories(aProjectFolderWDNR);

                foreach (string subdirectory in subdirectoryEntries)
                {
                    filesinDirectory = Directory.GetFiles(subdirectory);
                    numFiles = numFiles + filesinDirectory.Length;
                    subdirectoryEntries = Directory.GetDirectories(subdirectory);
                    foreach (string subDirectory in subdirectoryEntries)
                    {
                        filesinDirectory = Directory.GetFiles(subDirectory);
                        numFiles = numFiles + filesinDirectory.Length;
                    }
                }
                if (numFiles >= 1)
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                }
            }   
            return pass;                
        }

        public bool testingWDNRBoundingBox(string aProjectFolder, string animal, double north, double south, double east, double west)
        {
            bool pass = false;
            string aProjectFolderWDNR = System.IO.Path.Combine(aProjectFolder, "WDNR", "StateWide", animal);
            string aCacheFolderWDNR = System.IO.Path.Combine(aProjectFolderWDNR, "Cache");
            D4EM.Data.LayerSpecification _animal = new D4EM.Data.LayerSpecification();
            switch (animal)
            {
                case "Beef":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                    break;
                case "Chickens":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                    break;
                case "Dairy":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                    break;
                case "Swine":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                    break;
                case "Turkeys":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                    break;
            }
            D4EM.Data.Source.WDNR wdnr = new D4EM.Data.Source.WDNR();
            try
            {
                wdnr.getDataWithinBoundingBox(_animal, aProjectFolderWDNR, aCacheFolderWDNR, north, south, east, west);
            }
            catch (Exception ex)
            {
            }
            if (Directory.Exists(aProjectFolderWDNR))
            {
                string[] filesinDirectory = Directory.GetFiles(aProjectFolderWDNR, "", SearchOption.AllDirectories);
                int numFiles = filesinDirectory.Length;
                string[] subdirectoryEntries = Directory.GetDirectories(aProjectFolderWDNR);

                foreach (string subdirectory in subdirectoryEntries)
                {
                    filesinDirectory = Directory.GetFiles(subdirectory);
                    numFiles = numFiles + filesinDirectory.Length;
                    subdirectoryEntries = Directory.GetDirectories(subdirectory);
                    foreach (string subDirectory in subdirectoryEntries)
                    {
                        filesinDirectory = Directory.GetFiles(subDirectory);
                        numFiles = numFiles + filesinDirectory.Length;
                    }
                }
                if (numFiles >= 1)
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                }
            }
            return pass;
        }
        public bool testingWDNRHuc8(string aProjectFolder, string animal, string aHuc)
        {
            bool pass = false;
            string aProjectFolderWDNR = System.IO.Path.Combine(aProjectFolder, "WDNR", "StateWide", animal);
            string aCacheFolderWDNR = System.IO.Path.Combine(aProjectFolderWDNR, "Cache");
            D4EM.Data.LayerSpecification _animal = new D4EM.Data.LayerSpecification();
            switch (animal)
            {
                case "Beef":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                    break;
                case "Chickens":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                    break;
                case "Dairy":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                    break;
                case "Swine":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                    break;
                case "Turkeys":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                    break;
            }
            D4EM.Data.Source.WDNR wdnr = new D4EM.Data.Source.WDNR();
            try
            {
                wdnr.getDataWithinHuc8(_animal, aProjectFolderWDNR, aCacheFolderWDNR, aHuc);
            }
            catch (Exception ex)
            {
            }
            if (Directory.Exists(aProjectFolderWDNR))
            {
                string[] filesinDirectory = Directory.GetFiles(aProjectFolderWDNR, "", SearchOption.AllDirectories);
                int numFiles = filesinDirectory.Length;
                string[] subdirectoryEntries = Directory.GetDirectories(aProjectFolderWDNR);

                foreach (string subdirectory in subdirectoryEntries)
                {
                    filesinDirectory = Directory.GetFiles(subdirectory);
                    numFiles = numFiles + filesinDirectory.Length;
                    subdirectoryEntries = Directory.GetDirectories(subdirectory);
                    foreach (string subDirectory in subdirectoryEntries)
                    {
                        filesinDirectory = Directory.GetFiles(subDirectory);
                        numFiles = numFiles + filesinDirectory.Length;
                    }
                }
                if (numFiles >= 1)
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                }
            }
            return pass;
        }
        public bool testingWDNRHuc12(string aProjectFolder, string animal, string aHuc12)
        {
            bool pass = false;
            string aProjectFolderWDNR = System.IO.Path.Combine(aProjectFolder, "WDNR", "StateWide", animal);
            string aCacheFolderWDNR = System.IO.Path.Combine(aProjectFolderWDNR, "Cache");
            D4EM.Data.LayerSpecification _animal = new D4EM.Data.LayerSpecification();
            switch (animal)
            {
                case "Beef":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                    break;
                case "Chickens":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                    break;
                case "Dairy":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                    break;
                case "Swine":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                    break;
                case "Turkeys":
                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                    break;
            }  
            D4EM.Data.Source.WDNR wdnr = new D4EM.Data.Source.WDNR();
            try
            {
                wdnr.getDataWithinHuc12(_animal, aProjectFolderWDNR, aCacheFolderWDNR, aHuc12);
            }
            catch (Exception ex)
            {
            }
            if (Directory.Exists(aProjectFolderWDNR))
            {
                string[] filesinDirectory = Directory.GetFiles(aProjectFolderWDNR, "", SearchOption.AllDirectories);
                int numFiles = filesinDirectory.Length;
                string[] subdirectoryEntries = Directory.GetDirectories(aProjectFolderWDNR);

                foreach (string subdirectory in subdirectoryEntries)
                {
                    filesinDirectory = Directory.GetFiles(subdirectory);
                    numFiles = numFiles + filesinDirectory.Length;
                    subdirectoryEntries = Directory.GetDirectories(subdirectory);
                    foreach (string subDirectory in subdirectoryEntries)
                    {
                        filesinDirectory = Directory.GetFiles(subDirectory);
                        numFiles = numFiles + filesinDirectory.Length;
                    }
                }
                if (numFiles >= 1)
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                }
            }
            return pass;
        }
    }
}
