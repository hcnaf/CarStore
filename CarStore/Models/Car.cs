using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Required(ErrorMessage = "Please, enter car name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter car description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please, enter car price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive.")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please, enter car category.")]
        public string Category { get; set; }

        public string ImageURL { get; set; }
    }
}
