using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class Motherboard
    {
        public Motherboard()
        {
            IsActived = true;
            IsDeleted = false;
            CreationTime = DateTime.Now;
        }
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
        [Display(Name = "Güvenlik stoğu")]
        public int SStock { get; set; }
        [Display(Name = "Fotoğraf")]
        [DataType(DataType.ImageUrl)]
        public string imgs { get; set; }
        [Display(Name = "Bellek teknolijisi")]
        public string MemoryTechnology { get; set; }
        [Display(Name = "Bellek slot sayısı")]
        public string MemorySlot { get; set; }
        [Display(Name = "Bellek hız Uyumu")]
        public string MemoryClockSpeed { get; set; }
        [Display(Name = "PCI Verisonu")]
        public string PCIVersion { get; set; }
        [Display(Name = "1den fazla GPU?")]
        public bool MultiGPU { get; set; }
        [Display(Name = "İşlemci uyumu")]
        public string ChipsetManufacturer { get; set; }

        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; }
    }
}