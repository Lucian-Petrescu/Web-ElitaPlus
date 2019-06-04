using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace WSGService1
{
    public partial class Service1 : ServiceBase
    {

        List<string> FilesToProcess = null;

        System.Timers.Timer timer1 = new System.Timers.Timer();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            this.StartService();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            this.StopService();
        }

        public void StartService()
        {
            FilesToProcess = new List<string>();

            this.fileSystemWatcher1.Path = WServiceGermany.Properties.Settings.Default.LOOKOUT_PATH;

            int interval = int.Parse(WServiceGermany.Properties.Settings.Default.RUN_INTERVAL) * 1000;

            this.timer1.Interval = interval;

            this.timer1.Enabled = false;

            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);

        }

        private bool _RunningEvent = false;
        private bool _RunningProcessFiles = false;

        public void StopService()
        {
            while (this._RunningEvent || this._RunningProcessFiles)
            { }
        }

        private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {

            this._RunningEvent = true;

            System.Diagnostics.Debug.WriteLine("File created: " + e.FullPath);

            FileInfo finfo = new FileInfo(e.FullPath);

            string file_name = finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length);

            if (file_name.ToLower().StartsWith(WServiceGermany.Properties.Settings.Default.STARTS_WITH))
            {

                FilesToProcess.Add(e.FullPath);

            }

            if (FilesToProcess.Count > 0) this.timer1.Enabled = true;

            this._RunningEvent = false;

        }

        private bool FileCanBeRead(string FullPath)
        {
            try
            {

                FileStream f = new FileStream(FullPath, FileMode.Open);
                f.Close();
                f.Dispose();
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void timer1_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.ProcessFiles();
        }

        private void ProcessFiles()
        {

            this.timer1.Enabled = false;

            this._RunningProcessFiles = true;

            List<string> DeletedFiles = new List<string>();

            this.MoveFiles(DeletedFiles);

            this.MarkFilesAsProcessed(DeletedFiles);

            this._RunningProcessFiles = false;

            if (this.FilesToProcess.Count == 0) this.timer1.Enabled = false; else this.timer1.Enabled = true;
        }

        private void MarkFilesAsProcessed(List<string> DeletedFiles)
        {
            if (DeletedFiles.Count > 0)
            {
                foreach (string file in DeletedFiles)
                {
                    int idx = -1;

                    for (int i = 0; i < FilesToProcess.Count; i++)
                    {
                        if (file.Equals(FilesToProcess[i]))
                        {
                            idx = i;
                            break;
                        }
                    }

                    if (idx >= 0)
                    {
                        FilesToProcess.RemoveAt(idx);
                    }
                }

            }
        }

        private void MoveFiles(List<string> DeletedFiles)
        {
            if (FilesToProcess.Count > 0)
            {
                try
                {

                    foreach (string file in FilesToProcess)
                    {
                        if (FileCanBeRead(file))
                        {
                            bool overwrite = false;

                            FileInfo finfo = new FileInfo(file);

                            string file_name = finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length);

                            string TargetPath = WServiceGermany.Properties.Settings.Default.LOOKOUT_PATH + "\\" + "TEMP_ " + file_name;

                            // Create Directory
                            if (!Directory.Exists(TargetPath)) Directory.CreateDirectory(TargetPath);

                            if (WServiceGermany.Properties.Settings.Default.OVERRIDE_FILES.ToLower().Equals("yes")) overwrite = true;

                            // Copy Source File
                            File.Copy(file, TargetPath + "\\" + finfo.Name, overwrite);

                            // Delete Source File - Move
                            File.Delete(file);

                            DeletedFiles.Add(file);

                        }
                    }
                }
                catch
                {
                }
            }

        }
    }
}
