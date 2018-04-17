using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProfileDriven.RabbitServices
{

    [JsonObject(MemberSerialization.OptIn)]
    public class RabbitServiceConfig
    {
        [JsonProperty("rabbitServerConfig")]
        public RabbitServerConfig RabbitServerConfig { get; set; }
        private Dictionary<string, RabbitProfile> _rabbitProfiles;


        [JsonProperty("profiles")]
        public Dictionary<string, RabbitProfile> RabbitProfiles
        {
            get { return _rabbitProfiles ?? (_rabbitProfiles = new Dictionary<string, RabbitProfile>()); }
            set { _rabbitProfiles = value; }
        }

        //=> _rabbitProfiles ?? (_rabbitProfiles = new Dictionary<string, RabbitProfile>());
    }
}