using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunCorp.IRecover.Data
{
    public class ProgramInfoData
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "programcontact")]
        public string programContact { get; set; }

        [JsonProperty(PropertyName = "additionalinfo")]
        public string additionalInfo { get; set; }
    }
}
