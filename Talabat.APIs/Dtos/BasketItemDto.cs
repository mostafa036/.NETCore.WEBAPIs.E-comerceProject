﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
       
      
        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "Quantity must be one at leastet")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Price must be Greater zero")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }

    }
}