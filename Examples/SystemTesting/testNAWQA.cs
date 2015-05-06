using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace D4EMSystemTesting
{
    public class testNAWQA
    {
        public bool testingNAWQA(string aProjectFolder, bool doLatLong, bool doAvgStdDev)
        {
            bool pass = false;
            string aProjectFolderNAWQA = System.IO.Path.Combine(aProjectFolder, "NAWQA");
            string aCacheFolderNAWQA = System.IO.Path.Combine(aProjectFolderNAWQA, "Cache");
            string waterType = "surfacegroundwater";
            string fileType = "csv";
            string queryType = "serial";
            string state = "GEORGIA";
            string[] counties = new string[1];
            counties[0] = "DEKALB";
            string[] parameters = new string[10];
            parameters[0] = "00010  - Temperature_ water_ degrees Celsius";
            parameters[1] = "90400 - pH_ water_ area weighted average_ standard units";
            parameters[2] = "00400 - pH_ water_ unfiltered_ field_ standard units";
            parameters[3] = "00403 - pH_ water_ unfiltered_ laboratory_ standard units";
            parameters[4] = "00680 - Organic carbon_ water_ unfiltered_ milligrams per liter";
            parameters[5] = "00681 - Organic carbon_ water_ filtered_ milligrams per liter";
            parameters[6] = "01047 - Iron(II)_ water_ filtered_ micrograms per liter";
            parameters[7] = "99032 - Iron(II)_ water_ unfiltered_ micrograms per liter";
            parameters[8] = "99114 - Iron(II)_ water_ filtered_ field_ milligrams per liter";
            parameters[9] = "99128 - Iron(II)_ water_ unfiltered_ field_ milligrams per liter";
            string startYear = "2000";
            string endYear = "2001";
            double lat = 33.815666;
            double lng = -84.223938;
            string aSaveFolder = "GEORGIA(DEKALB) GW&SW";
            if (doAvgStdDev == false)
            {
                if (doLatLong == false)
                {
                    try
                    {
                        D4EM.Data.Source.NAWQA nawqa = new D4EM.Data.Source.NAWQA(aProjectFolderNAWQA, aCacheFolderNAWQA, waterType, fileType, queryType);
                        nawqa.getAllDataStateCounties(state, counties, parameters, startYear, endYear);
                    }
                    catch (Exception ex)
                    {
                    }
                    string aSubFolder = System.IO.Path.Combine(aProjectFolderNAWQA, aSaveFolder);
                    if (Directory.Exists(aSubFolder))
                    {
                        string[] filesinDirectory = Directory.GetFiles(aSubFolder);
                        int numFiles = filesinDirectory.Length;
                        if (numFiles == 11)
                        {
                            pass = true;
                        }
                        else
                        {
                            pass = false;
                        }
                    }

                }
                else
                {
                    try
                    {
                        D4EM.Data.Source.NAWQA nawqa = new D4EM.Data.Source.NAWQA(aProjectFolderNAWQA, aCacheFolderNAWQA, waterType, fileType, queryType);
                        nawqa.getAllDataLatLong(lat, lng, parameters, startYear, endYear);
                    }
                    catch (Exception ex)
                    {
                    }
                    string aSubFolder = System.IO.Path.Combine(aProjectFolderNAWQA, aSaveFolder);
                    if (Directory.Exists(aSubFolder))
                    {
                        string[] filesinDirectory = Directory.GetFiles(aSubFolder);
                        int numFiles = filesinDirectory.Length;
                        if (numFiles == 11)
                        {
                            pass = true;
                        }
                        else
                        {
                            pass = false;
                        }
                    }
                }
            }
            else
            {
                if (doLatLong == false)
                {
                    try
                    {
                        D4EM.Data.Source.NAWQA nawqa = new D4EM.Data.Source.NAWQA(aProjectFolderNAWQA, aCacheFolderNAWQA, waterType, fileType, queryType);
                        nawqa.getAverageAndStandardDeviationStateCounties(state, counties, parameters, startYear, endYear);
                    }
                    catch (Exception ex)
                    {
                    }
                    string aSubFolder = System.IO.Path.Combine(aProjectFolderNAWQA, aSaveFolder);
                    if (Directory.Exists(aSubFolder))
                    {
                        if (File.Exists(Path.Combine(aSubFolder, "Averages&StdDev.txt")))
                        {
                            pass = true;
                            File.Delete(Path.Combine(aSubFolder, "Averages&StdDev.txt"));
                        }                        
                        else
                        {
                            pass = false;
                        }
                    }

                }
                else
                {
                    try
                    {
                        D4EM.Data.Source.NAWQA nawqa = new D4EM.Data.Source.NAWQA(aProjectFolderNAWQA, aCacheFolderNAWQA, waterType, fileType, queryType);
                        nawqa.getAverageAndStandardDeviationLatLong(lat, lng, parameters, startYear, endYear);
                    }
                    catch (Exception ex)
                    {
                    }
                    string aSubFolder = System.IO.Path.Combine(aProjectFolderNAWQA, aSaveFolder);
                    if (Directory.Exists(aSubFolder))
                    {
                        if (File.Exists(Path.Combine(aSubFolder, "Averages&StdDev.txt")))
                        {
                            pass = true;
                            File.Delete(Path.Combine(aSubFolder, "Averages&StdDev.txt"));
                        }
                        else
                        {
                            pass = false;
                        }
                    }
                }
            }
            return pass;
        }
    }
}
