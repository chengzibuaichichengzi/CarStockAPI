using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.DTOs
{
    public class CarUpdateDto
    {
        [Required]
        public int StockQuantity { get; set; }
    }
}
