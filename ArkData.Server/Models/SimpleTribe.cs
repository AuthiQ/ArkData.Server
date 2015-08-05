using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkData.Server.Models
{
    public class SimpleTribe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public int Members { get; set; }
    }
}
