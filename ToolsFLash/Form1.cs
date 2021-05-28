using Microsoft.VisualBasic.FileIO;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ToolsFLash
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string comp32 = @"C:\Windows\System32\Macromed\Flash\Flash.ocx";
            string comp64 = @"C:\Windows\SysWOW64\Macromed\Flash\Flash.ocx";

            if (!File.Exists(comp32) && !File.Exists(comp64))
            {
                txtConsole.AppendText("FILE COMPONENT TIDAK DITEMUKAN! \n" + Environment.NewLine);
            } else
            {
                txtConsole.AppendText("FILE COMPONENT DITEMUKAN!\n" + Environment.NewLine);
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            string sourceFile32 = Application.StartupPath + @"\include\System32\";
            string sourceFile64 = Application.StartupPath + @"\include\SysWOW64\";
            string destinationFile32 = @"C:\Windows\System32\";
            string destinationFile64 = @"C:\Windows\SysWOW64\";


            string sys32 = @"C:\Windows\System32\Macromed\Flash\Flash.ocx";
            string sys64 = @"C:\Windows\SysWOW64\Macromed\Flash\Flash.ocx";
            try
            {
                FileSystem.CopyDirectory(sourceFile32, destinationFile32, UIOption.AllDialogs);
                FileSystem.CopyDirectory(sourceFile64, destinationFile64, UIOption.AllDialogs);
                try
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    startInfo.FileName = "cmd.exe";
                    startInfo.UseShellExecute = false;
                    startInfo.Verb = "runas";
                    startInfo.RedirectStandardInput = true;
                    startInfo.RedirectStandardOutput = true;
                    process.StartInfo = startInfo;
                    process.Start();
                    process.StandardInput.WriteLine("regsvr32 " + sys32 + " && " + "regsvr32 " + sys64);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();
                    process.WaitForExit();
                    txtConsole.AppendText(process.StandardOutput.ReadToEnd());
                }
                catch (IOException iox)
                {
                    txtConsole.AppendText(iox.Message);
                }

                MessageBox.Show("DONE");
            }
            catch (IOException iox)
            {
                txtConsole.AppendText(iox.Message);
            }
        }
    }
}
