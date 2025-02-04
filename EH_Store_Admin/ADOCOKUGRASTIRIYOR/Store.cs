using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOCOKUGRASTIRIYOR
{
    public class Store
    {
        public Store()
        {
            IsItReadOnly = false; SignUpTime = DateTime.Now;
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int Tier { get; set; }

        public string Email { get; set; }

        

        public bool IsBanned { get; set; }

        public DateTime SignUpTime { get; set; }

        public bool IsItReadOnly { get; set; }
    }
}
