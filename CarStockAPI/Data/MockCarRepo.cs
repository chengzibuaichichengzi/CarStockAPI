using CarStockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Data
{
    public class MockCarRepo : ICarRepo
    {
        public void AddCar(Car car, string clientId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(Car car)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetAllCars(string clientId)
        {
            var cars = new List<Car> 
            {
                new Car { Id = new Guid("81188fb9-678e-4f9a-9c09-7ee3784545ac"), Make = "Audi", Model = "A4", Year = 2014},
                new Car { Id = new Guid("422e5db1-e7d7-4555-8293-04d0bf2f667c"), Make = "BMW", Model = "5 Series", Year = 2016},
                new Car { Id = new Guid("7026201f-e66a-4704-87b7-720c41bc76fd"), Make = "Audi", Model = "A5", Year = 2015}
            };

            return cars;
        }

        public Car GetCarById(Guid id, string clientId)
        {
            return new Car { Id = new Guid("81188fb9-678e-4f9a-9c09-7ee3784545ac"), Make = "Audi", Model = "A4", Year = 2014 };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCarStock(Car car, string clientId)
        {
            throw new NotImplementedException();
        }
    }
}
