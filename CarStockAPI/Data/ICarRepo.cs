using CarStockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Data
{
    public interface ICarRepo
    {
        bool SaveChanges();

        IEnumerable<Car> GetAllCars(string clientId);
        Car GetCarById(Guid id, string clientId);
        void AddCar(Car car, string clientId);
        void UpdateCarStock(Car car, string clientId);
        void DeleteCar(Car car);
    }
}
