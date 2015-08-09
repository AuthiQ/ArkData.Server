using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Microsoft.Owin.Hosting;
using System.Runtime.Remoting;

namespace ArkData.Server
{
    public static class Server
    {
        public static ArkDataContainer container;
        public static Object containerLock = new object();

        private static Timer online_refresh;
        private static IDisposable webSvc;

        private static string folder;
        private static string api_key;
        private static string ip_address;
        private static int port;

        private static bool server_running = false;

        public static async void Start(string folder, string api_key,
                                string ip_address, int port, string url)
        {
            try
            {
                Program.cfgForm.Log("Loading ARK profiles...");
                container = await ArkDataContainer.CreateAsync(folder);

                Program.cfgForm.Log("Linking Steam profiles...");
                await container.LoadSteamAsync(api_key);

                Program.cfgForm.Log("Loading online players...");
                await container.LoadOnlinePlayersAsync(ip_address, port);
                Program.cfgForm.Log(container.Players.Where(p => p.Online).Count() + " players online.");

                Program.cfgForm.Log("Starting HTTP server...");

                webSvc = WebApp.Start<ServerStartup>(url);

                server_running = true;

                online_refresh = new Timer();
                online_refresh.Interval = 180000;
                online_refresh.Tick += async (sender, e) =>
                {
                    try
                    {
                        ArkDataContainer cont;
                        cont = await ArkDataContainer.CreateAsync(folder);
                        await cont.LoadSteamAsync(api_key);
                        await cont.LoadOnlinePlayersAsync(ip_address, port);
                        lock (containerLock)
                            container = cont;
                        Program.cfgForm.Log(container.Players.Where(p => p.Online).Count() + " players online.");
                    }
                    catch (Exception ex)
                    {
                        handleException(ex);
                    }
                };
                online_refresh.Start();

                Program.cfgForm.Log("Server is running.");
            }
            catch (System.Exception ex)
            {
                handleException(ex);
            }
        }

        private static void handleException(Exception ex)
        {
            Program.cfgForm.OpenUI();
            Stop();

            if (ex is System.IO.DirectoryNotFoundException)
            {
                Program.cfgForm.Log("DirectoryNotFoundException Occurred: " + ex.Message);
                return;
            }
            if (ex is System.IO.FileLoadException)
            {
                Program.cfgForm.Log("FileLoadException Occurred: " + ex.Message);
                return;
            }
            if (ex is WebException)
            {
                Program.cfgForm.Log("WebException Occurred: " + ex.Message);
                return;
            }
            if (ex is ServerException)
            {
                Program.cfgForm.Log("ServerException Occurred: " + ex.Message);
                return;
            }
            if(ex.InnerException != null)
            {
                if(ex.InnerException.Message.Contains("The process cannot access the file because it is being used by another process"))
                {
                    Program.cfgForm.Log("Exception Occurred: The server couldn't be started because the port is already in use. " + 
                        "There are known issues with Skype using port 80, you might want to kill Skype and try again.");
                }
                return;
            }

            Program.cfgForm.Log("Exception Occurred: " + ex.Message);
        }

        public static void Restart(string folder, string api_key,
                                string ip_address, int port, string url)
        {
            if (server_running)
                if (MessageBox.Show(
                    "The server is running, this action will restart the server, are you sure?",
                    "Server is running",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                    return;
                else
                {
                    Program.cfgForm.Log("Restarting server...");
                    Start(folder, api_key, ip_address, port, url);
                }
        }

        public static void Stop()
        {
            if (!server_running)
                return;
            else
            {
                webSvc.Dispose();
                online_refresh.Stop();
                online_refresh.Dispose();
                server_running = false;
                ip_address = string.Empty;
                port = 0;
            }
        }
    }
}
