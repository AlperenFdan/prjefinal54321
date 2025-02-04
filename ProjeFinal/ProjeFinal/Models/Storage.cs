using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjeFinal.Models
{
    public class Storage
    {
        public Storage()
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
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 350, ErrorMessage = "Bu alan Maksimum 350 Karakterden oluşabilir!")]
        public string Description { get; set; }
        [Display(Name = "Stok")]
        public int stock { get; set; }
        [Display(Name = "Güvenlik stoğu")]

        public int SStock { get; set; }
        [Display(Name = "Fotoğraf")]
        [DataType(DataType.ImageUrl)]
        public string imgs { get; set; }
        [Display(Name = "Deoplama Alanaı")]
        public string StorageCapacity { get; set; }
        [Display(Name = "Yazma hızı")]
        public string SequentialWriting { get; set; }
        [Display(Name = "Okuma hızı")]
        public string SequentialReading { get; set; }
        [Display(Name = "Uyumlu bağlantı")]
        public string connections { get; set; }
        [Display(Name = "Yükseklik")]
        public string Height { get; set; }
        [Display(Name = "Genişlik")]
        public string Width { get; set; }
        [Display(Name = "Tür")]
        public string Type { get; set; }

        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; }
    }
}