using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FSW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPath.Text))
                this.fileSystemWatcher1.Path = this.txtPath.Text;
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            this.txtResults.Text += "File " + e.FullPath + " changed. " + Environment.NewLine;
        }

        private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            this.txtResults.Text += "File " + e.FullPath + " created. " + Environment.NewLine;
        }

        private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            this.txtResults.Text += "File " + e.FullPath + " deleted. " + Environment.NewLine;
        }

        private void fileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
        {            
            this.txtResults.Text += "File " + e.OldFullPath + " renamed to " + e.FullPath + Environment.NewLine;
        }

    }
}