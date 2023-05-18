using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Property = RealEstates.Models.Property;

namespace RealEstates.Services
{
    public class PropertiesService : IPropertiesService

    {
        private readonly ApplicationDbContext dbContext;

        public PropertiesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Add(int size, int yardSize, byte floor, byte totalFloors, string district, int year, string propertyType, string buildingType, int price)
        {
            var property = new Property
            {
                Size = size,
                YardSize = yardSize <= 0 ? null : yardSize,
                Floor = floor <= 0 || floor > 255 ? null : floor,
                TotalFloors = totalFloors <= 0 || totalFloors > 255 ? null : totalFloors,
                Year = year <= 1800 ? null : year,
                Price = price <= 0 ? null : price,
            };

            var dbDistrict = dbContext.Districts.FirstOrDefault(x => x.Name == district);
            if (dbDistrict == null)
            {
                dbDistrict = new District { Name = district };
            }

            property.District = dbDistrict;

            var dbPropertyType = dbContext.PropertyTypes.FirstOrDefault(x => x.Name == propertyType);

            if (dbPropertyType == null)
            {
                dbPropertyType = new PropertyType { Name = propertyType };
            }

            property.Type = dbPropertyType;

            var dbBuildingType = dbContext.Buildings.FirstOrDefault(x => x.Name == buildingType);

            if (dbBuildingType == null)
            {
                dbBuildingType = new BuildingType { Name = buildingType };
            }

            property.BuildingType = dbBuildingType;

            dbContext.Properties.Add(property);
            dbContext.SaveChanges();
        }

        public decimal AveragePricePerQuadratMeter()
        {
            var properties = dbContext.Properties.Where(x => x.Price.HasValue)
                .Average(x => x.Price / (decimal)x.Size) ?? 0;
                
                

                return properties;

        }

        public IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize)
        {
            var properties = dbContext.Properties.Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Size >= minSize && x.Size <= maxSize)
                
                .Distinct()
                .Select(x => new PropertyInfoDto
                {
                    DistinctName = x.District.Name,
                    Size = x.Size,
                    Price = x.Price ?? 0,
                    PropertyType = x.Type.Name,
                    BuildingType = x.BuildingType.Name,
       
                })
                .Distinct()
                .ToList();
            return properties;
        }
    }
}
