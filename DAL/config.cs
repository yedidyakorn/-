using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public static class Config
    {

        public static int GUEST_REQUEST_COUNTER { get; set; } = 20000000;

        public static int ORDER_COUNTER { get; set; } = 20000000;

        public static int HOSTING_UNIT_COUNTER { get; set; } = 20000000;

        public static int FEE_RATE = 10;

    }

}
