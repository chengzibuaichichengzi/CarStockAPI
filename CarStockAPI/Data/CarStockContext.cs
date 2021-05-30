using CarStockAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Data
{
    public class CarStockContext: DbContext
    {
        public CarStockContext(DbContextOptions<CarStockContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}
