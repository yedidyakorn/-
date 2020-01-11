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
            var target = Activator.CreateInstance(t.GetType());

            FieldInfo[] fis = t.GetType().GetFields();
            foreach (FieldInfo fi in fis)
            {
                var fieldValue = fi.GetValue(t);
                if (fi.FieldType.Namespace != t.GetType().Namespace)
                    fi.SetValue(t, fieldValue);
                else
                    fi.SetValue(t, cloneT(t));
            }

            return t;
        }
       
    }
}
