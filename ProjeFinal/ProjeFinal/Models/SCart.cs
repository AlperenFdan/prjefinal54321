using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class SCart
    {
        public SCart() {
        AddedDate = DateTime.Now;
        }
        public int ID { get; set; }

      
        [ForeignKey("UserID")]
        public virtual Users User { get; set; }
        public int UserID { get; set; } 

        public int ProductID { get; set; } 

        public string ProductType { get; set; } 

        public int Quantity { get; set; } 

        public decimal price { get; set; }

        public DateTime AddedDate { get; set; } 
    }
}