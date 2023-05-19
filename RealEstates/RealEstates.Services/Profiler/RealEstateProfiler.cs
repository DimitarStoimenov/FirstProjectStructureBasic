using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using RealEstates.Models;

namespace RealEstates.Services.Profiler
{
    public  class RealEstateProfiler : Profile
    {
        public RealEstateProfiler()
        {
            this.CreateMap<RealEstates.Models.Property, PropertyInfoDto>()
                .ForMember(x => x.BuildingType, y => y.MapFrom(s => s.BuildingType.Name))
                .ForMember(x => x.DistinctName, y => y.MapFrom(s => s.District.Name));

            this.CreateMap<District, DistrictInfoDto>()
                .ForMember(x => x.AveragePricePeSquareMeter, s => s.MapFrom(t => t.Properties.Where(x => x.Price.HasValue)
               .Average(x => x.Price / (decimal)x.Size) ?? 0));

            this.CreateMap<RealEstates.Models.Property, GetFullDataDto>()
                .ForMember(x => x.DistinctName, y => y.MapFrom(s => s.District.Name))
                .ForMember(x => x.BuildingType, y => y.MapFrom(s => s.BuildingType.Name))
                .ForMember(x => x.PropertyType, y => y.MapFrom(s => s.Type.Name));

            this.CreateMap<Tag, TagInfoDto>();
               
        }

    }
}
