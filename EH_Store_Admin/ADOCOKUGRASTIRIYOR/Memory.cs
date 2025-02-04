using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOCOKUGRASTIRIYOR
{
    public class Memory
    {
        public Memory()
        {
            IsItReadOnly = false;
            imgs = "Add Image";
            CreationTime = DateTime.Now;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal ListPrice { get; set; }
        public int BrandID { get; set; }

        public int CategoryID { get; set; }

        public string Description { get; set; }
        public int stock { get; set; }
        public int SStock { get; set; }
        public string imgs { get; set; }
        public string MemoryType { get; set; }

        public string CapacityPerModule { get; set; }

        public string MemoryFrequency { get; set; }

        public string TotalMemoryCapacity { get; set; }
        public bool IsItReadOnly { get; set; }
        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
