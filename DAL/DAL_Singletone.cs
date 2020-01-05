using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public static class DAL_Singletone
    {
        private static Lazy<Dal_imp> _instance = new Lazy<Dal_imp>();

        public static Dal_imp Instance
        {
            get
            {
                return _instance.Value;
            }
        }

    }
}
