using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EPAUtility
{
    public class CheckIfFileIsInUse
    {
        public CheckIfFileIsInUse()
        {
        }

        public static bool IsFileLocked(string fileName) 
        {
            FileInfo file = new FileInfo(fileName);
            FileStream stream = null;      
            try     
            {         
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);     
            }    
            catch (IOException)     
            {         
                //the file is unavailable because it is:         
                //still being written to         
                //or being processed by another thread         
                //or does not exist (has already been processed)         
                return true;     
            }     
            finally     
            {        
                if (stream != null)             
                    stream.Close();     
            }      
            //file is not locked     
            return false; } 
    }
}
