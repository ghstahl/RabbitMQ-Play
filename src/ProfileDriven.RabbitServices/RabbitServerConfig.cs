using Newtonsoft.Json;

namespace ProfileDriven.RabbitServices
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RabbitServerConfig
    {
        [JsonProperty("nameSpace")]
        public string NameSpace { get; set; }
        [JsonProperty("host")]
        public string Host { get; set; }
        [JsonProperty("altHost")]
        public string AltHost { get; set; }
        [JsonProperty("virtualHost")]
        public string VirtualHost { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("port")]
        public int? Port { get; set; }
    }
}