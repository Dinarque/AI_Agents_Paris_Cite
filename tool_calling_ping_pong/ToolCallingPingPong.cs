using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ToolCallingPingPongLauncher
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string htmlPath = Path.Combine(exeDir, "index.html");

            if (!File.Exists(htmlPath))
            {
                MessageBox.Show(
                    "Could not find index.html in:\n" + htmlPath,
                    "Function Calling Ping-Pong Visualizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            string fileUri = new Uri(htmlPath).AbsoluteUri;

            string[] appBrowsers = new string[]
            {
                @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
                @"C:\Program Files\Microsoft\Edge\Application\msedge.exe",
                @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            };

            foreach (string browser in appBrowsers)
            {
                if (File.Exists(browser))
                {
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = browser,
                            Arguments = string.Format("--app=\"{0}\" --window-size=1180,820", fileUri),
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                        return;
                    }
                    catch
                    {
                        // Fall through to default browser
                    }
                }
            }

            try
            {
                Process.Start(new ProcessStartInfo(htmlPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to launch visualizer:\n" + ex.Message,
                    "Function Calling Ping-Pong Visualizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}

