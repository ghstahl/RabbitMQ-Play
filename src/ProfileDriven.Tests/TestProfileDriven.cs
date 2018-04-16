using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Benchmark.Proto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ProfileDriven.RabbitServices;
using RabbitMQ.Client;

namespace ProfileDriven.Tests
{
    [TestClass]
    public class TestProfileDriven
    {

        private IContainer _autofacContainer;
     
        private static ConnectionFactory _connectionFactory;
        private RabbitService _rabbitService;


        [AssemblyInitialize]
        public static void InitializeTestServer(TestContext testContext)
        {
        }

        [TestCleanup]
        public void TestClean()
        {
            _rabbitService = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            string json = @"{
                'basicProperties': {
                    'appId': 'protoAppId',
                    'contentType': 'application/x-protobuf',
                    'contentEncoding': null,
                    'clusterId': null,
                    'correlationId': null,
                    'expiration': null,
                    'persist': true,
                    'enableMessageId': true,
                    'enableTimestamp': true,
                    'type': null,
                    'headers': {
                        'x-test': 'x-test-value'
                    }
                },
                'profileQueue': {
                    'queue': 'proto',
                    'durable': true,
                    'exclusive': false,
                    'autoDelete': false
                },
                'name': 'proto',
                'routeKey': 'proto',
                'userAgent': 'proto-aggregator-useragent',
                'exchange': null,
                'deadLetterExchange': null,
                'maxQueueLength': 1000000,
                'args': {
                    'x-arg': 'x-arg-value'
                }

            }";

            /*
             *
             _factory = new ConnectionFactory
            {
                UserName = userName,
                Password = password,
                HostName = hostName,
                VirtualHost = virtualhost,
                Port = port
            };
             */

            RabbitProfile profile =
                JsonConvert.DeserializeObject<RabbitProfile>(json);

            _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
 

            var rabbitServiceConfig = new RabbitServiceConfig()
            {
                RabbitServerConfig = new RabbitServerConfig()
                {
                    Host = "localhost"
                }
            };

            rabbitServiceConfig.RabbitProfiles.Add(profile.Name, profile);
            _rabbitService = new RabbitService(rabbitServiceConfig);
        }

        [TestMethod]
        public async Task TestSendProtoWithNullOverridesAsync()
        {
            var message = TestUtilities.BuildMessage();
            await _rabbitService.SendAsync("proto", message, null, null);
        }
        [TestMethod]
        public async Task TestSendProtoAsync()
        {
            var message = TestUtilities.BuildMessage();
            var headers = new Dictionary<string, object>
            {
                {"x-custom-header", "x-custom-header-value"},
              };
            var args = new Dictionary<string, object>
            {
                {"blah-arg", "blah-arg-value"}
            };

            await _rabbitService.SendAsync("proto", message, args, headers);
        }
    }
}
