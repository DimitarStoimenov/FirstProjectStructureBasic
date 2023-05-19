using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstates.Services.Models;

namespace RealEstates.Services
{
    public interface IPropertiesService
    {
        void Add( 
        int size,
        int yardSize,
        byte floor,
        byte totalFloors,
        string district,
        int year,
        string type,
        string buildingType,
        int price
        );

        double AverageSize(int districtId);

        decimal AveragePricePerQuadratMeter();

        decimal AveragePricePerQuadratMeter(int districtId);

        IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize);

        IEnumerable<GetFullDataDto> GetFullData(int count);


    }
}
