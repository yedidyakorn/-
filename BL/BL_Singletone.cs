using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
     public class BL_Singletone
    {
        private static Lazy<IBL> _instance;

        public static IBL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Lazy<IBL>(() => new Ibl_imp());
                }
                return _instance.Value;
            }
        }
    }
}
