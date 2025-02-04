using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOCOKUGRASTIRIYOR
{
    public class Motherboard
    {
        public Motherboard()
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

        public string MemoryTechnology { get; set; }

        public string MemorySlot { get; set; }

        public string MemoryClockSpeed { get; set; }

        public string PCIVersion { get; set; }
        public bool MultiGPU { get; set; }

        public string ChipsetManufacturer { get; set; }

        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsItReadOnly { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

