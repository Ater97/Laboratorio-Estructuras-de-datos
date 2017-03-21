using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Laboratory_2.UtilitiesClass;

namespace Laboratory_2.Models
{
    public class ProductModel : IComparable<ProductModel> 
    {
        [Display(Name = "Codigo de producto")]
        public string ProductID { get; set; }
        
        [Display(Name = "Descripcion")]
        public string ProductDescription { get; set; }

       
        [Display(Name = "Precio")]
        public double ProductPrize { get; set; }

        
        [Display(Name = "Cantidad en inventario")]
        public double ProductCount { get; set; }

        public int CompareTo(ProductModel other)
        {
            return ProductID.CompareTo(other.ProductID);
        }

        public string MultiInformation
        {
            get
            {
                string res = ProductDescription + "  Q" + ProductPrize.ToString();
                return res;
            }
        }
    }
}