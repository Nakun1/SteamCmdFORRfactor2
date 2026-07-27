using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InstallPackageRF2
{
    public partial class Form1 : Form
    {
        private class GameConfig
        {
            public string DisplayName;
            public string SteamAppId;
            public string InstallDirRelative;
            public bool HasWorkshop;
            public string WorkshopAppId;
        }

        private static readonly GameConfig RFactor2Config = new GameConfig
        {
            DisplayName = "rFactor 2",
            SteamAppId = "400300",
            InstallDirRelative = "../rFactor2-Dedicated",
            HasWorkshop = true,
            WorkshopAppId = "365960"
        };

        private static readonly GameConfig AMS2Config = new GameConfig
        {
            DisplayName = "Automobilista 2",
            SteamAppId = "1338040",
            InstallDirRelative = "../AMS2-Dedicated",
            HasWorkshop = false,
            WorkshopAppId = null
        };

        private GameConfig CurrentGame
        {
            get { return this.rbAMS2.Checked ? AMS2Config : RFactor2Config; }
        }

        private string PathServer
        {
            get { return Environment.CurrentDirectory + @"\" + this.CurrentGame.InstallDirRelative; }
        }

        private const string SteamCmdDownloadUrl = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(this.Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetControlsEnabled(false);
            this.EnsureSteamCmdUpToDate();
            this.SetControlsEnabled(true);
            this.UpdateUiForSelectedGame();
        }

        private void SetControlsEnabled(bool enabled)
        {
            this.btnUpdateServer.Enabled = enabled;
            this.btnOpenWorkshop.Enabled = enabled;
            this.btnDlMod.Enabled = enabled;
            this.txtIdMod.Enabled = enabled;
            this.rbRFactor2.Enabled = enabled;
            this.rbAMS2.Enabled = enabled;
        }

        /// <summary>
        /// Télécharge et installe steamcmd.exe s'il est absent, puis force sa mise à jour
        /// (steamcmd se met à jour tout seul dès qu'on l'exécute).
        /// </summary>
        private void EnsureSteamCmdUpToDate()
        {
            string originalTitle = this.Text;
            string steamCmdPath = Environment.CurrentDirectory + @"\steamcmd.exe";

            if (!File.Exists(steamCmdPath))
            {
                this.Text = originalTitle + " - Téléchargement de SteamCMD...";
                this.Cursor = Cursors.WaitCursor;

                string zipPath = Environment.CurrentDirectory + @"\steamcmd.zip";
                try
                {
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        client.DownloadFile(SteamCmdDownloadUrl, zipPath);
                    }

                    // tar.exe est intégré à Windows depuis la mise à jour 1803 et sait extraire les .zip.
                    System.Diagnostics.ProcessStartInfo extractInfo = new System.Diagnostics.ProcessStartInfo(
                        "tar", "-xf \"" + zipPath + "\" -C \"" + Environment.CurrentDirectory + "\"");
                    extractInfo.UseShellExecute = false;
                    extractInfo.CreateNoWindow = true;
                    using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(extractInfo))
                    {
                        proc.WaitForExit();
                    }

                    if (!File.Exists(steamCmdPath))
                    {
                        MessageBox.Show("L'extraction de SteamCMD a échoué (tar.exe introuvable ou zip invalide).");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Impossible de télécharger/installer SteamCMD automatiquement : " + ex.Message);
                    return;
                }
                finally
                {
                    if (File.Exists(zipPath))
                    {
                        File.Delete(zipPath);
                    }
                    this.Cursor = Cursors.Default;
                    this.Text = originalTitle;
                }
            }

            this.Text = originalTitle + " - Mise à jour de SteamCMD...";
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ExecuteCommandSync('"' + steamCmdPath + '"' + " +quit");
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Text = originalTitle;
            }
        }

        /// <summary>
        /// Executes a shell command synchronously.
        /// </summary>
        /// <param name="command">string command</param>
        /// <remarks>Trouvé ici: https://stackoverflow.com/a/59235057</remarks>
        private static void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
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
                proc.WaitForExit();
            }
            catch (Exception objException)
            {
                // Log the exception
                MessageBox.Show("ExecuteCommandSync failed" + objException.Message);
            }
        }

        /// <summary>
        /// Exécute une commande sans fenêtre visible, en capturant toute sa sortie (stdout + stderr).
        /// </summary>
        /// <returns>true si une erreur a été détectée (code de sortie non nul ou "ERROR" dans la sortie).</returns>
        private static bool ExecuteCommandCaptureOutput(string command, out string output)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
            {
                proc.StartInfo = procStartInfo;
                proc.OutputDataReceived += (s, e) => { if (e.Data != null) sb.AppendLine(e.Data); };
                proc.ErrorDataReceived += (s, e) => { if (e.Data != null) sb.AppendLine(e.Data); };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();

                output = sb.ToString();
                return proc.ExitCode != 0 || output.IndexOf("ERROR", StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

        /// <summary>
        /// Affiche une sortie de commande dans une fenêtre dédiée avec défilement.
        /// </summary>
        private static void ShowOutputDialog(string title, string output)
        {
            using (Form dialog = new Form())
            {
                dialog.Text = title;
                dialog.Size = new System.Drawing.Size(700, 450);
                dialog.StartPosition = FormStartPosition.CenterParent;

                TextBox txtOutput = new TextBox();
                txtOutput.Multiline = true;
                txtOutput.ReadOnly = true;
                txtOutput.ScrollBars = ScrollBars.Vertical;
                txtOutput.WordWrap = false;
                txtOutput.Dock = DockStyle.Fill;
                txtOutput.Font = new System.Drawing.Font("Consolas", 9F);
                txtOutput.Text = output;

                dialog.Controls.Add(txtOutput);
                dialog.ShowDialog();
            }
        }

        private void rbGame_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateUiForSelectedGame();
        }

        private void UpdateUiForSelectedGame()
        {
            GameConfig game = this.CurrentGame;
            this.label1.Text = "Met à jour le serveur si nouvelle version d'" + game.DisplayName;

            // Pas de Steam Workshop pour ce jeu : on désactive les fonctionnalités liées.
            this.btnOpenWorkshop.Enabled = game.HasWorkshop;
            this.btnDlMod.Enabled = game.HasWorkshop;
            this.txtIdMod.Enabled = game.HasWorkshop;
            this.label2.Enabled = game.HasWorkshop;
        }

        private void btnUpdateServer_Click(object sender, EventArgs e)
        {
            try
            {
                GameConfig game = this.CurrentGame;
                string path = '"' + Environment.CurrentDirectory + @"\steamcmd.exe" + '"';
                string cmd = path + @" +force_install_dir " + game.InstallDirRelative
                    + " +login anonymous +app_update " + game.SteamAppId + " +quit";
                //this.txtDebug.Text = cmd;

                string output;
                bool hasError = ExecuteCommandCaptureOutput(cmd, out output);
                if (hasError)
                {
                    ShowOutputDialog("Erreur lors de la mise à jour de " + game.DisplayName, output);
                }
                else if (output.IndexOf("already up to date", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show("Pas de mise à jour à faire, " + game.DisplayName + " est déjà à jour.");
                }
                else
                {
                    MessageBox.Show("Mise à jour de " + game.DisplayName + " réussie.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnOpenWorkshop_Click(object sender, EventArgs e)
        {
            GameConfig game = this.CurrentGame;
            if (!game.HasWorkshop)
            {
                MessageBox.Show(game.DisplayName + " n'a pas de Steam Workshop.");
                return;
            }

            System.Diagnostics.Process.Start("https://steamcommunity.com/app/" + game.WorkshopAppId + "/workshop/");
        }

        private void btnDlMod_Click(object sender, EventArgs e)
        {
            long idMod;
            GameConfig game = this.CurrentGame;

            try
            {
                if (!game.HasWorkshop)
                {
                    MessageBox.Show(game.DisplayName + " n'a pas de Steam Workshop.");
                }
                else if (string.IsNullOrEmpty(this.txtIdMod.Text.Trim()))
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
                    string cmd = pathSteamcmd + " +login anonymous +workshop_download_item " + game.WorkshopAppId + " " + idMod + " +quit";
                    ExecuteCommandSync(cmd);

                    //this.txtDebug.Text = cmd;

                    string pathMod =  Environment.CurrentDirectory + @"\steamapps\workshop\content\" + game.WorkshopAppId + @"\" + idMod ;
                    string pathPackage = this.PathServer + @"\Packages\";
                    if (!Directory.Exists(pathMod))
                    {
                        throw new Exception("Le mod n'a pas été telechargé, vérifier l'ID.");
                    }

                    // On déplace les fichiers téléchargés dans le dossier Packages du serveur
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
                        //    cmd = this.PathServer + @"\Bin64\ModMgr.exe -i" + fileInfo.Name
                        //        + @" -p" + pathPackage + @" -d" + this.PathServer;
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
