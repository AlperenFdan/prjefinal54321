using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOCOKUGRASTIRIYOR
{
    public class GraphicsCard
    {   public GraphicsCard() {
            IsItReadOnly = false;
            imgs = "Add Image";
            CreationTime = DateTime.Now;
        }

        public int ID { get; set; }

        public string Name { get; set; }
     
        public decimal Price { get; set; }

        public decimal ListPrice { get; set; }
        public Nullable<int> BrandID { get; set; }

        public Brand Brand { get; set; }

        public Nullable<int> CategoryID { get; set; }
        public Category Category { get; set; }


        public string Description { get; set; }
     
        public int stock { get; set; }
   
        public int SStock { get; set; }
   
        public string imgs { get; set; }
        public int VRAM { get; set; }

        public string Series { get; set; }

        public string bitnumber { get; set; }

        public string CompatibleConnect { get; set; }

        public string Connects { get; set; }

        public string StorgeType { get; set; }
        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; }

        public bool IsItReadOnly { get; set; }
    }
}
