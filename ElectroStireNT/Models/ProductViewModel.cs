using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectroStireNT.Models
{
    public class ProductViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Введите название фильма")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание фильма")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Введите категорию фильма")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Введите описание фильма")]
        public decimal Price { get; set; }
    }
}