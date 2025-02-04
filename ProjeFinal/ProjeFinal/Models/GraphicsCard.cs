using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeFinal.Models
{
    public class GraphicsCard
    {
        public GraphicsCard()
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
        [Display(Name = "Güvenlik Stoğu")]
        public int SStock { get; set; }
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string imgs { get; set; }
        [Display(Name = "VRAM")]
        public int VRAM { get; set; }
        [Display(Name = "Seri")]
        public string Series { get; set; }
        [Display(Name = "Bit Sayısı")]
        public string bitnumber { get; set; }
        [Display(Name = "Uyumlu Bağlantı")]
        public string CompatibleConnect { get; set; }
        [Display(Name = "Açıklama")]
        public string Connects { get; set; }
        [Display(Name = "Bağlantılar")]
        public string StorgeType { get; set; }

        
        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }


        public DateTime CreationTime { get; set; }
    }
}