using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public static class DAL_Singletone
    {
        private static Lazy<IDAL> _instance;

        public static IDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Lazy<IDAL>(() => new DAL_XML_imp());
                }
                return _instance.Value;           
            }
        }

    }
}
