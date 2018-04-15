using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using benchmarks.proto2;
using ProtoBuf;

namespace Benchmark.Proto
{
    public class TestUtilities
    {
        static void AutoFillObject(object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propertyValue = null;
                if (typeof(string).IsAssignableFrom(property.PropertyType))
                {
                    // Found a property that is an string
                    propertyValue = $"test-{property.Name}";
                }

                if (typeof(long[]).IsAssignableFrom(property.PropertyType))
                {
                    // Found a property that is an string
                    var val = new long[] { 1, 2, 3, 4 };

                    propertyValue = val;
                }
                if (typeof(float).IsAssignableFrom(property.PropertyType))
                {
                    propertyValue = (float)5.678;
                }



                if (typeof(List<string>).IsAssignableFrom(property.PropertyType))
                {
                    List<string> propValue = (List<string>)property.GetValue(obj, null);
                    propValue.Add("well");
                    propValue.Add("hello");
                    propValue.Add("there");

                }
                if (typeof(byte[]).IsAssignableFrom(property.PropertyType))
                {
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        Serializer.Serialize(ms2, "Hello");
                        propertyValue = ms2.ToArray();
                    }
                }
                if (typeof(long).IsAssignableFrom(property.PropertyType))
                {
                    propertyValue = 2324;
                }
                if (typeof(int).IsAssignableFrom(property.PropertyType))
                {
                    propertyValue = 1010;
                }
                if (typeof(bool).IsAssignableFrom(property.PropertyType))
                {
                    propertyValue = true;
                }

                if (propertyValue != null)
                {
                    property.SetValue(obj, propertyValue, null);
                }
            }
        }
        public static byte[] BuildMessage()
        {
            GoogleMessage2 gm2 = new GoogleMessage2()
            {
                Group1s = { }
            };
            var group1 = new GoogleMessage2.Group1();
            AutoFillObject(group1);
            gm2.Group1s.Add(group1);
            AutoFillObject(gm2);


            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize(ms, gm2);
                ms.Position = 0;
                GoogleMessage2 clone = Serializer.Deserialize<GoogleMessage2>(ms);
            }

            byte[] bs2;
            using (MemoryStream ms2 = new MemoryStream())
            {
                Serializer.Serialize(ms2, gm2);

                bs2 = ms2.ToArray();
            }

            using (MemoryStream ms3 = new MemoryStream())
            {
                ms3.Write(bs2, 0, bs2.Length);
                ms3.Position = 0;
                GoogleMessage2 clone = Serializer.Deserialize<GoogleMessage2>(ms3);
            }


            return bs2;
        }
    }
}
