using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ArkData.Server
{
    public class ArkController : ApiController
    {
        [HttpGet, Authorization]
        public Object Search(string type, string q)
        {
            if (type == null || type == string.Empty ||
                q == null || q == string.Empty ||
                (type.ToLower() != "tribe" && type.ToLower() != "player"))
            {
                throw new Exception("The parameters 'type' and 'q' are required. Parameter 'type' can only be player or tribe.");
            }

            if (type.ToLower() == "player")
                return SearchPlayer(q);
            else
                return SearchTribe(q);
        }

        private List<Models.SimplePlayer> SearchPlayer(string q)
        {
            List<Models.SimplePlayer> result;
            lock (Server.containerLock)
                result = Server.container.Players.Where(
                    p => p.CharacterName.ToLower().Contains(q) || p.SteamName.ToLower().Contains(q)
                    ).Select(p => new Models.SimplePlayer()
                    {
                        Id = p.Id,
                        CharacterName = p.CharacterName,
                        SteamName = p.SteamName,
                        AvatarUrl = p.AvatarUrl,
                        Level = p.Level,
                        Online = p.Online
                    }).ToList();
            return result;
        }

        private List<Models.SimpleTribe> SearchTribe(string q)
        {
            List<Models.SimpleTribe> result;
            lock (Server.containerLock)
                result = Server.container.Tribes.Where(
                    t => t.Name.ToLower().Contains(q)
                    ).Select(t => new Models.SimpleTribe()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Owner = t.OwnerId.Value,
                        Members = t.Players.Count
                    }).ToList();
            return result;
        }

        [HttpGet, Authorization]
        public Models.Player Player(long id)
        {
            ArkData.Player player;
            lock (Server.containerLock)
                player = Server.container.Players.SingleOrDefault(p => p.Id == Convert.ToInt64(id));

            if (player != null)
            {
                return new Models.Player()
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
            }
            else
                throw new Exception("The player with id " + id + " couldn't be found.");
        }

        [HttpGet, Authorization]
        public Models.Tribe Tribe(int id)
        {
            ArkData.Tribe a_tribe;
            lock (Server.containerLock)
                a_tribe = Server.container.Tribes.SingleOrDefault(t => t.Id == id);

            if (a_tribe != null)
            {
                return new Models.Tribe()
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
            }
            else
                throw new Exception("The tribe with id " + id + " couldn't be found.");
        }

        [HttpGet, Authorization]
        public List<Models.SimplePlayer> Online()
        {
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
            return players;
        }

        [HttpPost]
        public async Task<Models.TokenResponse> Authenticate([FromBody] Models.TokenRequest request)
        {
            using (var ctx = new Data.DataContext())
            {
                var user = ctx.XUsers.SingleOrDefault(u => u.Username == request.Username);
                if (user != null)
                {
                    if (request.Signature == user.Password)
                    {
                        var token = ctx.XTokens.SingleOrDefault(t => t.Username == user.Username);

                        if (token != null)
                        {
                            return new Models.TokenResponse()
                            {
                                Token = token.Token
                            };
                        }
                        else
                        {
                            token = new Data.XToken()
                            {
                                Username = user.Username,
                                Token = (Guid.NewGuid().ToString() + Guid.NewGuid().ToString()).Replace("-", "").ToLower(),
                                Created = DateTime.Now
                            };
                            ctx.XTokens.Add(token);

                            await ctx.SaveChangesAsync();

                            return new Models.TokenResponse()
                            {
                                Token = token.Token
                            };
                        }
                    }
                    else
                    {
                        Program.cfgForm.Log(string.Format("Failed authorization attempt on {0}", Request.RequestUri.ToString()));
                        throw new Exception("The authorization failed. Check the signature, it should consist out of the username signed with the pasword.");
                    }
                }
                else
                {
                    Program.cfgForm.Log(string.Format("Failed authorization attempt on {0}", Request.RequestUri.ToString()));
                    throw new Exception("The authorization failed. This user does not exist.");
                }
            }
        }
    }
}
