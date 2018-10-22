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

        /// <summary>
        /// Reads JSON file and converts it
        /// </summary>
        static void ConvertJSON()
        {
            string path = "../../../data.json";
            string data = "";

            using (StreamReader sr = File.OpenText(path))
            {
                data = sr.ReadToEnd();
            }

            RootObject rootObj = JsonConvert.DeserializeObject<RootObject>(data);

            // Neighborhood query with lambda
            Console.WriteLine("Neighborhood query with Lambda");
            var lambdaNeigh = rootObj.features.Where(x => x.properties.neighborhood != "");

            foreach (var item in lambdaNeigh)
            {
                Console.WriteLine(item.properties.neighborhood);
            }

            Console.WriteLine("\n");
            Console.WriteLine("Non-unique neighborhoods");
            //Non unique neighborhoods
            var neighborhoods = (from addr in rootObj.features
                                      where addr.properties.neighborhood != ""
                                      select addr.properties.neighborhood);

            //Prints all neighborhoods
            foreach (var item in neighborhoods)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n");
            Console.WriteLine("Unique neighborhoods");
            var uniqueNeighborhoods = (from uniq in neighborhoods group uniq by uniq);

            foreach (var item in uniqueNeighborhoods)
            {
                Console.WriteLine(item.Key);
            }

            Console.WriteLine("\n");
            Console.WriteLine("Query chain for unique neighborhoods");

            var oneLineUniqueNeighborhoods = (from uniq in rootObj.features
                                              where uniq.properties.neighborhood != ""
                                              select uniq.properties.neighborhood).Distinct();

            foreach (var item in oneLineUniqueNeighborhoods)
            {
                Console.WriteLine(item);
            }
        }
    }
}
