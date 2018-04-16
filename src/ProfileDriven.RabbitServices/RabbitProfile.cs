using System.Collections.Generic;

namespace ProfileDriven.RabbitServices
{

    public class RabbitProfile
    {
        public class ProfileBasicProperties
        {
            private Dictionary<string, object> _headers;
            public Dictionary<string, object> Headers => _headers ?? (_headers = new Dictionary<string, object>());
            public string AppId { get; set; }
            public string ContentType { get; set; }
            public string ContentEncoding { get; set; }
            public string ClusterId { get; set; }
            public string CorrelationId { get; set; }
            public string Expiration { get; set; }
            public bool Persist { get; set; }
            public bool EnableMessageId { get; set; }
            public bool EnableTimestamp { get; set; }
            public string Type { get; set; }

        }
        private ProfileBasicProperties _basicProperties;
        public ProfileBasicProperties BasicProperties => _basicProperties ?? (_basicProperties = new ProfileBasicProperties());

        public class ProfileQueueConfig
        {
            public string Queue { get; set; }
            public bool Durable { get; set; }
            public bool Exclusive { get; set; }
            public bool AutoDelete { get; set; }
        }
        private ProfileQueueConfig _profileQueueConfig;
        public ProfileQueueConfig ProfileQueue => _profileQueueConfig ?? (_profileQueueConfig = new ProfileQueueConfig());

        public const string PROTOBUF_CONTENT_TYPE = "application/x-protobuf";
 
        public string Name { get; set; }
      
        public string RouteKey { get; set; }
        public string UserAgent { get; set; }
        public string Exchange { get; set; }

        private Dictionary<string, object> _args;
        public Dictionary<string, object> Args => _args ?? (_args = new Dictionary<string, object>());

        public string DeadLetterExchange { get; set; }
        public int MaxQueueLength { get; set; }

    }
}