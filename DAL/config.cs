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
        private static int guest_request_counter = 10000000;

        private static int order_counter = 10000000;

        private static int hosting_unit_counter = 10000002;


        public static int GUEST_REQUEST_COUNTER { get => ++guest_request_counter; }

        public static int ORDER_COUNTER { get => ++order_counter; }

        public static int HOSTING_UNIT_COUNTER { get => ++hosting_unit_counter; }

        public static int FEE_RATE = 10;

    }

}
