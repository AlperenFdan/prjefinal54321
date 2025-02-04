using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class Favorites
    {
        public int ID { get; set; }

        public int userID { get; set; }

    
        [ForeignKey("userID")]
        public virtual Users User { get; set; }

        public int ProductID { get; set; }

        public string ProductType { get; set; }
    }
}