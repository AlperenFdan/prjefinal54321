using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class Processor
    {
        public int ID { get; set; }
        [Display(Name = "İsim")]
        [StringLength(maximumLength: 150, ErrorMessage = "Bu alan Maksimum 150 Karakterden oluşabilir!")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        public decimal ListPrice { get; set; }
     
        public Nullable<int> BrandID { get; set; }
        [Display(Name = "Marka")]
        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }
       
        public Nullable<int> CategoryID { get; set; }
        [Display(Name = "Kategori")]
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        [Display(Name = "Açıklama")]
        [StringLength(maximumLength: 350, ErrorMessage = "Bu alan Maksimum 350 Karakterden oluşabilir!")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Stok")]
        public int stock { get; set; }
        [Display(Name = "Güvenlik Stoğu")]
        public int SStock { get; set; }
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string imgs { get; set; }
        [Display(Name = "Maksimum Frekans")]
        public double MakximumFrequency { get; set; }
        [Display(Name = "L3 Önbellek")]
        public string L3Cache { get; set; }
        [Display(Name = "L2 Önbellek")]
        public string L2Cahce { get; set; }
        [Display(Name = "Çekirdek sayısı")]
        public string NumberOfCores { get; set; }
        [Display(Name = "Saat frekasnı")]
        public double ClockFrequency { get; set; }
        [Display(Name = "Otobüs Hızı")]
        public string BusSpeed { get; set; }
        [Display(Name = "İşlemci Türü")]
        public string ProcessorType { get; set; }
        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

    }
}