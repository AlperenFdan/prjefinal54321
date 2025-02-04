using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOCOKUGRASTIRIYOR
{
    public class Storage
    {
        public Storage()
        {
            IsItReadOnly = false;
            imgs = "Add Image";
            CreationTime = DateTime.Now;
        }


        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal ListPrice { get; set; }
        public Nullable<int> BrandID { get; set; }
    
        public Nullable<int> CategoryID { get; set; }

        public string Description { get; set; }

        public int stock { get; set; }

        public int SStock { get; set; }

        public string imgs { get; set; }

        public string StorageCapacity { get; set; }

        public string SequentialWriting { get; set; }

        public string SequentialReading { get; set; }

        public string connections { get; set; }

        public string Height { get; set; }

        public string Width { get; set; }

        public string Type { get; set; }

        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsItReadOnly { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
