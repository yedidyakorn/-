using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;

namespace DAL
{
    public static class Cloning 
    {

        public static IClonable clone(this IClonable original) {
            
            return original != null ? cloneT(original) : null;

        }

        public static T cloneT<T>(this T t)
        {
            var target = (T)Activator.CreateInstance(t.GetType());

            PropertyInfo[] props = t.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var fieldValue = prop.GetValue(t);
                    if (fieldValue.GetType().Namespace.Contains("System") 
                    || fieldValue.GetType().GetProperties().Count() == 0)
                        prop.SetValue(target, fieldValue);
                else
                    prop.SetValue(target, cloneT(fieldValue));
            }

            return target;
        }
       
    }
}
