using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.NetSuite
{
    public class NetSuiteRequestBodyDto
    {
        public string action { get; set; }
        public string recordType { get; set; }
        public int id { get; set; }
    }
}
