using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class Users
    {
        public int ID { get; set; }
        [Display(Name = "Kullanıcı adı")]
        public string Name { get; set; }
        [Display(Name = "İsim")]
        public string SurnName { get; set; }
        [Display(Name = "Soy isim")]
        public string UserName { get; set; }
        [Display(Name = "Mail adresi")]
        public string Email { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        
        public DateTime SignUpTime { get; set; }
    }
}