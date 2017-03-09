using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laboratory_2.Models
{
    public class ProductModel
    {
        [Key]
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string ProductDescription { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public double ProductPrize { get; set; }

        [Required]
        [Display(Name = "Cantidad en inventario")]
        public long ProductCount { get; set; }


    }
}