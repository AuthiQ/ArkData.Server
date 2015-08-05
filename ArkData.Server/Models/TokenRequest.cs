using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkData.Server.Models
{
    public class TokenRequest
    {
        public string Username { get; set; }
        public string Signature { get; set; }
    }
}
