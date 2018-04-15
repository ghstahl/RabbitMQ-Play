using System.Collections.Generic;

namespace ProfileDriven.RabbitServices
{
    public class RabbitServiceConfig
    {
        public RabbitServerConfig RabbitServerConfig { get; set; }
        private Dictionary<string, RabbitProfile> _rabbitProfiles;

        public Dictionary<string, RabbitProfile> RabbitProfiles => _rabbitProfiles ?? (_rabbitProfiles = new Dictionary<string, RabbitProfile>());
    }
}