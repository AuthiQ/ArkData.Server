using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkData.Server.Models
{
    public class Player : SimplePlayer
    {
        public string SteamId { get; set; }
        public DateTime Created { get; set; }
        public SimpleTribe Tribe { get; set; }
        public string ProfileUrl { get; set; }
        public bool CommunityBanned { get; set; }
        public bool VACBanned { get; set; }
        public int NumberOfVACBans { get; set; }
        public int DaysSinceLastBan { get; set; }
        public int NumberOfGameBans { get; set; }
    }
}
