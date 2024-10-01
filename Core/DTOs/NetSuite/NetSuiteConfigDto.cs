using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.NetSuite
{
    public class NetSuiteConfigDto
    {
        public string BaseUrl { get; set; } = "https://6397908-sb2.restlets.api.netsuite.com/app/site/hosting/restlet.nl";
        public string ConsumerKey { get; set; } = "b1e9612819c3465bccc732de437c7ee73af582a4fe4ff8e1f44417f4ca823963";
        public string ConsumerSecret { get; set; } = "b8a825a6d23d3d6ecce439517bb7f974f29461cb6647c59dea8aeac65e9920ce";
        public string TokenKey { get; set; } = "75976c054ca1d29cf74beba201e231446f25d0fe88163421ad6fb02578e616b4";
        public string TokenSecret { get; set; } = "b95ac53ad8fa189b39d2a4c4ec0e1c403beba3a267e1a21ab496f41bec8a9486";
        public string Realm { get; set; } = "6397908_SB2";
        public string ScriptId { get; set; } = "1768";
        public string DeployId { get; set; } = "1";
    }
}
