using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class Memory
    {
        public Memory()
        {
            IsActived = true;
            IsDeleted = false;
            CreationTime = DateTime.Now;
        }
        public int ID { get; set; }
        [Display(Name = "İsim")]
        [StringLength(maximumLength: 150, ErrorMessage = "Bu alan Maksimum 150 Karakterden oluşabilir!")]
        public string Name { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "List fiyatı")]
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
        [Display(Name = "Güvenlik stoğu")]
        public int SStock { get; set; }
       
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string imgs { get; set; }
        [Display(Name = "Bellek türü")]
        public string MemoryType { get; set; }
        [Display(Name = "Adet başına kapaste")]
        public string CapacityPerModule { get; set; }
        [Display(Name = "Bellek Frekansı")]
        public string MemoryFrequency { get; set; }
        [Display(Name = "Toplam Bellek kapastesi")]
        public string TotalMemoryCapacity { get; set; }
        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}