using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Benchmark.Proto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            RabbitProfile profile = new RabbitProfile()
            {
                Name = "proto",
                RouteKey = "proto",
                UserAgent = "proto-aggregator"
            };
            profile.ProfileQueue.Queue = "proto";
            profile.ProfileQueue.Durable = true;
            profile.ProfileQueue.Exclusive = false;
            profile.ProfileQueue.AutoDelete = false;

            profile.BasicProperties.EnableMessageId = true;
            profile.BasicProperties.EnableTimestamp = true;
            profile.BasicProperties.AppId = "proto";
            profile.BasicProperties.Headers.Add("x-test", "test");
            profile.BasicProperties.ContentType = RabbitProfile.PROTOBUF_CONTENT_TYPE;

            //   profile.Args.Add("x-dead-letter-exchange", "x-dead-letter-exchange-value");
            //   profile.Args.Add("x-max-length-bytes", 1000000);



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
