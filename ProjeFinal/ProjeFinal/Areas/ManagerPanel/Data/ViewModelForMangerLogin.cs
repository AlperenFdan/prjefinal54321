using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Areas.ManagerPanel.Data
{
    public class ViewModelForMangerLogin
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email alanı zorunludur!")]
        public string EMail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı zorunludur!")]
        public string Password { get; set; }
    }
}