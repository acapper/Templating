using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            DasSoapApi das = new DasSoapApi();
            Type thisType = das.GetType();

            using (StreamReader r = new StreamReader("api.json"))
            {
                string jsonText = r.ReadToEnd();
                JObject json = JObject.Parse(jsonText);
                foreach (var i in json.SelectToken("apiEndPoints"))
                {
                    MethodInfo theMethod = thisType.GetMethod(i.SelectToken("name").ToString());
                    var res = theMethod.Invoke(das, new object[] { "Message" });

                    Console.WriteLine(i.SelectToken("name").ToString());
                    Console.WriteLine(res);
                    Console.WriteLine();
                }
            }
        }
    }
}
