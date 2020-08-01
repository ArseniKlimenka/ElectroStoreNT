using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectroStireNT.Models
{
    public class CreateProductViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название фильма")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите описание фильма")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Введите цену фильма")]
        [Range(0.00, 1000.00, ErrorMessage = "Введено некорректное значение цены")]
        public decimal Price { get; set; }       

        [Required(ErrorMessage = "Выберите жанр фильма")]
        public int CategoryId { get; set; }
      
        public SelectList Categories { get; set; }
    }
}