using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfileDriven.RabbitServices
{

    [JsonObject(MemberSerialization.OptIn)]
    public class RabbitProfile
    {
        public class ProfileBasicProperties
        {
            private Dictionary<string, object> _headers;
            [JsonProperty("headers")]
            public Dictionary<string, object> Headers
            {
                get { return _headers ?? (_headers = new Dictionary<string, object>()); }
                set { _headers = value; }
            }

     
            [JsonProperty("appId")]
            public string AppId { get; set; }

            [JsonProperty("contentType")]
            public string ContentType { get; set; }

            [JsonProperty("contentEncoding")]
            public string ContentEncoding { get; set; }

            [JsonProperty("clusterId")]
            public string ClusterId { get; set; }

            [JsonProperty("correlationId")]
            public string CorrelationId { get; set; }

            [JsonProperty("expiration")]
            public string Expiration { get; set; }

            [JsonProperty("persist")]
            public bool Persist { get; set; }

            [JsonProperty("enableMessageId")]
            public bool EnableMessageId { get; set; }

            [JsonProperty("enableTimestamp")]
            public bool EnableTimestamp { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

        }
        private ProfileBasicProperties _basicProperties;
        
        //=> _basicProperties ?? (_basicProperties = new ProfileBasicProperties());

        public class ProfileQueueConfig
        {
            public string Queue { get; set; }
            public bool Durable { get; set; }
            public bool Exclusive { get; set; }
            public bool AutoDelete { get; set; }
        }
        private ProfileQueueConfig _profileQueueConfig;
        

        public const string PROTOBUF_CONTENT_TYPE = "application/x-protobuf";
 
 
        private Dictionary<string, object> _args;

        [JsonProperty("args")]
        public Dictionary<string, object> Args
        {
            get { return _args ?? (_args = new Dictionary<string, object>()); }
            set { _args = value; }
        }
        //=> _args ?? (_args = new Dictionary<string, object>());

 

        [JsonProperty("basicProperties")]
        public ProfileBasicProperties BasicProperties
        {
            get { return _basicProperties ?? (_basicProperties = new ProfileBasicProperties()); }
            set { _basicProperties = value; }
        }

        [JsonProperty("profileQueue")]
        public ProfileQueueConfig ProfileQueue
        {
            get { return _profileQueueConfig ?? (_profileQueueConfig = new ProfileQueueConfig()); }
            set { _profileQueueConfig = value; }
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("routeKey")]
        public string RouteKey { get; set; }

        [JsonProperty("userAgent")]
        public string UserAgent { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("deadLetterExchange")]
        public string DeadLetterExchange { get; set; }

        [JsonProperty("maxQueueLength")]
        public long MaxQueueLength { get; set; }
    }
}