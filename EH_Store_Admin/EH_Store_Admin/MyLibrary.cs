using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EH_Store_Admin
{
    public class MyLibrary
    {
        public static bool PropControl(object obj)
        {
            return obj.GetType()
                  .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                  .Where(prop => prop.Name != "Brand" && prop.Name != "Category" && prop.Name != "Product" ) 
                  .All(prop =>
                  {
                      var value = prop.GetValue(obj);
                      if (value is string str)
                      {
                          if (str == "Add Image")
                          {
                              return false;
                          }
                          else
                          {
                              return !string.IsNullOrWhiteSpace(str);
                          }
                      }
                      if (value == null) return false;
                     
                      
                      return true; 
                  });
           
          

        }

      
    }
}
