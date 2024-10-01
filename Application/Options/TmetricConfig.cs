using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Options
{
    public class TmetricConfig
    {
        public required string BaseUrl { get; set; }
        public required string Token { get; set; }
    }
}
