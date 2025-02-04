using ProjeFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProjeFinal.MyHelpers
{
    public class Mylib
    {
        EH_Store db = new EH_Store();
        public static bool PropControl(object obj)
        {
            return obj.GetType()
                  .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                  .Where(prop => prop.Name != "Brand" && prop.Name != "Category" && prop.Name != "imgs")
                  .All(prop =>
                  {
                      var value = prop.GetValue(obj);
                      if (value is string str)
                      {
                          return !string.IsNullOrWhiteSpace(str);
                      }
                      if (value == null) return false;


                      return true;
                  });



        }
        public object GetArticle(string productType, int productId)
        {
            Type type = Type.GetType("ProjeFinal.Models." + productType);
            if (type == null) return null;

            var dbSet = db.Set(type);
            var product = dbSet.Find(productId);

            return product;
        }
    
    }
}