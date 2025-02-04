using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class Emp
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Display(Name = "Soyisim")]
        public string SurnName { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Display(Name = "E posta")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        public bool isBanned { get; set; }

  



    }
}