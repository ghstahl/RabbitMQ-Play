using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;

namespace ProfileDriven.RabbitServices
{
    public static class RabbitServicesExtensions
    {
        public static Dictionary<string, object> MergeHeaders(this RabbitProfile rabbitProfile,
            Dictionary<string, object> overrideHeaders)
        {
            // make a copy of the profile headers
            var merged = rabbitProfile.BasicProperties.Headers.ToDictionary(entry => entry.Key,
                entry => entry.Value);
             
            overrideHeaders = overrideHeaders ?? new Dictionary<string, object>();
            
            overrideHeaders.ToList().ForEach(x => merged[x.Key] = x.Value);

            return merged;
        }
        public static Dictionary<string, object> MergeArgs(this RabbitProfile rabbitProfile,
            Dictionary<string, object> overrideArgs)
        {
            // make a copy of the profile headers
            var merged = rabbitProfile.Args.ToDictionary(entry => entry.Key,
                entry => entry.Value);

            overrideArgs = overrideArgs ?? new Dictionary<string, object>();

            overrideArgs.ToList().ForEach(x => merged[x.Key] = x.Value);

            return merged;
        }

        public static IBasicProperties BuildProperties(this RabbitProfile profileConfig, IModel model, Dictionary<string, object> headers)
        {
            IBasicProperties props = model.CreateBasicProperties();

            props.AppId = profileConfig.BasicProperties.AppId;
            var mergedHeaders = profileConfig.MergeHeaders(headers);
            props.Headers = mergedHeaders;

            if (profileConfig.BasicProperties.EnableMessageId)
            {
                props.MessageId = Guid.NewGuid().ToString();
            }

            if (!string.IsNullOrWhiteSpace(profileConfig.BasicProperties.ContentType))
            {
                props.ContentType = profileConfig.BasicProperties.ContentType;
            }

            if (!string.IsNullOrWhiteSpace(profileConfig.BasicProperties.ContentEncoding))
            {
                props.ContentEncoding = profileConfig.BasicProperties.ContentEncoding;
            }

            if (!string.IsNullOrWhiteSpace(profileConfig.BasicProperties.ClusterId))
            {
                props.ClusterId = profileConfig.BasicProperties.ClusterId;
            }

            if (!string.IsNullOrWhiteSpace(profileConfig.BasicProperties.CorrelationId))
            {
                props.CorrelationId = profileConfig.BasicProperties.CorrelationId;
            }
            if (!string.IsNullOrWhiteSpace(profileConfig.BasicProperties.Expiration))
            {
                props.Expiration = profileConfig.BasicProperties.Expiration;
            }
            if (!string.IsNullOrWhiteSpace(profileConfig.BasicProperties.Type))
            {
                props.Type = profileConfig.BasicProperties.Type;
            }

            if (profileConfig.BasicProperties.EnableTimestamp)
            {
                var timeInMilliSeconds =
                    Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
                props.Timestamp = new AmqpTimestamp(timeInMilliSeconds);
            }

            return props;
        }
    }
}
