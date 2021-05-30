using CarStockAPI.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Car_ValidYear]
        public int Year { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        public int StockQuantity { get; set; }
        [Required]
        public string DealerClientId { get; set; }
    }
}
