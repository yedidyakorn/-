using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        public long HostingUnitKey { get; set; }

        public long GuestRequestKey { get; set; }

        public long OrderKey { get; set; }

        public  OrderStatuses Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime OrderDate { get; set; } //Email Delivery Date to Customer

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
