using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class Category
    {
        public Category() { 
            
            IsActive = true;

            CreationTime = DateTime.Now;
        }

        public int ID { get; set; }
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

  

        public bool IsActive { get; set; }

    
        
        public DateTime CreationTime { get; set; }
    }
}