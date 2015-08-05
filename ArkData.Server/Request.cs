using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ArkData.Server.Data;
using System.Collections.Generic;

namespace ArkData.Server
{
    static class Request
    {
        public async static void Authenticate(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (request.HttpMethod == "POST")
            {
                var input = JsonConvert.DeserializeObject<Models.TokenRequest>
                    (new StreamReader(request.InputStream).ReadToEnd());
                using (var ctx = new DataContext())
                {
                    var user = ctx.XUsers.SingleOrDefault(u => u.Username == input.Username);
                    if (user != null)
                    {
                        if (input.Signature == user.Password)
                        {
                            response.ContentType = "application/json";
                            var token = ctx.XTokens.SingleOrDefault(t => t.Username == user.Username);

                            byte[] serialized;
                            if (token != null)
                            {
                                serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                                {
                                    Token = token.Token
                                }));
                            }
                            else
                            {
                                token = new XToken()
                                {
                                    Username = user.Username,
                                    Token = (Guid.NewGuid().ToString() + Guid.NewGuid().ToString()).Replace("-", "").ToLower(),
                                    Created = DateTime.Now
                                };
                                ctx.XTokens.Add(token);

                                await ctx.SaveChangesAsync();

                                serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                                {
                                    Token = token.Token
                                }));
                            }
                            response.ContentEncoding = Encoding.UTF8;
                            response.ContentLength64 = serialized.Length;
                            response.OutputStream.Write(serialized, 0, serialized.Length);

                            response.StatusCode = (int)HttpStatusCode.OK;
                        }
                        else
                        {
                            response.StatusCode = (int)HttpStatusCode.Forbidden;
                            Program.cfgForm.Log(string.Format("Failed authorization attempt from {0}", request.RemoteEndPoint.ToString()));
                        }
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        Program.cfgForm.Log(string.Format("Failed authorization attempt from {0}", request.RemoteEndPoint.ToString()));
                    }
                }
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                Program.cfgForm.Log(string.Format("Failed authorization attempt from {0}", request.RemoteEndPoint.ToString()));
            }

            response.OutputStream.Close();
        }

        public static void Search(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (Program.cfgForm.useAuthentication)
                if (!CheckToken(request.Headers["Authorization"].ToLower().Replace("bearer ", "")))
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    response.OutputStream.Close();
                    Program.cfgForm.Log(string.Format("Failed authorization attempt from {0}", request.RemoteEndPoint.ToString()));
                    return;
                }

            var type = request.QueryString["type"];
            var query = request.QueryString["q"];
            if (type == null || type == string.Empty ||
                query == null || query == string.Empty ||
                (type.ToLower() != "tribe" && type.ToLower() != "player"))
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.StatusDescription = "Parameters 'q' and 'type' required for this request.";
                response.OutputStream.Close();
                return;
            }

            response.ContentType = "application/json";
            if (type.ToLower() == "player")
            {
                List<Models.SimplePlayer> result;
                lock (Server.containerLock)
                    result = Server.container.Players.Where(
                        p => p.CharacterName.ToLower().Contains(query) || p.SteamName.ToLower().Contains(query)
                        ).Select(p => new Models.SimplePlayer()
                        {
                            Id = p.Id,
                            CharacterName = p.CharacterName,
                            SteamName = p.SteamName,
                            AvatarUrl = p.AvatarUrl,
                            Level = p.Level,
                            Online = p.Online
                        }).ToList();
                var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));

                response.ContentEncoding = Encoding.UTF8;
                response.ContentLength64 = serialized.Length;
                response.OutputStream.Write(serialized, 0, serialized.Length);

                response.StatusCode = (int)HttpStatusCode.OK;
            }
            if (type.ToLower() == "tribe")
            {
                List<Models.SimpleTribe> result;
                lock (Server.containerLock)
                    result = Server.container.Tribes.Where(
                        t => t.Name.ToLower().Contains(query)
                        ).Select(t => new Models.SimpleTribe()
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Owner = t.OwnerId.Value,
                            Members = t.Players.Count
                        }).ToList();
                var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));

                response.ContentEncoding = Encoding.UTF8;
                response.ContentLength64 = serialized.Length;
                response.OutputStream.Write(serialized, 0, serialized.Length);

                response.StatusCode = (int)HttpStatusCode.OK;
            }

            response.OutputStream.Close();
        }

        public static void Tribe(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (Program.cfgForm.useAuthentication)
                if (!CheckToken(request.Headers["Authorization"].ToLower().Replace("bearer ", "")))
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    response.OutputStream.Close();
                    Program.cfgForm.Log(string.Format("Failed authorization attempt from {0}", request.RemoteEndPoint.ToString()));
                    return;
                }

            var id = request.QueryString["id"];

            if (id == string.Empty || id == null)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.StatusDescription = "Parameter 'id' required for this request.";
                response.OutputStream.Close();
                return;
            }

            ArkData.Tribe a_tribe;
            lock (Server.containerLock)
                a_tribe = Server.container.Tribes.SingleOrDefault(t => t.Id == Convert.ToInt32(id));

            if (a_tribe != null)
            {
                var tribe = new Models.Tribe()
                {
                    Id = a_tribe.Id,
                    Name = a_tribe.Name,
                    Owner = new Models.SimplePlayer()
                    {
                        Id = a_tribe.Owner.Id,
                        CharacterName = a_tribe.Owner.CharacterName,
                        SteamName = a_tribe.Owner.SteamName,
                        AvatarUrl = a_tribe.Owner.AvatarUrl,
                        Level = a_tribe.Owner.Level,
                        Online = a_tribe.Owner.Online
                    },
                    Created = a_tribe.FileCreated,
                    Members = a_tribe.Players.Select(p => new Models.SimplePlayer()
                    {
                        Id = p.Id,
                        CharacterName = p.CharacterName,
                        SteamName = p.SteamName,
                        AvatarUrl = p.AvatarUrl,
                        Level = p.Level,
                        Online = p.Online
                    }).ToList()
                };

                var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tribe));

                response.ContentType = "application/json";
                response.ContentEncoding = Encoding.UTF8;
                response.ContentLength64 = serialized.Length;
                response.OutputStream.Write(serialized, 0, serialized.Length);

                response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.StatusDescription = string.Format("The tribe with id: {0} couldn't be found.", id);
            }
            response.OutputStream.Close();
        }

        public static void Player(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (Program.cfgForm.useAuthentication)
                if (!CheckToken(request.Headers["Authorization"].ToLower().Replace("bearer ", "")))
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    response.OutputStream.Close();
                    Program.cfgForm.Log(string.Format("Failed authorization attempt from {0}", request.RemoteEndPoint.ToString()));
                    return;
                }

            var id = request.QueryString["id"];

            if (id == string.Empty || id == null)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.StatusDescription = "Parameter 'id' required for this request.";
                response.OutputStream.Close();
                return;
            }

            ArkData.Player player;
            lock (Server.containerLock)
                player = Server.container.Players.SingleOrDefault(p => p.Id == Convert.ToInt64(id));

            if (player != null)
            {
                var @out = new Models.Player()
                {
                    Id = player.Id,
                    CharacterName = player.CharacterName,
                    SteamName = player.SteamName,
                    SteamId = player.SteamId,
                    AvatarUrl = player.AvatarUrl,
                    ProfileUrl = player.ProfileUrl,
                    Level = player.Level,
                    CommunityBanned = player.CommunityBanned,
                    VACBanned = player.VACBanned,
                    DaysSinceLastBan = player.DaysSinceLastBan,
                    NumberOfGameBans = player.NumberOfGameBans,
                    NumberOfVACBans = player.NumberOfVACBans,
                    Created = player.FileCreated,
                    Online = player.Online,
                    Tribe = new Models.SimpleTribe()
                    {
                        Id = player.Tribe.Id,
                        Name = player.Tribe.Name,
                        Owner = player.Tribe.OwnerId.Value,
                        Members = player.Tribe.Players.Count
                    }
                };

                var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@out));

                response.ContentType = "application/json";
                response.ContentEncoding = Encoding.UTF8;
                response.ContentLength64 = serialized.Length;
                response.OutputStream.Write(serialized, 0, serialized.Length);

                response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.StatusDescription = string.Format("The tribe with id: {0} couldn't be found.", id);
            }
            response.OutputStream.Close();
        }

        public static void OnlinePlayers(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (Program.cfgForm.useAuthentication)
                if (!CheckToken(request.Headers["Authorization"].ToLower().Replace("bearer ", "")))
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    response.OutputStream.Close();
                    Program.cfgForm.Log(string.Format("Failed authorization attempt from {0}", request.RemoteEndPoint.ToString()));
                    return;
                }

            List<Models.SimplePlayer> players;
            lock (Server.containerLock)
                players = Server.container.Players.Where(p => p.Online).Select(p => new Models.SimplePlayer()
                {
                    Id = p.Id,
                    CharacterName = p.CharacterName,
                    SteamName = p.SteamName,
                    AvatarUrl = p.AvatarUrl,
                    Level = p.Level,
                    Online = p.Online
                }).ToList();

            var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(players));

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = serialized.Length;
            response.OutputStream.Write(serialized, 0, serialized.Length);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.OutputStream.Close();
        }

        private static bool CheckToken(string token)
        {
            return new DataContext().XTokens.Any(t => t.Token == token);
        }
    }
}
