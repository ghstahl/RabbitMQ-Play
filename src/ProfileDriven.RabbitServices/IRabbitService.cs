using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileDriven.RabbitServices
{
    public interface IRabbitService
    {
        Task SendAsync(string profile, byte[] message, 
            Dictionary<string, object> args,
            Dictionary<string, object> headers);
    }
}

