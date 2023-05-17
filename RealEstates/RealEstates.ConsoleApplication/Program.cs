using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services;
using RealEstates.Services.Models;
using System.Text;

namespace RealEstates.ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var db = new ApplicationDbContext();

            db.Database.Migrate();

           
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option");
                Console.WriteLine("1. Property Search");
                Console.WriteLine("2. Most expensive districts");
                Console.WriteLine("0. EXIT");

                bool parsed = int.TryParse(Console.ReadLine(), out int option);
                if (parsed && option == 0)
                {
                    break;
                }
                if (parsed && (option >= 1 && option <= 2))
                {
                    switch (option)
                    {
                        case 1:
                            PropertySearch(db);
                            break;
                            case 2:
                            GetMostExpensiveProperty(db);
                            break;

                        default:
                            break;
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void GetMostExpensiveProperty(ApplicationDbContext db)
        {
            Console.Write("Districts count");
            int count = int.Parse(Console.ReadLine());
            IDistrictsService districtsService = new DistrictsService(db);
            var districts = districtsService.GetMostExpensiveDistricts(count);
            foreach (var district in districts)
            {
                Console.WriteLine($"{district.Name} => {district.PropertiesCount} => {district.AveragePricePeSquareMeter}€/m²");
            }
        }

        private static void PropertySearch(ApplicationDbContext db)
        {
            Console.Write("MinPrice:");
            int minPrice = int.Parse(Console.ReadLine());

            Console.Write("MaxPrice:");
            int maxPrice = int.Parse(Console.ReadLine());

            Console.Write("MinSize:");
            int minSize = int.Parse(Console.ReadLine());

            Console.Write("MaxSize:");
            int maxSize = int.Parse(Console.ReadLine());

            IPropertiesService service = new PropertiesService(db);

            IEnumerable<PropertyInfoDto> properties = service.Search(minPrice, maxPrice, minSize, maxSize);

            foreach (PropertyInfoDto property in properties)
            {
                Console.WriteLine($"{property.DistinctName};{property.BuildingType}; {property.PropertyType} => {property.Price}€ => {property.Size}m²");
            }
        }
    }
}