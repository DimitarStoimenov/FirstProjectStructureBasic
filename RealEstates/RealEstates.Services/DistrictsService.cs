﻿using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{
    public class DistrictsService : BaseServices, IDistrictsService
    {
        private readonly ApplicationDbContext db;

        public DistrictsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DistrictInfoDto> GetMostExpensiveDistricts(int count)
        {
            var districts = db.Districts
                .ProjectTo<DistrictInfoDto>(this.Mapper.ConfigurationProvider)
                
                
            //    Select(x => new DistrictInfoDto
            //{
            //    Name = x.Name,
            //    PropertiesCount = x.Properties.Count(),
            //    AveragePricePeSquareMeter = db.Properties.Where(x => x.Price.HasValue)
            //    .Average(x => x.Price / (decimal)x.Size) ?? 0
            //}) 
                .OrderByDescending( x => x.AveragePricePeSquareMeter)
                .Take(count)
                .ToList();

            return districts;
        }

        public IEnumerable<DistrictInfoDto> MostExpenciveDistrict(int count)
        {
            return GetMostExpensiveDistricts(1);
        }
    }
}
