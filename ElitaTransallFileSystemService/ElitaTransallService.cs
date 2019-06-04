using System;
//using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;


namespace ElitaTransallFileSystemService
{

    public partial class ElitaTransallService : ServiceBase
    {

        #region "Declaration"

        private TransallPath ipPath;
        private TransallPath opPath;
        private string strError = string.Empty;
      

#endregion 
       
        public ElitaTransallService()
        {
            InitializeComponent();
        }


        #region "Transall Methods"

        /// <summary>
        /// OnStart
        /// </summary>
        /// <remarks>Starting point of service</remarks>
        protected override void OnStart(string[] args) 
        {
            string strMessage = "Starting Transall service at- " + DateTime.Now.ToString();
            TransallPath.CreateGenericLogFile(strMessage, string.Empty);
            ThreadStart transallThread = new ThreadStart(StartWatchingFiles);
            Thread newThread = new Thread(transallThread);
            newThread.Start();
        }


        /// <summary>
        /// StartWatchingFiles
        /// </summary>
        /// <remarks>Starting point - Processes files from Input folder and then Output folder</remarks>
        private void StartWatchingFiles()
        {
            int period =0;
            string strGenericLogFolder = string.Empty; 
       
            while (true) // Continuously check for the folders 
            {
                try
                {
                    int.TryParse(TransallPath.Key(configKeys.delayTime),out period);
                   
                    using (ipPath = new TransallPath(true))
                    {
                        strGenericLogFolder = ipPath.GenericLogfolder;
                        //1>Move the files from Input Folder to Output folder if got generated in last cycle 
                        ipPath.MoveProcessedFilesToOutput();

                        //2>Check for newly arrived files for processing. Move those files to Temp    
                        ipPath.PickSourceFilesToProcess();

                        //3>Start Processing Files inside TEMP folder
                        ipPath.ProcessTempFolder();

                        //4>Move the files from Input Folder to Output folder
                        ipPath.MoveProcessedFilesToOutput();
                    }

                    using (opPath = new TransallPath(false))
                    {
                        strGenericLogFolder = opPath.GenericLogfolder;
                        //5>check for files available in Output folder. Call FTP process 
                        opPath.ProcessOutputfiles();

                        //6>Clearout TRC files. Move to Processed folder  
                        opPath.ProcessOPTrcFiles();

                        //7> Clear Old Output files
                        opPath.DeleteOldfiles();
                    }

                    System.Threading.Thread.Sleep(period);

                }
                catch (Exception ex)
                {
                    strError = ex.Message + ex.StackTrace; 
                    TransallPath.CreateGenericLogFile(strError, strGenericLogFolder);
                }

            }
        }


        protected override void OnStop()
        {
            string strMessage = "Stopping Transall service at- " + DateTime.Now.ToString();
            TransallPath.CreateGenericLogFile(strMessage, string.Empty);
        }

        #endregion



    }
}
