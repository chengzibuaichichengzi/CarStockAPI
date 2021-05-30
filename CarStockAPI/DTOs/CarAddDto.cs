using CarStockAPI.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.DTOs
{
    public class CarAddDto
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Car_ValidYear]
        public int Year { get; set; }
    }
}
