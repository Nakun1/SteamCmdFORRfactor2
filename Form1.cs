using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InstallPackageRF2
{
    public partial class Form1 : Form
    {
        public string PathRFactor = Environment.CurrentDirectory + @"\..\rfactor2-dedicated";

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Executes a shell command synchronously.
        /// </summary>
        /// <param name="command">string command</param>
        /// <param name="keepWindowOpen">si vrai, la fenêtre cmd reste ouverte après l'exécution (utilise /k au lieu de /c)</param>
        /// <remarks>Trouvé ici: https://stackoverflow.com/a/59235057</remarks>
        /// <returns>string, as output of the command.</returns>
        private static void ExecuteCommandSync(object command, bool keepWindowOpen = false)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
                // /k garde la fenêtre ouverte pour laisser la sortie visible.
                string switchArg = keepWindowOpen ? "/k " : "/c ";
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", switchArg + command);
                // The following commands are needed to redirect the standard output. 
                //This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = false;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = false;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Si la fenêtre doit rester ouverte (/k), on ne bloque pas l'UI en attendant sa fermeture.
                if (!keepWindowOpen)
                {
                    proc.WaitForExit();
                }

                //// Get the output into a string
                //string result = proc.StandardOutput.ReadToEnd();

                //// Display the command output.
                //Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                // Log the exception
                MessageBox.Show("ExecuteCommandSync failed" + objException.Message);
            }
        }

        private void btnUpdateRF2_Click(object sender, EventArgs e)
        {
            try
            {
                string path = '"' + Environment.CurrentDirectory + @"\steamcmd.exe" + '"';
                string cmd = path + @" +force_install_dir ../rFactor2-Dedicated +login anonymous +app_update 400300 +quit";
                //this.txtDebug.Text = cmd;
                ExecuteCommandSync(cmd, keepWindowOpen: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnOpenWorkshop_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://steamcommunity.com/app/365960/workshop/");
        }

        private void btnDlMod_Click(object sender, EventArgs e)
        {
            long idMod;

            try
            {
                if (string.IsNullOrEmpty(this.txtIdMod.Text.Trim()))
                {
                    MessageBox.Show("Il faut mettre un ID boubourse!");
                }
                else if (!long.TryParse(this.txtIdMod.Text.Trim(), out idMod))
                {
                    MessageBox.Show("L'ID doit être un nombre ptain!");
                }
                else
                {
                    string pathSteamcmd = '"' + Environment.CurrentDirectory + @"\steamcmd.exe" + '"';
                    string cmd = pathSteamcmd + " +login anonymous +workshop_download_item 365960 " + idMod + " +quit";
                    ExecuteCommandSync(cmd);

                    //this.txtDebug.Text = cmd;

                    string pathMod =  Environment.CurrentDirectory + @"\steamapps\workshop\content\365960\" + idMod ;
                    string pathPackage = this.PathRFactor + @"\Packages\";
                    if (!Directory.Exists(pathMod))
                    {
                        throw new Exception("Le mod n'a pas été telechargé, vérifier l'ID.");
                    }

                    // On déplace les fichiers téléchargés dans le dossier Packages de RF2
                    List<string> files = Directory.GetFiles(pathMod).ToList();
                    FileInfo fileInfo;

                    foreach (string file in files)
                    {
                        fileInfo = new FileInfo(file);

                        string pathPackageFull =  pathPackage + @"\" + fileInfo.Name ;

                        if (File.Exists(pathPackageFull))
                        {
                            File.Delete(pathPackageFull);
                        }

                        File.Move(file, pathPackageFull);

                        // Installer le mod avec modmgr, ne fonctionne pas :(
                        //try
                        //{
                        //    cmd = this.PathRFactor + @"\Bin64\ModMgr.exe -i" + fileInfo.Name
                        //        + @" -p" + pathPackage + @" -d" + this.PathRFactor;
                        //    ExecuteCommandSync(cmd);
                        //}
                        //catch
                        //{
                        //    // On ne fait rien pour ne pas bloquer le reste du téléchargement, au pire on installera le mod manuellement
                        //}
                    }

                    Directory.Delete(pathMod);

                    MessageBox.Show("Ayé");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
    }
}
