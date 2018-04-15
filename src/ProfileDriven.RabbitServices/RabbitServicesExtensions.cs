using System.Collections.Generic;
using System.Linq;

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
    }
}
