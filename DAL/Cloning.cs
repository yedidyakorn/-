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


            IClonable target = (IClonable)Activator.CreateInstance(original.GetType());

            FieldInfo[] fis = original.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo fi in fis)
            {
                IClonable fieldValue = (IClonable)fi.GetValue(original);
                if (fi.FieldType.Namespace != original.GetType().Namespace)
                    fi.SetValue(target, fieldValue);
                else
                    fi.SetValue(target, clone(fieldValue));
            }

            return target;
        }

       
    }
}
