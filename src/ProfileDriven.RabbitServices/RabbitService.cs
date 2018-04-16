using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace ProfileDriven.RabbitServices
{
    public class RabbitService : IRabbitService
    {
        private RabbitServiceConfig _configuration;
        private IConnectionFactory _connectionFactory;
        public RabbitService(
            RabbitServiceConfig configuration)
        {
            _configuration = configuration;
            if (_configuration?.RabbitProfiles == null || _configuration.RabbitProfiles.Count == 0)
            {
                throw new ArgumentNullException("configuration", "either null or empty configuration");
            }

            var connectionFactory = new ConnectionFactory()
            {
                HostName = configuration.RabbitServerConfig.Host 
                 
            };
            if (configuration.RabbitServerConfig.Port != null)
            {
                connectionFactory.Port = (int) configuration.RabbitServerConfig.Port;
            }

            if (!string.IsNullOrWhiteSpace(configuration.RabbitServerConfig.Username))
            {
                connectionFactory.UserName = configuration.RabbitServerConfig.Username;
            }

            if (!string.IsNullOrWhiteSpace(configuration.RabbitServerConfig.Password))
            {
                connectionFactory.Password = configuration.RabbitServerConfig.Password;
            }

            if (!string.IsNullOrWhiteSpace(configuration.RabbitServerConfig.Password))
            {
                connectionFactory.Password = configuration.RabbitServerConfig.Password;
            }
            if (!string.IsNullOrWhiteSpace(configuration.RabbitServerConfig.VirtualHost))
            {
                connectionFactory.VirtualHost = configuration.RabbitServerConfig.VirtualHost;
            }
            _connectionFactory = connectionFactory;
 
          
        }

        public async Task SendAsync(string profile, byte[] message,
            Dictionary<string,object> args,
            Dictionary<string, object> headers)
        {
            if(string.IsNullOrWhiteSpace(profile))
                throw new ArgumentNullException("profile");
            if (!_configuration.RabbitProfiles.ContainsKey(profile))
            {
                throw new KeyNotFoundException($"{profile} not found");
            }

            var profileConfig = _configuration.RabbitProfiles[profile];
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    var mergedArgs = profileConfig.MergeArgs(args);
                    model.QueueDeclare(
                        queue: profileConfig.ProfileQueue.Queue
                        , durable: profileConfig.ProfileQueue.Durable
                        , exclusive: profileConfig.ProfileQueue.Exclusive
                        , autoDelete: profileConfig.ProfileQueue.AutoDelete
                        , arguments: mergedArgs.Count>0?mergedArgs:null);



                    IBasicProperties props = profileConfig.BuildProperties(model, headers);

                    model.BasicPublish(
                        exchange: profileConfig.Exchange??"", 
                        routingKey: profileConfig.RouteKey,
                        basicProperties: props, 
                        body: message);
                }
            }
        }
    }
}