using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOCOKUGRASTIRIYOR
{
    public class Processor
    {
        public Processor() {
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
        public double MakximumFrequency { get; set; }

        public string L3Cache { get; set; }

        public string L2Cahce { get; set; }

        public string NumberOfCores { get; set; }

        public double ClockFrequency { get; set; }

        public string BusSpeed { get; set; }

        public string ProcessorType { get; set; }

        public bool IsItReadOnly { get; set; }
        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
