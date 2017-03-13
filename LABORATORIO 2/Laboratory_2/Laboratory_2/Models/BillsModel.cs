﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laboratory_2.Models
{
    public class BillsModel: IComparable<BillsModel>
    {
        
        public char Serie { get; set; }

       
        [Display(Name = "Correlativo")]
        public int Correlative { get; set; }

        
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        
        public string NIT { get; set; }

        
        [Display(Name = "Fecha de venta")]
        public string SaleDate { get; set; }

       
        [Display(Name = "Descripción de la compra")]
        public string BillDescription { get; set; }

        public double Total { get; set; }

        public int CompareTo(BillsModel other)
        {
            if (Serie >= other.Serie)
            {
                return 1;
            }
            return 0;
        }
    }
}