using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Proto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProfileDriven.RabbitServices;

namespace ProfileDriven.Tests
{
 
    [TestClass]
    public class TestJsonSerialization
    {

        [TestMethod]
        [DeploymentItem("source")]
        public async Task TestJsonWithDictionary()
        {
             string json = @"{
                'basicProperties': {
                    'appId': 'a_string',
                    'contentType': 'a_string',
                    'contentEncoding': 'a_string',
                    'clusterId': 'a_string',
                    'correlationId': 'a_string',
                    'expiration': 'a_string',
                    'persist': true,
                    'enableMessageId': true,
                    'enableTimestamp': true,
                    'type': 'a_string',
                    'headers': {
                        'x-test': 'x-test-value'
                    }
                },
                'profileQueue': {
                    'queue': 'a_string',
                    'durable': true,
                    'exclusive': true,
                    'autoDelete': true
                },
                'name': 'a_string',
                'routeKey': 'a_string',
                'userAgent': 'a_string',
                'exchange': 'a_string',
                'deadLetterExchange': 'a_string',
                'maxQueueLength': 123,
                'args': {
                    'x-arg': 'x-arg-value'
                }

            }";

            RabbitProfile obj = 
                JsonConvert.DeserializeObject<RabbitProfile>(json);
        }
    }
}
