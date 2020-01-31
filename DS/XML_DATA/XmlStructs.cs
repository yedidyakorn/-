using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.XML_DATA
{
    public class OrdersXml
    {
        public long lastSerial { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }

    public class GuestRequestsXml
    {
        public long lastSerial { get; set; }

        public List<GuestRequest> GuestRequests { get; set; } = new List<GuestRequest>();
    }

    public class HostingUnitsXml
    {
        public long lastSerial { get; set; }

        public List<HostingUnit> HostingUnits { get; set; } = new List<HostingUnit>();
    }
}
