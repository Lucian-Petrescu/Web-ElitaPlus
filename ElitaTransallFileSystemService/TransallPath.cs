using Assurant.ElitaPlus.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ElitaTransallFileSystemService
{

    public class TransallPath : IDisposable
    {
        private DirectoryInfo[] subDir;
        
        private string strError = string.Empty;
        private string strMessage = string.Empty;
        
        
        public string RootPath = string.Empty;
        public Dictionary<DirectoryInfo, string> UsableRegionInfo;
        public bool isInputDir;
        public string[] ValidExtensions;
        public string ValidExtensionString = string.Empty;
        public string CurrentEnvironment = string.Empty;
        public string CurrentUser = string.Empty;
        public string LogDB = string.Empty;
        public string GenericLogfolder = string.Empty;
        public string CurrentRegion = string.Empty;
        public string CurrentHub = string.Empty;
        public string Tempfolder = string.Empty;
        public string Processedfolder = string.Empty;
        public string Errorfolder = string.Empty;
        public int LogSwitch = 0;
        public StringBuilder FileLogger = new StringBuilder();
        public StringBuilder DBLogger = new StringBuilder(); 
        

        enum TransallMove // file movement
        {
            Root_Temp,
            Root_PreProcess_Error,
            Root_Error,
            Root_Output,
            Root_Processed, 
            Temp_Processed,
            Temp_Error,
            Temp_Output,
            Output_Processed,
            Output_Error
        }

        enum FileCollection // what type of files required for processing
        { 
            VaildExtensions,
            ExcludeValidExtensions,
            TRCFiles,
            PRCFiles,
            FilesToDelete
        }

        enum LogLocation // where to add the log
        {
            both = 0,
            database = 1,
            file = 2
        }

        enum LogType // what type of log 
        {
            Error = 0,
            Information = 1
        }


        #region "Constructor"

        public TransallPath(bool isInput)
        {
            isInputDir = isInput;
            PrepareUsableRegions();
        }

        #endregion 
       
        #region "Accessible Methods"

        public static string Key(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

       
        public void PickSourceFilesToProcess()
        {
            IEnumerable<System.IO.FileInfo> fileQuery = null;
            int intProcessed = 0;
            int intError = 0;
            bool blnRegion = false; 
            
            try
            {
                DBLogger.Length = 0;
                FileLogger.Length = 0;
                
                //for DB 
                strMessage = "[PickSourceFilesToProcess] - ";
                BuildMessage(String.Format(TransallMessages.PollingStart, strMessage, DateTime.Now.ToString()),false,false);

                foreach (DirectoryInfo dir in this.UsableRegionInfo.Keys)
                {
                    try
                    {
                        intProcessed = 0;
                        intError = 0;
                        this.CurrentRegion = dir.Name.ToUpper();
                        this.CurrentHub = Key(dir.Name.ToUpper());

                        BuildMessage(TransallMessages.PollingStart + " Before Login Environment - " + this.CurrentEnvironment + " Hub - " + this.CurrentHub + " Region - " + this.CurrentRegion + " User - " + this.CurrentUser, false, true);
                        blnRegion = RegionLogin(this.CurrentHub);
                        BuildMessage(TransallMessages.PollingStart + " After Login Environment - " + this.CurrentEnvironment + " Hub - " + this.CurrentHub + " Region - " + this.CurrentRegion + " User - " + this.CurrentUser, false, true);

                        fileQuery = GetRequiredFileList(dir, FileCollection.VaildExtensions);

                        foreach (FileInfo fi in fileQuery)
                        {
                            try
                            {
                                if (blnRegion && !IsFileLocked(fi) && PreProcessInputFile(fi))
                                {
                                    MoveFile(TransallMove.Root_Temp, fi, false);
                                    intProcessed += 1;
                                }
                                else if (!blnRegion)
                                {
                                    BuildMessage(string.Format(TransallMessages.RegionLoginError, this.CurrentUser, this.CurrentRegion, this.CurrentHub), true, false);
                                    MoveFile(TransallMove.Root_Error, fi, true);
                                }
                            }
                            catch (Exception ex) // in case of error, skip only that file and proceed with others 
                            {
                                BuildMessage(strMessage + " "  + ex.Message + " " + ex.StackTrace, true, true);
                                AddLog(true, true, null, LogType.Error); 
                            }                           
                        }
                         
                        if (intProcessed > 0)
                            BuildMessage(String.Format(TransallMessages.PickedForProcessing, strMessage, intProcessed.ToString(), this.CurrentRegion), false, false);

                        //check if there are any old .prc files in this folder. Move those files to Processed folder.  
                        fileQuery = GetRequiredFileList(dir, FileCollection.PRCFiles);
                        foreach (FileInfo fi in fileQuery)
                        {
                            try
                            {
                                if (!IsFileLocked(fi))
                                {
                                    MoveFile(TransallMove.Root_Processed, fi, true);
                                }
                            }
                            catch (Exception ex) // in case of error, skip only that file and proceed with remaining 
                            {
                                BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                                AddLog(true, true, null, LogType.Error);
                            }
                            
                        }


                        // Once transfered, move remaining files to ERROR folder. As this is not valid extension 
                        fileQuery = GetRequiredFileList(dir, FileCollection.ExcludeValidExtensions);

                        foreach (FileInfo fi in fileQuery)
                        {
                            try
                            {
                                if (!IsFileLocked(fi))
                                {
                                    BuildMessage(string.Format(TransallMessages.InvalidExtension, strMessage, this.ValidExtensionString, fi.Extension), true, false);
                                    MoveFile(TransallMove.Root_Error, fi, true);
                                    intError += 1;
                                }
                            }catch (Exception ex) // in case of error, skip only that file and proceed with remaining 
                            {
                                BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                                AddLog(true, true, null, LogType.Error); 
                            }
                        }

                        if (intError > 0)
                            BuildMessage(String.Format(TransallMessages.RegionErrors, strMessage, intError.ToString(), this.CurrentRegion), false, false);
                    }
                    catch (Exception ex)
                    {
                        BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                        // Add Error  in Input error Folder  
                        AddLog(true, true, null,LogType.Error);   
                    }

                }

                if (intProcessed > 0 || intError > 0)
                {
                    //Save Current statistics in DB
                    AddLog(false, false, null, LogType.Information); 
                }
            }
            catch (Exception ex)
            {
                BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                // Add Error  in Input error Folder  
                AddLog(true, true, null, LogType.Error); 
            }

        }

        public void ProcessTempFolder()
        {
            IEnumerable<System.IO.FileInfo> fileQuery = null;
            DirectoryInfo dirTemp;
            int intProcessed;
            string strDestination = string.Empty;
            bool blnRegion = false; 

            try
            {
                strMessage = "[ProcessTempFolder] -"; 
                //start processing Temp files Region wise 
                foreach (DirectoryInfo dir in this.UsableRegionInfo.Keys)
                {
                    DBLogger.Length = 0;
                    intProcessed = 0;
                    strDestination = string.Empty;

                    this.CurrentRegion = dir.Name.ToUpper();
                    this.CurrentHub = Key(dir.Name.ToUpper());

                    dirTemp = new DirectoryInfo(string.Format(this.Tempfolder, dir.Name.ToUpper()));

                    fileQuery = GetRequiredFileList(dirTemp, FileCollection.VaildExtensions);

                    BuildMessage(String.Format(TransallMessages.TransallStart, strMessage, this.CurrentRegion, DateTime.Now.ToString()),false,false);

                    BuildMessage("Before Login - Environment - " + this.CurrentEnvironment + " Hub - " + this.CurrentHub + " Region - " + this.CurrentRegion + " User - " + this.CurrentUser, false, true);
                    blnRegion = RegionLogin(this.CurrentHub);
                    BuildMessage("After Login - Environment - " + this.CurrentEnvironment + " Hub - " + this.CurrentHub + " Region - " + this.CurrentRegion + " User - " + this.CurrentUser, false, true);

                    foreach (FileInfo fi in fileQuery)
                        {
                            try
                            {
                                if (blnRegion && fi.Exists && !IsFileLocked(fi))// Possiblity that paired files will get processed together and this collection will have that entry with old path of file.  
                                {
                                    //FileLogger.Length = 0;

                                    if (fi.Length <= 0) // directly move files with 0 bytes to Error folder 
                                    {
                                        BuildMessage(String.Format(TransallMessages.TransallFileSkip, strMessage, fi.FullName, fi.Length.ToString(), fi.IsReadOnly.ToString()), true, false);
                                        MoveFile(TransallMove.Temp_Error, fi, true);
                                        intProcessed += 1;
                                    }
                                    else
                                    {
                                        ProcessInputTempFile(fi);

                                        if (FileLogger.Length > 0)
                                        {
                                            AddLog(false, true, fi, LogType.Information);
                                            intProcessed += 1;
                                        }
                                    }
                                    strDestination = (strDestination == string.Empty) ? fi.Directory.FullName : strDestination;

                                }
                                else if (!blnRegion)
                                {
                                    BuildMessage(string.Format(TransallMessages.RegionLoginError, this.CurrentUser, this.CurrentRegion, this.CurrentHub), true, false);
                                    MoveFile(TransallMove.Temp_Error, fi, true);
                                }

                            }
                            catch (Exception ex)
                            {
                                BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                                AddLog(true, true, null, LogType.Error);
                            }
                           
                        }

                        if (intProcessed > 0) // Add DB log only if this region's files were processed. 
                        {
                            BuildMessage(String.Format(TransallMessages.TransallEnd, strMessage, intProcessed.ToString(), this.CurrentRegion, strDestination.Trim()), false, false);
                            AddLog(false, false, null, LogType.Information);
                        }
                }
            }
            catch (Exception ex)
            {
                BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                AddLog(true, true, null, LogType.Error);
            }
        }

        public void ProcessOutputfiles()
        {
            string fileNamePath = string.Empty;
            IEnumerable<System.IO.FileInfo> fileQuery = null;
            FileInfo fileOutput;
            try
            {
                strMessage = "[ProcessOutputfiles]- ";
                foreach (DirectoryInfo dir in this.UsableRegionInfo.Keys)
                {
                    this.CurrentRegion = dir.Name.ToUpper();
                    this.CurrentHub = Key(this.CurrentRegion).ToString();
                    fileQuery = GetRequiredFileList(dir,  FileCollection.VaildExtensions);
                    foreach (FileInfo outputDirectoryFile in fileQuery)
                    {
                        try
                        {
                            DBLogger.Length = 0;

                            if (outputDirectoryFile.Extension != Key(configKeys.outputTriggerExtension)) // Process only if having Trigger Extension
                                continue;

                            fileNamePath = Path.ChangeExtension(outputDirectoryFile.FullName, Key(configKeys.outputExtension));

                            fileOutput = new FileInfo(fileNamePath);

                            if (RegionLogin(this.CurrentHub))
                            {
                                OutputFileCreated(fileOutput.DirectoryName, fileOutput);
                                AddLog(false, false, fileOutput, LogType.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, false, false);
                            BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, false);
                            if (outputDirectoryFile.Exists)
                            {
                                MoveOutputProcessed(outputDirectoryFile, Path.Combine(Path.Combine(this.RootPath, this.CurrentRegion), Key(configKeys.errorFolder)));
                                AddLog(false, true, outputDirectoryFile, LogType.Error);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                AddLog(true, true, null, LogType.Error);
            }

        }

        public void ProcessOPTrcFiles()
        {
            IEnumerable<System.IO.FileInfo> fileQuery = null;
            try
            {
                strMessage = "[ProcessOPTrcFiles]- ";
                //clearout TRC files in each region
                foreach (DirectoryInfo dir in this.UsableRegionInfo.Keys)
                {
                    this.CurrentRegion = dir.Name.ToUpper();
                    this.CurrentHub = Key(this.CurrentRegion).ToString();
                    fileQuery = GetRequiredFileList(dir, FileCollection.TRCFiles);
                    foreach (FileInfo outputDirectoryFile in fileQuery)
                    {
                        MoveFile(TransallMove.Output_Processed,outputDirectoryFile,false);
                    }
                }
            }
            catch (Exception ex)
            {
                BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                AddLog(true, true, null, LogType.Error);
            }

        }

        public void DeleteOldfiles()
        {
            string strProcessed = string.Empty;
            string strError = string.Empty;
            IEnumerable<System.IO.FileInfo> fileQuery = null;
            DirectoryInfo subDir;
            int intProcessed = 0;
            string strMessage = "DeleteOldfiles";

            try
            {
                if (Key(configKeys.cleanOutput).ToString() == "1") // Skip if setting is for keeping output files i.e. 0  
                {

                    foreach (DirectoryInfo dir in this.UsableRegionInfo.Keys)
                    {
                        this.CurrentRegion = dir.Name.ToUpper();
                        subDir = new DirectoryInfo(string.Format(this.Processedfolder, this.CurrentRegion));
                        fileQuery = GetRequiredFileList(subDir, FileCollection.FilesToDelete);

                        foreach (FileInfo fi in fileQuery)
                        {
                            try
                            {
                                BuildMessage(string.Format(TransallMessages.FileDeleted, fi.Name,
                                        string.Format(TransallMessages.LogCodeOutput, this.CurrentRegion),
                                        Key(configKeys.processedFolder), fi.CreationTime),
                                        false, false);
                                fi.Delete();
                                intProcessed += 1;
                            }
                            catch (Exception ex)// skip this file and move to other 
                            {
                                BuildMessage(strMessage + " " + fi.Name + " : " + ex.Message, true, true);
                                AddLog(true, true, null, LogType.Error);
                            }
                        }

                        subDir = new DirectoryInfo(string.Format(this.Errorfolder, this.CurrentRegion));
                        fileQuery = GetRequiredFileList(subDir, FileCollection.FilesToDelete);

                        foreach (FileInfo fi in fileQuery)
                        {
                            try
                            {
                                BuildMessage(string.Format(TransallMessages.FileDeleted, fi.Name,
                                       string.Format(TransallMessages.LogCodeOutput, this.CurrentRegion),
                                       Key(configKeys.errorFolder), fi.CreationTime),
                                       false, false);
                                fi.Delete();
                                intProcessed += 1;

                            }
                            catch (Exception ex) // skip this file and move to other 
                            {
                                BuildMessage(strMessage + " " + fi.Name + " : " + ex.Message, true, true);
                                AddLog(true, true, null, LogType.Error);
                            }
                        }

                        if (intProcessed > 0)
                            AddLog(false, false, null, LogType.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                BuildMessage(strMessage + " " + ex.Message, true, true);
                AddLog(true, true, null, LogType.Error);
            }
        
        }
      

        /// <summary>
        /// MoveProcessedFilesToOutput
        /// </summary>
        /// <remarks>This method moves processed files into Output folder for transfering @FTP site</remarks>
        public void MoveProcessedFilesToOutput()
        {
            IEnumerable<System.IO.FileInfo> fileList = null;
            string strOutTextfile = string.Empty;
            string strOutfile = string.Empty;
            string strDestinationPath = string.Empty;
            int charsToskip;
            DirectoryInfo dirTemp;
            FileInfo fileOut;
            FileInfo fileOutTrigger;
            int intProcessed;

            try
            {
                foreach (DirectoryInfo dir in this.UsableRegionInfo.Keys)
                {
                    intProcessed = 0;
                    DBLogger.Length = 0;

                    this.CurrentRegion = dir.Name.ToUpper();
                    this.CurrentHub = Key(this.CurrentRegion).ToString();
                    strMessage = "[MoveProcessedFilesToOutput]- ";
                    BuildMessage(string.Format(TransallMessages.TransallMoveToOutput, DateTime.Now.ToString(), strMessage, this.CurrentRegion), false, false);

                    dirTemp = new DirectoryInfo(Path.Combine(dir.FullName, Key(configKeys.tempFolder)));
                    strDestinationPath = string.Format(Path.Combine(Key(configKeys.outputDirectory), dir.Name), this.CurrentEnvironment);

                    fileList = dirTemp.GetFiles("*.*");
                    charsToskip = dirTemp.FullName.Length;

                    // Fetch only those files where both txt and OUT files available. hence > 1 chk 
                    var queryProcFiles = from file in fileList
                                         group file.FullName.Substring(charsToskip) by Path.GetFileNameWithoutExtension(file.Name) into fileGroup
                                         where fileGroup.Count() > 1
                                         select fileGroup;

                    //queryDupFiles contain only name of such files. 
                    foreach (var file in queryProcFiles)
                    {
                        try
                        {
                            strOutTextfile = file.Key + Key(configKeys.outputTriggerExtension);
                            strOutfile = file.Key + Key(configKeys.outputExtension);

                            fileOut = new FileInfo(Path.Combine(dirTemp.FullName, strOutfile));
                            fileOutTrigger = new FileInfo(Path.Combine(dirTemp.FullName, strOutTextfile));

                            BuildMessage("File - "+ fileOut.Name + " Environment - " + this.CurrentEnvironment + " Hub - " + this.CurrentHub + " Region - " + this.CurrentRegion + " User - " + this.CurrentUser, false, true);

                            if (!IsFileLocked(fileOut) && !IsFileLocked(fileOutTrigger))
                            {
                                BuildMessage(fileOut.Name, false, false);
                                MoveFile(TransallMove.Temp_Output, fileOut, false);
                                MoveFile(TransallMove.Temp_Output, fileOutTrigger, false);
                                intProcessed += 1;
                            }
                        }
                        catch (Exception ex) // skip this set and move further 
                        {
                            BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                            AddLog(true, true, null, LogType.Error);
                        }
                    }
            
                    if (intProcessed > 0)
                    {
                        BuildMessage(String.Format(TransallMessages.TransallOPTransferComplete, strMessage, intProcessed.ToString(), this.CurrentRegion), false, false);
                        AddLog(false, false, null, LogType.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                BuildMessage(strMessage + " " + ex.Message + " " + ex.StackTrace, true, true);
                AddLog(true, true, null, LogType.Error);
            }

        }

        
        public static void CreateGenericLogFile(string strMessage, string strPath)
        {

            string strConfigDetails;
            string pathfinder = string.Empty;
            string logExtension = ConfigurationManager.AppSettings[configKeys.logExtension];
            

            if (strPath == string.Empty)
                strPath = Path.Combine(string.Format(ConfigurationManager.AppSettings[configKeys.inputDirectory]
                                                        , EnvironmentContext.Current.EnvironmentName.ToLower())
                                                        , ConfigurationManager.AppSettings[configKeys.logFolder]);
            pathfinder = GetLogDB();
            
            if(pathfinder.IndexOf("1") >=0)
                pathfinder = "PATH1" ;
            else if (pathfinder.IndexOf("2") >= 0)
                pathfinder = "PATH2"; 
            else 
                pathfinder = "PROD"; 

            string strFile = string.Format(TransallMessages.GenericLogFileName, pathfinder,System.DateTime.Now.ToString("yyyyMMdd") + logExtension);
            string strLogFilePath = Path.Combine(strPath, strFile);

            if (File.Exists(strLogFilePath)) // Add log in same file if available on that date
            {

                using (StreamWriter sw = File.AppendText(strLogFilePath))
                {
                    sw.Write("\r\n\r\nLog Entry : " + DateTime.Now.ToString() + "\r\n");
                    sw.WriteLine(strMessage);
                }
            }
            else
            {
                //when File is newly created, insert current Config values for reference 
                using (StreamWriter sw = File.CreateText(strLogFilePath))
                {
                    sw.Write("\r\nConfig Settings : " + "\r\n");
                    strConfigDetails = "Path:" + pathfinder + Environment.NewLine;
                    strConfigDetails = strConfigDetails + "Hub Region:" + Assurant.ElitaPlus.Common.AppConfig.HubRegion.ToUpper() + Environment.NewLine;
                    strConfigDetails = strConfigDetails + "Common Log DB:" + GetLogDB() + Environment.NewLine;
                    strConfigDetails = strConfigDetails + "Delay Time:" + ConfigurationManager.AppSettings[configKeys.delayTime].ToString() + Environment.NewLine;
                    strConfigDetails = strConfigDetails + "Log Switch:" + ConfigurationManager.AppSettings[configKeys.logSwitch].ToString() + Environment.NewLine;
                    strConfigDetails = strConfigDetails +"Clean Output Flag:" + ConfigurationManager.AppSettings[configKeys.cleanOutput].ToString() + Environment.NewLine;
                    strConfigDetails = strConfigDetails + "Days to keep old Files:" + ConfigurationManager.AppSettings[configKeys.daystoKeep].ToString() + Environment.NewLine;
                    strConfigDetails = strConfigDetails + "Bypass Keyword:" + ConfigurationManager.AppSettings[configKeys.bypassKeyword].ToString() + Environment.NewLine;
                    sw.Write(strConfigDetails + "\r\n");
                    sw.Write("\r\nLog Entry : " + DateTime.Now.ToString() + "\r\n");
                    sw.WriteLine(strMessage);
                }
            }
        }

        private static string GetLogDB()
        {
            string strVal = string.Empty;
            string strDB = string.Empty; // 


            if(EnvironmentContext.Current.Environment == Environments.Production)
                strDB = ConfigurationManager.AppSettings[configKeys.logDBProd];
            else
                strDB = ConfigurationManager.AppSettings[configKeys.logDB];
            
            //if {0} : need to set default based on HubRegion. Else User has explicitly set the value in Config file for logging details. 
            if(strDB.IndexOf("{0}") >= 0)
            {
                int intCurrentPath = GetPathValue();

                if (intCurrentPath > 0)
                    strVal = "A" + intCurrentPath.ToString().Trim();
                else
                    strVal = "SA";

                strDB = string.Format(strDB,strVal);

            }

            return strDB;
        }

        // 1: Path 1 , 2 : Path 2 , 0 : Prod 
        private static int GetPathValue()
        {
            int intCurrentPath = 0;

            if (Assurant.ElitaPlus.Common.AppConfig.HubRegion.ToUpper().IndexOf("1") >= 0)
                intCurrentPath = 1;
            else if (Assurant.ElitaPlus.Common.AppConfig.HubRegion.ToUpper().IndexOf("2") >= 0)
                intCurrentPath = 2; 

            return intCurrentPath;
        }

        public void Dispose()
        {

        }



        #endregion


        #region "Private Methods"
        
        #region "Common Private Methods"

        private bool KeyAvailable(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null || ConfigurationManager.AppSettings[key] == string.Empty)
                return false;
            else
                return true;
        }

        private void PrepareUsableRegions()
        {
            DirectoryInfo di;
            string strPath = string.Empty;
            
            this.CurrentEnvironment = EnvironmentContext.Current.EnvironmentName.ToLower();
            this.CurrentUser = Key(configKeys.userID);
            this.LogDB = GetLogDB(); // Default Log DB 
            int.TryParse(Key(configKeys.logSwitch), out this.LogSwitch); // default Log switch : 0 : seperate in file & DB, 1: All in DB , 2 : All in File 
            this.UsableRegionInfo = new Dictionary<DirectoryInfo, string>();


            if (this.isInputDir)
            {
                this.ValidExtensionString = Key(configKeys.fileExtension) + "," + Key(configKeys.killExtension);
                this.RootPath = String.Format(Key(configKeys.inputDirectory), CurrentEnvironment);
                this.Tempfolder = Path.Combine(Path.Combine(RootPath, "{0}"), Key(configKeys.tempFolder));
            }
            else
            {
                this.ValidExtensionString = Key(configKeys.outputExtension) + "," + Key(configKeys.outputTriggerExtension);
                this.RootPath = String.Format(Key(configKeys.outputDirectory), CurrentEnvironment);
            }

            this.Errorfolder = Path.Combine(Path.Combine(RootPath, "{0}"), Key(configKeys.errorFolder));
            this.Processedfolder = Path.Combine(Path.Combine(RootPath, "{0}"), Key(configKeys.processedFolder));
            this.GenericLogfolder = Path.Combine(this.RootPath, Key(configKeys.logFolder));
            Directory.CreateDirectory(Path.Combine(this.RootPath, Key(configKeys.logFolder)));

            this.ValidExtensions = this.ValidExtensionString.ToUpper().Split(',');

            di = new DirectoryInfo(RootPath);
            subDir = di.GetDirectories();

            // Get All subdirectories in Main input folder 
            foreach (DirectoryInfo dir in subDir)
            {
                // Create subfolders in folders mentioned in Config files only & not in other folders
                if (KeyAvailable(dir.Name.ToUpper()) && dir.Name != Key(configKeys.logFolder))
                {
                    this.UsableRegionInfo.Add(dir, Key(dir.ToString().ToUpper()));

                    if (this.isInputDir)
                    {
                        strPath = Path.Combine(dir.FullName, Key(configKeys.tempFolder));
                        Directory.CreateDirectory(strPath);
                    }

                    strPath = Path.Combine(dir.FullName, Key(configKeys.processedFolder));
                    Directory.CreateDirectory(strPath);

                    strPath = Path.Combine(dir.FullName, Key(configKeys.errorFolder));
                    Directory.CreateDirectory(strPath);
                }
            }
        }


        private IEnumerable<System.IO.FileInfo> GetRequiredFileList(DirectoryInfo dir, FileCollection collect)
        {
            IEnumerable<System.IO.FileInfo> fileQuery = null;
            IEnumerable<System.IO.FileInfo> fileList;
            int intCountDays;

            fileList = dir.GetFiles("*.*");
            var exts = this.ValidExtensions;

            switch (collect)
            { 
                case FileCollection.VaildExtensions:
                    fileQuery = from file in fileList
                                where exts.Contains(file.Extension.ToUpper())
                                select file;
                    break;
                case FileCollection.ExcludeValidExtensions:
                    fileQuery = from file in fileList
                                where !exts.Contains(file.Extension.ToUpper())
                                select file;
                    break;
                case FileCollection.TRCFiles:
                    fileQuery = dir.GetFiles("*" + Key(configKeys.trcExtn));
                    break;
                case FileCollection.PRCFiles:
                    fileQuery = dir.GetFiles("*" + Key(configKeys.processExtension));
                    break;
                case FileCollection.FilesToDelete:
                    int.TryParse(Key(configKeys.daystoKeep), out intCountDays);
                    exts = new string[]{Key(configKeys.trcExtn).ToString().ToUpper()}; //Dont fetch TRC files. Keep TRC files in the folder for reference 
                    fileQuery = from file in fileList
                                where file.CreationTime < DateTime.Now.AddDays(-intCountDays) && !exts.Contains(file.Extension.ToUpper())
                                select file;
                    break;
            }

            return fileQuery;
        }


        private void MoveFile(TransallMove operation, FileInfo file, bool blnGenerateLog)
        {
            string strSourcePath = string.Empty;
            string strDestinationPath = string.Empty;
            string strFile = string.Empty;
            int intSetError = 0;

            strSourcePath = file.Directory.FullName;
            strMessage = "[MoveFile]-";

            switch (operation)
            {
                case TransallMove.Root_Temp:
                    strDestinationPath = string.Format(this.Tempfolder, this.CurrentRegion);
                    intSetError += 1;
                    break;
                case TransallMove.Root_Error:
                    strDestinationPath = string.Format(this.Errorfolder, this.CurrentRegion);
                    break;
                case TransallMove.Root_PreProcess_Error:
                    strDestinationPath = string.Format(this.Errorfolder, this.CurrentRegion);
                    break;
                case TransallMove.Root_Output:
                    strDestinationPath = string.Format(Path.Combine(Key(configKeys.outputDirectory), this.CurrentRegion), this.CurrentEnvironment);
                    intSetError += 1;
                    break;
                case TransallMove.Root_Processed:
                    strDestinationPath = string.Format(this.Processedfolder, this.CurrentRegion);
                    intSetError += 1;
                    break;
                case TransallMove.Temp_Processed:
                    strDestinationPath = string.Format(this.Processedfolder, this.CurrentRegion);
                    intSetError += 1;
                    break;
                case TransallMove.Temp_Error:
                    strDestinationPath = string.Format(this.Errorfolder, this.CurrentRegion);
                    break;
                case TransallMove.Temp_Output:
                    strDestinationPath = string.Format(Path.Combine(Key(configKeys.outputDirectory), this.CurrentRegion), this.CurrentEnvironment);
                    break;
                case TransallMove.Output_Processed: // Need to be call for output folder operations only 
                    strDestinationPath = string.Format(this.Processedfolder, this.CurrentRegion);
                    break;
                case TransallMove.Output_Error: // Need to be call for output folder operations only 
                    strDestinationPath = string.Format(this.Errorfolder, this.CurrentRegion);
                    break;
            }

            try
            {
                if (!IsFileLocked(file))
                {
                    strSourcePath = Path.Combine(strSourcePath, file.Name);
                    strDestinationPath = Path.Combine(strDestinationPath, file.Name);

                    if (File.Exists(strDestinationPath))
                    {
                        if (intSetError > 0) // If flag is to set duplicate file, then create new file 
                        {
                            BuildMessage(string.Format(TransallMessages.DuplicateFile, strMessage, file.Name, Key(configKeys.tempFolder)), true, false);
                            FileInfo oldFile = new FileInfo(strDestinationPath);
                            if (!IsFileLocked(oldFile))
                            {
                                //As same file available. Rename existing file in the folder to .Old
                                strFile = string.Format("{0}.{1}.old", oldFile.Name, DateTime.Now.Ticks.ToString());
                                oldFile.MoveTo(Path.Combine(string.Format(this.Processedfolder, this.CurrentRegion), strFile));
                                file.MoveTo(strDestinationPath); // now copy this file
                            }
                        }
                        else// Delete existing and then move this file 
                        {
                            if (File.Exists(strDestinationPath))
                            {
                                FileInfo fileDup = new FileInfo(strDestinationPath);
                                fileDup.Delete();
                            }
                            file.MoveTo(strDestinationPath);
                        }
                    }
                    else // first time copy. So copy directly 
                    {
                        file.MoveTo(strDestinationPath);
                    }

                    if (blnGenerateLog)
                    {
                        AddLog(false, true, file, LogType.Information);
                    }

                }
                else
                {
                    BuildMessage("File is Locked and no further process, File - " + file.Name, false, true);
                }
            }
            catch (Exception ex)
            {
                BuildMessage(String.Format(TransallMessages.TransallSourceException, strMessage, file.FullName, Environment.NewLine, ex.Message, ex.StackTrace), false, false);
            }

        }

        bool RegionLogin(string hub)
        {

            string networkID = Key(configKeys.userID).ToString();
            string[] values = hub.Split('\\');
            string connType = values[0];
            string domainName = values[1];

            if (!Login(networkID, connType, domainName))
            {
                BuildMessage(string.Format(TransallMessages.LoginError, networkID, connType, domainName),true,true);
                AddLog(true, true, null, LogType.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        bool Login(string networkID, string connType, string machineDomain)
        {
            //string errMsg = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                Assurant.ElitaPlus.BusinessObjectsNew.Authentication oAuthentication = new Assurant.ElitaPlus.BusinessObjectsNew.Authentication();

                // The Machine name ex; ATL0, BUE0, ORK0 have to passed with EU or NONE if development
                oAuthentication.CreatePrincipalForServices(networkID, connType, machineDomain);
                System.Threading.Thread.CurrentPrincipal = (Assurant.ElitaPlus.BusinessObjectsNew.ElitaPlusPrincipal)(oAuthentication.CreatePrincipalForServices(networkID, connType, machineDomain));
                Assurant.ElitaPlus.BusinessObjectsNew.Authentication.SetCulture();

                return true;
            }
            catch (Exception ex)
            {
                //errMsg = "[" + networkID + "-" + connType + "-" + machineDomain + "]";
                sb.Append("[" + networkID + "-" + connType + "-" + machineDomain + "]");
                //errMsg = errMsg + Environment.NewLine + ex.Message + Environment.NewLine;
                sb.Append(Environment.NewLine + ex.Message);
                sb.Append(Environment.NewLine + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    //errMsg += " Inner Exception Message" + ex.InnerException.Message;
                    sb.Append(Environment.NewLine + " Inner Exception:");
                    sb.Append(Environment.NewLine + ex.InnerException.Message);
                    sb.Append(Environment.NewLine + ex.InnerException.StackTrace);
                }
                BuildMessage(sb.ToString(), true, true);

                return false;
            }
        }


        //Check if file is unavailable because it is still being written to or being processed by another thread or deleted
        private bool IsFileLocked(FileInfo file)
        {
            FileStream strm = null;
            bool blnLocked;
            try
            {
                if (file.IsReadOnly == true)
                {
                    blnLocked = true;
                    BuildMessage("File Is Readonly, File - " + file.Name, false, true);
                }
                else
                {
                    strm = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None); //if file is locked. it will raise exception
                    BuildMessage("File Is Not Locked, File - " + file.Name, false, true);
                    blnLocked = false;
                }
            }
            catch (IOException)
            {
                BuildMessage("Error in Opening the File, File - " + file.Name, false, true);
                blnLocked = true; 
            }
            finally
            {
                if (strm != null)
                    strm.Close();
            }
            return blnLocked;
        }

        private void BuildMessage(string strMessage ,bool isForLogfile, bool isGeneric)
        {

            if (isGeneric) // Generic messages will always be stored in LOG folder irrespective of switch 
            {
                this.FileLogger.AppendLine(strMessage);
                return;
            }

            switch (this.LogSwitch)
            {
                case (int)LogLocation.both: //Seperate in DB & File
                    if (isForLogfile)
                        this.FileLogger.AppendLine(strMessage);
                    else
                        this.DBLogger.AppendLine(strMessage);
                    break;
                case (int)LogLocation.database://All in DB (So no log will get generated for File) 
                        this.DBLogger.AppendLine(strMessage);
                    break;
                case (int)LogLocation.file://All in File (So no log will get generated for DB)
                        this.FileLogger.AppendLine(strMessage);
                    break;
            }

        }

        private void AddLog(bool isGeneric, bool isLogFile,FileInfo fi , LogType logTyp)
        {
            string strPath = string.Empty;
            string strCode = string.Empty; 

            //create Log file if below criteria is satisfied.
            if (FileLogger.Length > 0 && ((this.LogSwitch == (int)LogLocation.both && isLogFile) || this.LogSwitch == (int)LogLocation.file || isGeneric))
            {
                if (isGeneric || fi==null)
                    CreateGenericLogFile(FileLogger.ToString(), this.GenericLogfolder);
                else
                {
                    strPath = string.Format("{0}.log",Path.Combine(fi.Directory.FullName,Path.GetFileNameWithoutExtension(fi.Name)));
                    using (StreamWriter sw = File.CreateText(strPath))
                    {
                        sw.WriteLine(FileLogger.ToString());
                    }
                }
                //Once written clear the contents 
                FileLogger.Length = 0;

            }

            //when it is required to Add in DB 
            if (DBLogger.Length > 0 && ((this.LogSwitch == (int)LogLocation.both && !isLogFile) || this.LogSwitch == (int)LogLocation.database) && !isGeneric)
            {
                if (fi == null)
                    strPath = string.Format(TransallMessages.DefaultLog,(logTyp == LogType.Error ? "Error" : "Information"));
                else
                    strPath = fi.FullName.ToString();

                if (this.isInputDir)
                    strCode = string.Format(TransallMessages.LogCodeInput, this.CurrentRegion); 
                else
                    strCode = string.Format(TransallMessages.LogCodeOutput, this.CurrentRegion);

                strMessage = string.Empty;

                if (RegionLogin(this.LogDB.ToString()))  // Log to Default DB 
                {
                    Assurant.ElitaPlus.BusinessObjectsNew.TransallMapping.LogTransallMessage(this.CurrentHub, strPath,
                                                                                        strCode, DBLogger.ToString(),
                                                                                        this.CurrentUser);
                }
                else//not able to login. so add all log in Generic file 
                {
                    CreateGenericLogFile(DBLogger.ToString(),this.GenericLogfolder);
                }
             
                //Once written clear the contents 
                DBLogger.Length = 0;
            }

        
        }

        #endregion
        
        #region "Input Path Specific Methods"

        bool PreProcessInputFile(FileInfo file)
        {
            bool blnValid = true;
            string strMessage = string.Empty;
            string strDestinationPath = string.Empty;
            int intFilecount = 0;

            BuildMessage("File - " + file.Name + " Environment - " + this.CurrentEnvironment + " Hub - " + this.CurrentHub + " Region - " + this.CurrentRegion + " User - " + this.CurrentUser, false, true);
            DataView dv = Assurant.ElitaPlus.BusinessObjectsNew.TransallMapping.GetList(file.Name, null);
            strMessage = "[PreProcessInputFile] -";
           
            // 1> Check if file has mapping issue. In that case move the file in Error folder 
            if (dv.Count != 1)
            {
                blnValid = false;
                BuildMessage(String.Format(TransallMessages.TransallMappingIssue, strMessage,file.Name, dv.Count.ToString()), true, false);
                MoveFile(TransallMove.Root_PreProcess_Error, file,true);
            }
            else
            {
                //2> Check if file has  mapping of "NONE" at transall level which means file has to be transfered to Output directory directly without processing  
                if (dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_PACKAGE].ToString().ToUpper() == Key(configKeys.bypassKeyword).ToUpper())
                {
                    blnValid = false;
                    BuildMessage(String.Format(TransallMessages.BypassTransall, strMessage, file.Name), true,false);
                    MoveFile(TransallMove.Root_Output, file,false);
                    //create .OUT file and move that as well to output 
                    CreateOutFile(file.Directory.FullName, file);
                    //transfer Log file to Processed folder 
                    AddLog(false, true, file, LogType.Information);
                }
                else //3> If Multiple files required for this dealer then check if there are same number of files available for further processing 
                {
                    intFilecount = (Int16)dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_NUM_FILES];
                    if (intFilecount > 1)
                    {
                        DirectoryInfo dir = new DirectoryInfo(file.DirectoryName);
                        FileInfo[] files = null;
                        files = dir.GetFiles((string)dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_INBOUND_FILENAME]);
                        blnValid = false; 
                        if (files.Count() != intFilecount)
                            BuildMessage(String.Format(TransallMessages.TransallFileWait, strMessage, file.FullName), false,false);//SKIP file in this cycle as all are not received.
                        else
                        { 
                            //Move all these paired files in TEMP folder at same time
                            foreach (FileInfo fl in files)
                            {
                                MoveFile(TransallMove.Root_Temp,fl,false);
                            }

                        }

                    }
                }
            }

            if (!blnValid)
                AddLog(false,false,file,LogType.Information);

            return blnValid;
        }



        void ProcessInputTempFile(FileInfo fil)
        {

            try
            {
                strMessage = "[ProcessInputTempFile]- ";
                if (fil.Exists && (fil.Length > 0 || Key(configKeys.killExtension).Contains(fil.Extension.ToLower())))
                {
                    // Kill the file processing if marked for. 
                    if (Key(configKeys.killExtension).Contains(fil.Extension.ToLower()))
                    {
                        KillProcess(fil.Name);
                        fil.Delete();
                        BuildMessage(String.Format(TransallMessages.TransallKill, strMessage, fil.Name),true,false);
                        return;
                    }

                    try
                    {
                        //check if this file does not have mapping at db level 
                        DataView dv = Assurant.ElitaPlus.BusinessObjectsNew.TransallMapping.GetList(fil.Name, null);

                        if (dv.Count == 1)
                        {
                            BuildMessage(String.Format(TransallMessages.TransallFileStart, strMessage, fil.FullName.ToUpper(), DateTime.Now.ToString()), true, false);
                            BuildMessage(fil.Name, false, false);
                            //If only 1 file is needed for this client, call transall with the file
                            if ((Int16)dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_NUM_FILES] == 1)
                            {
                                BuildMessage("File - " + fil.FullName + " Environment - " + this.CurrentEnvironment + " Hub - " + this.CurrentHub + " Region - " + this.CurrentRegion + " User - " + this.CurrentUser, false, true);
                                MoveFile(TransallMove.Temp_Processed, fil, true);
                                BuildMessage("Confirm before start the Process, File - " + Path.Combine(string.Format(this.Processedfolder, this.CurrentRegion), fil.Name), false, true);
                                if (!File.Exists(Path.Combine(string.Format(this.Processedfolder, this.CurrentRegion), fil.Name)))
                                {
                                    BuildMessage("File Not moved yet, waiting for 5 seconds.", false, true);
                                    System.Threading.Thread.Sleep(5000);
                                }
                                LaunchTransAllForNewFiles(dv, string.Format("\"{0}\"", fil.FullName), this.CurrentHub.ToUpper(), this.CurrentRegion.ToUpper());
                            }
                            else
                            {
                                //multiple files are required for this client.  Check if they are all there
                                //If all are not prsent then skip processing it till second is received.  
                                DirectoryInfo dir = new DirectoryInfo(fil.DirectoryName);
                                FileInfo[] fils = null;

                                fils = dir.GetFiles((string)dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_INBOUND_FILENAME]);

                                if (fils.Count() == (Int16)dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_NUM_FILES])
                                {
                                    string FileName = "";
                                    foreach (FileInfo tmpFile in fils)
                                    {
                                        if (fil.Name == tmpFile.Name)
                                        {
                                            MoveFile(TransallMove.Temp_Processed, fil,false);
                                            FileName += string.Format("\"{0}\" ", fil.FullName);
                                        }
                                        else
                                        {
                                            MoveFile(TransallMove.Temp_Processed, tmpFile,false);
                                            FileName += string.Format("\"{0}\" ", tmpFile.FullName);
                                        }
                                       
                                    }
                                    LaunchTransAllForNewFiles(dv, FileName, this.CurrentHub.ToUpper(), this.CurrentRegion.ToUpper());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        BuildMessage(String.Format(TransallMessages.TransallFileException, strMessage, fil.FullName, Environment.NewLine, ex.Message, ex.StackTrace), true, false);
                        MoveFile(TransallMove.Temp_Error, fil, true);
                    }
                }
                else
                {
                    BuildMessage(String.Format(TransallMessages.TransallFileSkip, strMessage, fil.FullName, fil.Length.ToString(), fil.IsReadOnly.ToString()), true, false);
                    MoveFile(TransallMove.Temp_Error, fil, true);
                }
            }
            catch (Exception ex)
            {
                BuildMessage(String.Format(TransallMessages.TransallFileException,strMessage, fil.FullName, Environment.NewLine, ex.Message, ex.StackTrace), true, false);
            }
        }


        private void LaunchTransAllForNewFiles(DataView dv, string FileName, string strHubRgn, string strRegion)
        {
            string strCommand = null;
            var _Proc = new Process();

            // Processed Files will be stored in same directory as that of current file.
            string path = String.Format(this.Tempfolder, this.CurrentRegion);

            Directory.CreateDirectory(path); 

            strCommand = Key(configKeys.commandLineCall).ToString();

            _Proc.StartInfo = new ProcessStartInfo(strCommand);

            try
            {
                BuildMessage("Checking for Transall Exe file for Path - " + strCommand, false, true);

                if (!File.Exists(strCommand))
                {
                    BuildMessage("Transall Exe file not exists.", false, true);
                }
            }
            catch (Exception ex)
            {
                BuildMessage("Transall Exe file check error.", false, true);
                ex = null;
            }

            try
            {
                BuildMessage("Checking for Transall Package for Path - " + dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_PACKAGE].ToString(), false, true);

                if (!File.Exists(dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_PACKAGE].ToString()))
                {
                    BuildMessage("Transall Package file not exists.", false, true);
                }
            }
            catch (Exception ex)
            {
                BuildMessage("Transall Package file check error.", false, true);
                ex = null;
            }

            // When processing multiple files it would need to be called sperately
            string processArgStr = String.Format("\"{0}\" Startup {1} \"{2}\"",
                                   dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_PACKAGE].ToString(),
                                   FileName,
                                   path);

            _Proc.StartInfo.Arguments = processArgStr.ToString();

            _Proc.EnableRaisingEvents = true;

            BuildMessage("Arguments - " + processArgStr.ToString() + " Environment - " + this.CurrentEnvironment + " Hub - " + this.CurrentHub + " Region - " + this.CurrentRegion + " User - " + this.CurrentUser, false, true);
            _Proc.Start();

          
            BuildMessage(String.Format(TransallMessages.TransallLaunch
                                    , dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_PACKAGE].ToString()
                                    , _Proc.Id, FileName.Replace("\"", string.Empty)
                                      ), true, false);

        }


        /// <summary>
        /// KillProcess
        /// </summary>
        /// <remarks>Method to kill file process in case user explicitly want to kill the transall process</remarks>
        private void KillProcess(string FileName)
        {
            int processId;

            strMessage = "[KillProcess]-";
            if (int.TryParse(FileName.Replace(Key(configKeys.killExtension).ToString(), ""), out processId))
            {
                System.Diagnostics.Process proc = Process.GetProcessById(processId);
                if (proc != null)
                {
                    try
                    {
                        proc.Kill();
                        BuildMessage(String.Format(TransallMessages.KillProcesss,strMessage, proc.Id.ToString(), FileName), true, false);
                    }
                    catch (Exception ex)
                    {
                        BuildMessage(String.Format(TransallMessages.KillProcessError,strMessage, proc.Id.ToString(), ex.Message, ex.StackTrace, FileName), true, false);
                    }
                }
                else
                {
                    BuildMessage(String.Format(TransallMessages.KillProcessNotFound,strMessage, proc.Id.ToString(), FileName), true, false);
                }
            }
        }



        #endregion 

        #region "Output Path Specific Methods"

        private void CreateOutFile(string strPath, FileInfo fi)
        {
            string strOutFilePath = Path.Combine(strPath,fi.Name.Replace(fi.Extension, Key(configKeys.outputTriggerExtension)));

            using (StreamWriter sw = File.CreateText(strOutFilePath))
            {
                sw.WriteLine("DONE");
            }

        }

        void OutputFileCreated(string outputDirectory, FileInfo file)
        {
            DataView dv;
            ProcessAction pa = new ProcessAction(Key(configKeys.errorEmail));

            if (!IsFileLocked(file) && file.Length > 0)
            {
                try
                {
                    string str = file.DirectoryName.Substring(file.DirectoryName.LastIndexOf("\\") + 1);

                    // Another detail to be provided to identify the file. This can be done from the output filename the first 4 characters
                    string orig = file.FullName.Substring(file.FullName.LastIndexOf("\\") + 1);

                    dv = Assurant.ElitaPlus.BusinessObjectsNew.TransallMapping.GetListByOutputDirectoryAndFile(str, orig, null);

                    if (dv.Count == 1)
                    {
                        if (file.Name.Contains(TransallMessages.ReportLog))
                        {
                            BuildMessage(string.Format(TransallMessages.ProcessReportLogs,file.FullName), false, false);
                            pa.ProcessLogs(file.FullName, dv);
                        }
                        else if (file.Name.Contains(TransallMessages.RejectedRecords))
                        {
                            BuildMessage(string.Format(TransallMessages.ProcessReportRejections, file.FullName), false, false);
                            pa.ProcessRejects(file.FullName, dv);
                        }
                        else
                        {
                            BuildMessage(string.Format(TransallMessages.ProcessUpload, file.FullName), false, false);
                            pa.ProcessOutput(file.FullName, dv);
                        }

                        MoveOutputProcessed(file, Path.Combine(Path.Combine(this.RootPath, this.CurrentRegion), Key(configKeys.processedFolder)));
                        BuildMessage(string.Format(TransallMessages.FTPTransfer, file.Name), true, false);

                    }
                    else
                    {
                        string err = String.Format(TransallMessages.OutputMappingError, file.Name, dv.Count.ToString());
                        BuildMessage(String.Format(TransallMessages.OutputMappingError, file.Name, dv.Count.ToString()), false, false);
                        BuildMessage(String.Format(TransallMessages.OutputMappingError, file.Name, dv.Count.ToString()), true, false);
                        pa.ProcessErrors(file, err, DBLogger);
                        MoveOutputProcessed(file, Path.Combine(Path.Combine(this.RootPath, this.CurrentRegion), Key(configKeys.errorFolder)));
                        AddLog(false, true, file, LogType.Error);
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(TransallMessages.OutputException, ex.Message, Environment.NewLine, ex.StackTrace, file.FullName);
                    BuildMessage(err, false, false);

                    BuildMessage(err,true, false);
                    pa.ProcessErrors(file, err, DBLogger);
                    
                    MoveOutputProcessed(file, Path.Combine(Path.Combine(this.RootPath, this.CurrentRegion), Key(configKeys.errorFolder)));
                    AddLog(false, true, file, LogType.Error);

                }
            }

        }

        private void MoveOutputProcessed(FileInfo fi, string strDestination)
        {
            FileInfo fiTrigger;
            FileInfo fiOutput;
            if (!IsFileLocked(fi))
            {
         
                if (File.Exists(Path.Combine(strDestination, fi.Name.Replace(fi.Extension, Key(configKeys.outputTriggerExtension)))))
                {
                    fiTrigger = new FileInfo(Path.Combine(strDestination,
                                                            fi.Name.Replace(fi.Extension, Key(configKeys.outputTriggerExtension))
                                            ));
                    fiTrigger.Delete();
                }

                if (File.Exists(Path.Combine(strDestination, fi.Name)))
                {
                    fiOutput = new FileInfo(Path.Combine(strDestination, fi.Name));
                    fiOutput.Delete();
                }

                fiTrigger = new FileInfo(Path.Combine(fi.Directory.FullName,
                                                       fi.Name.Replace(fi.Extension, Key(configKeys.outputTriggerExtension))
                                                    ));
                fi.MoveTo(Path.Combine(strDestination, fi.Name));
                fiTrigger.MoveTo(Path.Combine(strDestination, fiTrigger.Name));
            }
        }

        #endregion



        #endregion 

       
    }



    public static class configKeys
    {
        public static string inputDirectory = "MONITOR_IN_DIRECTORY";
        public static string outputDirectory = "MONITOR_OUT_DIRECTORY";
        public static string fileExtension = "FILE_EXTENSION";
        public static string processExtension = "PROCESS_EXT";
        public static string killExtension = "KILL_EXT";
        public static string commandLineCall = "COMMAND_LINE";
        public static string region = "NA";
        public static string userID = "NETWORKID";
        public static string eventLog = "EVENTLOG";
        public static string eventSource = "EVENTSOURCE";
        public static string delayTime = "DELAYTIME";
        public static string outputDelayTime = "OUTPUT_SEND_FILE_DELAY";
        public static string outputTriggerExtension = "OUTPUT_FILE_TRIGGER_EXTENSION";
        public static string outputExtension = "OUTPUT_FILE_EXTENSION";
        public static string timerInterval = "TIMER_INTERVAL";
        public static string errorEmail = "ERRORMAIL";
        public static string tempFolder = "TEMP_FOLDER";
        public static string logFolder = "LOG_FOLDER";
        public static string logSwitch = "LOG_SWITCH";
        public static string processedFolder = "PROCESSED_FOLDER";
        public static string errorFolder = "ERROR_FOLDER";
        public static string errorExtension = "ERROR_EXT";
        public static string logExtension = "LOG_EXT";
        public static string logDB = "LOG_DB";
        public static string bypassKeyword = "BYPASS_KEYWORD";
        public static string trcExtn = "TRC_EXT";
        public static string cleanOutput = "CLEAN_OUTPUT";
        public static string daystoKeep = "DAYS_TO_KEEP";
        public static string logDBProd = "LOG_DB_PROD";
    }



    public static class TransallMessages
    {
        public static string DefaultLog = "Elita Transall Service Log - {0}";
        public static string ReportLog = "ReportLog";
        public static string RejectedRecords = "RejectRecords";
        public static string GenericLogFileName = "TransallLog_{0}_{1}";
        public static string PollingStart = "{0} Input Folder Polling Started at: {1}";
        public static string DuplicateFile = "{0} Duplicate File Received for Processing! File:{1} Already available in {2} folder. Renamed existing file with [.old] extension";
        public static string InvalidExtension = "{0} Error: File with invalid extension! Accepted extensions are-[{1}] Received- [{2}]";
        public static string PickedForProcessing = "{0} Picked {1} files from Input folder of {2} region for Processing further. Nearstore folder has file with more Log details.";
        public static string ProcessedRegion = "Processed files from region {0}";
        public static string RegionErrors = "{0} {1} Files in {2} region had error. Log file on Nearstore location folder - [Region\\Error] contains details";
        public static string TransallStart = "{0} Transall Processing started for Region- {1} at {2} ";
        public static string TransallEnd = "{0} Transall Process Initiated for {1} file(s) from {2} Region. File wise Log available at Nearstore Location :{2}";
        public static string TransallFileStart = " {0} Transall Process started for File - {1} at {2} ";
        public static string TransallKill = "{0} Process killed for file -{1}";
        public static string EmailError = "Error Sending Email notifying problem file";
        public static string TransallMappingIssue = " {0} Transall File mapping not found in database or multiple records found.  Check configuration. There are {2} mapping record(s) configured for file: {1} .Please Correct in Elita from Transall Mapping screen";
        public static string TransallFileException = "{0} Error Processing Incoming file(s): {1} {2} Message:{3} {2} Trace:{4}";
        public static string TransallSourceException = "{0} Error Processing Incoming Source file: {1} {2} Message:{3} {2} Trace:{4}";
        public static string TransallFileSkip = "{0} File did not make it into logic.  Either, empty, readonly, or does not exist.  File: {1}  | Length {2} | ReadOnly Status {3} ";
        public static string TransallLaunch = "Launching Transall {0} for file(s) {2} with process number: {1}.  To stop process, place file named {1}.kill in the input directory";
        public static string TransallMoveToOutput = "{0} - {1} Copying Processed file(s) in Output directory for Region - {2}";
        public static string TransallOPTransferComplete = "{0} Copied {1} file(s) for Region - {2} to Output folder";
        public static string KillProcesss = "{0} Killing Process Id: {1} for File - {2}";
        public static string KillProcessError = "{0} Error killing processId: {1} {2} {3}  for File - {4}";
        public static string KillProcessNotFound = "{0} Process Id: {1} Not Found  for File - {2}";
        public static string LoginError = "LOGIN FAILED for {0} Region: {1}, Connection Type: {2}";
        public static string RegionLoginError = "LOGIN FAILED for {0} Region: {1}, Connection Type: {2}. Please verify the region connection and Put the file in root folder to process";
        public static string FTPTransfer = "All processing complete for file: {0}. Transfered to FTP ";
        public static string OutputMappingError = "Output File mapping name not found in database or multiple records found.  Check configuration.There are {1} mapping record(s) configured for file: {0}";
        public static string OutputException = "Error Processing Outgoing files: {3} {1} {0} {1} {2} ";
        public static string TransallReject = "[{0} {1}] - Transall files Rejects";
        public static string ReportfileError = "Report file is empty or does not exist";
        public static string TransallFilesLoaded = "[{0} {2}] - Transall file(s) Loaded - File: {1}";
        public static string NoMailRecipient = "No mail recipients defined.  mail not sent";
        public static string TransallFileError = "[{0} {2}] - TRANSALL FILES ERRORS - File:  {1}";
        public static string FTPAccessError = "Error with FTP process : {0}   Error {1}  {2}";
        public static string FTPAccessErrorTrace = "Error with FTP process : {0} and {1}.  Error {2} {3}";
        public static string FTPProcessError = "Error with FTP process : {0} from Filestream.  Error {1} {2}";
        public static string BypassTransall = "{0} File {1} has been transfered to Output directory. As per setting Transall processing is not required for this file.";
        public static string TransallFileWait = "{0} File {1} has not received its pair file. Skipping its processing in this cycle.";
        public static string ProcessReportLogs = "Started Processing Logs for {0}";
        public static string ProcessReportRejections = "Started Processing Rejections for {0}";
        public static string ProcessUpload = "Started Uploading Process for {0}";
        public static string LogCodeInput = "{0} | INPUT";
        public static string LogCodeOutput = "{0} | OUTPUT";
        public static string FileDeleted = "Deleted old file:{0} from Region:{1} & Folder: {2} File was generated On: {3}";
    }
}
