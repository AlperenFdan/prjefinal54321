using ADOCOKUGRASTIRIYOR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EH_Store_Admin
{

    public class ViewModelcik
    {
        DataRegion dr = new DataRegion();
        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Tier> Tiers { get; set; }
        public ViewModelcik()
        {
            Brands = dr.ListBrands();
            Categories = dr.ListCategory();
            Tiers = dr.ListTier();
        }
    }


}
