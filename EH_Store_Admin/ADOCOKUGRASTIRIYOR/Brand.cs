using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOCOKUGRASTIRIYOR
{
    public class Brand
    {
        public Brand()
        {
            IsItReadOnly = false; CreationTime = DateTime.Now;
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

        public bool IsItReadOnly { get; set; }
    }
}
