using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using LINQ.Classes;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConvertJSON();
        }

        static void ConvertJSON()
        {
            string path = "../../../data.json";
            string data = "";

            using (StreamReader sr = File.OpenText(path))
            {
                data = sr.ReadToEnd();
            }

            RootObject rootObj = JsonConvert.DeserializeObject<RootObject>(data);

            //foreach (var item in rootObj.features)
            //{
            //    Console.WriteLine(item.properties.zip);
            //    Console.WriteLine(item.properties.state);
            //    Console.WriteLine(item.properties.city);
            //    Console.WriteLine(item.properties.borough);
            //    Console.WriteLine(item.properties.county);
            //    Console.WriteLine(item.properties.address);
            //    Console.WriteLine(item.properties.neighborhood);
            //}

            //var neighborhoods = rootObj.features.Where(x => x.properties.neighborhood != "");

            //foreach (var item in neighborhoods)
            //{
            //    Console.WriteLine(item.properties.neighborhood);
            //}

            var uniqueNeighborhoods = (from addr in rootObj.features
                                      where addr.properties.neighborhood != ""
                                      select addr.properties.neighborhood).Distinct();

            foreach (var item in uniqueNeighborhoods)
            {
                Console.WriteLine(item);
            }
        }
    }
}
