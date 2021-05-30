using AutoMapper;
using CarStockAPI.DTOs;
using CarStockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Profiles
{
    public class CarsProfile: Profile
    {
        public CarsProfile()
        {
            // Source Type -> Target Type
            CreateMap<Car, CarReadDto>();
            CreateMap<CarAddDto, Car>().BeforeMap((carDto, car) => car.CreatedTime = DateTime.Now);
            CreateMap<CarUpdateDto, Car>();
        }
    }
}
