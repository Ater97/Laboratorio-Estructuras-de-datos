using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laboratory_2.Models
{
    public class BillsModel
    {
        [Required]
        public char Serie { get; set; }

        [Required]
        [Display(Name = "Correlativo")]
        public int Correlative { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        public string NIT { get; set; }

        [Required]
        [Display(Name = "Fecha de venta")]
        public string SaleDate { get; set; }

        [Required]
        [Display(Name = "Descripción de la compra")]
        public string BillDescription { get; set; }

        [Required]
        public double Total { get; set; }
    }
}