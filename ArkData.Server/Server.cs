using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkData;
using System.Net;
using System.Runtime.Remoting;

namespace ArkData.Server
{
    public static class Server
    {
        public static ArkDataContainer container;
        public static Object containerLock = new object();

        private static Timer online_refresh;
        private static HttpListener listener;
        private static Task server_task;

        private static bool server_running = false;
        private static string ip_address;
        private static int port;

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
                listener = new HttpListener();
                listener.Prefixes.Add(url);
                listener.Start();

                server_running = true;
                server_task = Task.Run(async () =>
                {
                    while (server_running)
                        incoming_request(await listener.GetContextAsync());
                });

                online_refresh = new Timer();
                online_refresh.Interval = 180000;
                online_refresh.Tick += async (sender, e) =>
                {
                    lock (containerLock)
                        container = ArkDataContainer.Create(folder);
                    await container.LoadSteamAsync(api_key);
                    await container.LoadOnlinePlayersAsync(ip_address, port);
                    Program.cfgForm.Log(container.Players.Where(p => p.Online).Count() + " players online.");
                };
                online_refresh.Start();

                Server.ip_address = ip_address;
                Server.port = port;

                Program.cfgForm.Log("Server is running.");
            }
            catch (System.Exception ex)
            {
                Program.cfgForm.OpenUI();

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
                if(ex is ServerException)
                {
                    Program.cfgForm.Log("ServerException Occurred: " + ex.Message);
                    return;
                }

                Program.cfgForm.Log("Exception Occurred: " + ex.Message);
            }
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
                listener.Stop();
                server_running = false;
                ip_address = string.Empty;
                port = 0;
            }
        }

        private static void incoming_request(HttpListenerContext context)
        {
            if (context.Request.RawUrl.ToLower().StartsWith("/authenticate"))
                Request.Authenticate(context.Request, context.Response);
            if (context.Request.RawUrl.ToLower().StartsWith("/search"))
                Request.Search(context.Request, context.Response);
            if (context.Request.RawUrl.ToLower().StartsWith("/player"))
                Request.Player(context.Request, context.Response);
            if (context.Request.RawUrl.ToLower().StartsWith("/tribe"))
                Request.Tribe(context.Request, context.Response);
            if (context.Request.RawUrl.ToLower().StartsWith("/online"))
                Request.OnlinePlayers(context.Request, context.Response);
            else
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
