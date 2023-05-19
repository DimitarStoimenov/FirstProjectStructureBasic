using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services;
using System.Diagnostics;
using System.Drawing;
using System.Text.Json;

namespace RealEstates.Importer
{
     class Program
    {
        static void Main(string[] args)
        {
            
            ImportJsonFile("imot.bg-houses-Sofia-raw-data-2021-03-18.json");
            Console.WriteLine();
            ImportJsonFile("imot.bg-raw-data-2021-03-18.json");
        }

        public static void ImportJsonFile(string importFile)
        {
            var dbContext = new ApplicationDbContext();
            IPropertiesService propertiesService = new PropertiesService(dbContext);

            var properties = JsonSerializer.Deserialize<IEnumerable<PropertyAsJson>>(File.ReadAllText(importFile));

            foreach (var propJson in properties)
            {
                propertiesService.Add(propJson.Size, propJson.YardSize, propJson.Floor, propJson.TotalFloors, propJson.District, propJson.Year, propJson.Type, propJson.BuildingType, propJson.Price);

                Console.Write(".");
            }
        }
    }
}