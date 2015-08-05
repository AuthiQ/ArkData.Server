using System;
using System.Collections.Generic;

namespace ArkData.Server.Models
{
    public class Tribe : SimpleTribe
    {
        public new SimplePlayer Owner { get; set; }
        public new List<SimplePlayer> Members { get; set; }
        public DateTime Created { get; set; }
    }
}
