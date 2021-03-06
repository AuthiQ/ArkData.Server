﻿using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ArkData
{
    /// <summary>
    /// The container for the ARK data.
    /// </summary>
    public partial class ArkDataContainer
    {
        /// <summary>
        /// Loads the profile data for all users from the steam service
        /// </summary>
        /// <returns>The async task context.</returns>
        public async Task LoadSteamAsync(string apiKey)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < Players.Count; i++)
                builder.Append(Players[i].SteamId + ",");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://api.steampowered.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(string.Format("ISteamUser/GetPlayerSummaries/v0002/?key={0}&steamids={1}", apiKey, builder.ToString()));
                if (response.IsSuccessStatusCode)
                    using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        LinkSteamProfiles(await reader.ReadToEndAsync());
                    }
                else
                    throw new System.Net.WebException("The Steam API request was unsuccessful. Are you using a valid key?");

                response = await client.GetAsync(string.Format("ISteamUser/GetPlayerBans/v1/?key={0}&steamids={1}", apiKey, builder.ToString()));
                if (response.IsSuccessStatusCode)
                    using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        LinkSteamBans(await reader.ReadToEndAsync());
                    }
                else
                    throw new System.Net.WebException("The Steam API request was unsuccessful. Are you using a valid key?");
            }
            SteamLoaded = true;
        }

        /// <summary>
        /// Fetches the player server status. Can only be done after fetching Steam player data.
        /// </summary>
        /// <param name="ipString">The IP of the server.</param>
        /// <param name="port">The port of the server.</param>
        /// <returns>The async task context.</returns>
        public Task LoadOnlinePlayersAsync(string ipString, int port)
        {
            if (SteamLoaded)
                return Task.Run(() =>
                {
                    LinkOnlinePlayers(ipString, port);
                });
            else
                throw new System.Exception("The Steam user data should be loaded before the server status can be checked.");
        }

        /// <summary>
        /// Instantiates the ArkDataContainer and parses all the user-data files
        /// </summary>
        /// <param name="directory">The directory containing the profile and tribe files.</param>
        /// <returns>The async task context containing the resulting container.</returns>
        public static async Task<ArkDataContainer> CreateAsync(string directory)
        {
            if (!Directory.Exists(directory))
                throw new DirectoryNotFoundException("The ARK data directory couldn't be found.");

            var playerFiles = Directory.GetFiles(directory).Where(p => p.ToLower().Contains(".arkprofile")).ToArray();
            var tribeFiles = Directory.GetFiles(directory).Where(p => p.ToLower().Contains(".arktribe")).ToArray();

            if (playerFiles.Length == 0 && tribeFiles.Length == 0)
                throw new FileLoadException("The directory did not contain any of the parseable files.");

            var container = new ArkDataContainer();

            for (var i = 0; i < playerFiles.Length; i++)
                container.Players.Add(await Parser.ParsePlayerAsync(playerFiles[i]));

            for (var i = 0; i < tribeFiles.Length; i++)
                container.Tribes.Add(await Parser.ParseTribeAsync(tribeFiles[i]));

            container.LinkPlayerTribe();

            return container;
        }
    }
}
