using CarStockAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Data
{
    public class InMemoryCarRepo : ICarRepo
    {
        private readonly CarStockContext _context;

        public InMemoryCarRepo(CarStockContext context)
        {
            this._context = context;
        }

        public void AddCar(Car car, string clientId)
        {
            if (car == null) 
            {
                throw new ArgumentNullException(nameof(car));
            }

            // Auto assign dealer's client id when add a new car
            car.DealerClientId = clientId;
            _context.Cars.Add(car);
        }

        public void DeleteCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            _context.Cars.Remove(car);
        }

        public IEnumerable<Car> GetAllCars(string clientId)
        {
            return _context.Cars.Where(c => c.DealerClientId == clientId).ToList();
        }

        public Car GetCarById(Guid id, string clientId)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id && c.DealerClientId == clientId);
        }

        public void UpdateCarStock(Car car, string clientId)
        {
            if (car.DealerClientId != clientId)
            {
                throw new ArgumentNullException(nameof(car));
            }
            _context.Entry(car).State = EntityState.Modified;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
