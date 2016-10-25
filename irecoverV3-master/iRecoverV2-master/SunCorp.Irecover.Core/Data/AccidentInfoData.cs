using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunCorp.IRecover.Data
{
    public class AccidentInfoData
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "accidenttype")]
        public string accidentType { get; set; }

        [JsonProperty(PropertyName = "accidenttime")]
        public string accidentTime { get; set; }

        [JsonProperty(PropertyName = "accidentdetail")]
        public string accidentDetail { get; set; }

        [JsonProperty(PropertyName = "accidentnote")]
        public string accidentNote { get; set; }

    }
}
