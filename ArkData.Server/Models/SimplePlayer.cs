using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkData.Server.Models
{
    public class SimplePlayer
    {
        public long Id { get; set; }
        public string CharacterName { get; set; }
        public string SteamName { get; set; }
        public string AvatarUrl { get; set; }
        public short Level { get; set; }
        public bool Online { get; set; }
    }
}
