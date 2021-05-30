using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.DTOs
{
    public class CarReadDto
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
