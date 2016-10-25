

using Newtonsoft.Json;

namespace SunCorp.IRecover.Data
{
    public class AccountInfoData
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "profilename")]
        public string profileName { get; set; }

        [JsonProperty(PropertyName = "carregistration")]
        public string carRegistration { get; set; }

        [JsonProperty(PropertyName = "contactinfo")]
        public string contactInfo { get; set; }

        [JsonProperty(PropertyName = "age")]
        public string age { get; set; }

        [JsonProperty(PropertyName = "sex")]
        public string sex { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string address { get; set; }

        [JsonProperty(PropertyName = "policynumber")]
        public string policyNumber { get; set; }

    }

    
}
