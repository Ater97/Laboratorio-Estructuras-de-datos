using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laboratory_2.Models
{
    public class ProductModel : IComparable<ProductModel> 
    {
        [Display(Name = "Codigo de producto")]
        public int ProductID { get; set; }
        
        [Display(Name = "Descripcion")]
        public string ProductDescription { get; set; }

       
        [Display(Name = "Precio")]
        public double ProductPrize { get; set; }

        
        [Display(Name = "Cantidad en inventario")]
        public long ProductCount { get; set; }

        public int CompareTo(ProductModel other)
        {
            if(ProductID >= other.ProductID)
            {
                return 1;
            }
            return 0;
        }
    }
}