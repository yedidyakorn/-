using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using DS;

namespace DAL
{
    public class DAL_XML_imp : IDAL
    {
        private static long serialGuestRequest;
        private static long serialOrder;
        private static long serialHostingUnit;

        public DAL_XML_imp()
        {

            serialOrder = Int32.Parse(DataSourceXml.Orders.Element("lastSerial").Value) ;
            serialGuestRequest = Int32.Parse(DataSourceXml.GuestRequests.Element("lastSerial").Value);
            serialHostingUnit = Int32.Parse(DataSourceXml.HostingUnits.Element("lastSerial").Value);

            serialOrder = serialOrder == 0 ? Config.ORDER_COUNTER : serialOrder;
            serialGuestRequest = serialGuestRequest == 0 ? Config.GUEST_REQUEST_COUNTER : serialGuestRequest;
            serialHostingUnit = serialHostingUnit == 0 ? Config.HOSTING_UNIT_COUNTER : serialHostingUnit;

        }

        public bool AddGuestRequest(GuestRequest guestRequest)
        {
            guestRequest.GuestRequestKey = ++serialGuestRequest;
            XElement guestRequestElement = XElement.Parse(guestRequest.ToXMLstring());
            DataSourceXml.GuestRequests.Element("lastSerial").Value = guestRequestElement.Element("GuestRequestKey").Value;
            DataSourceXml.SaveGuestRequests();
            DataSourceXml.GuestRequests.Add(guestRequestElement);
            DataSourceXml.SaveGuestRequests();
            return true;
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            hostingUnit.HostingUnitKey = ++serialHostingUnit;
            XElement hostingUnitElement = XElement.Parse(hostingUnit.ToXMLstring());
            DataSourceXml.HostingUnits.Element("lastSerial").Value = hostingUnitElement.Element("HostingUnitKey").Value;
            DataSourceXml.SaveHostingUnits();
            DataSourceXml.HostingUnits.Add(hostingUnitElement);
            DataSourceXml.SaveHostingUnits();
          
        }

        public void AddOrder(Order order)
        {
            order.OrderKey = ++serialOrder;
            XElement orderElement = XElement.Parse(order.ToXMLstring());
            DataSourceXml.Orders.Element("lastSerial").Value = orderElement.Element("OrderKey").Value;
            DataSourceXml.SaveOrders();
            DataSourceXml.Orders.Add(orderElement);
            DataSourceXml.SaveOrders();
        }

        public bool DeleteGuestRequestByKey(long guestRequestKey)
        {

            XElement findGuestRequest = (from o in DataSourceXml.GuestRequests.Elements("GuestRequest")
                                         where Int32.Parse(o.Element("GuestRequestKey").Value) == guestRequestKey
                                         select o).FirstOrDefault();

            if (findGuestRequest == null)
            {
                return false;
            }

            findGuestRequest.Remove();

            DataSourceXml.SaveGuestRequests();
            return true;
        }

        public void DeleteHostingUnit(long hostingUnitKey)
        {

            XElement findHostUnit = (from hu in DataSourceXml.HostingUnits.Elements("HostingUnit")
                                     where Int32.Parse(hu.Element("HostingUnitKey").Value) == hostingUnitKey
                                     select hu).FirstOrDefault();
            if (findHostUnit == null)
            {
                return;
            }

            findHostUnit.Remove();
            DataSourceXml.SaveHostingUnits();

        }

        public List<BankBranch> GetBankBranchesList()
        {
            throw new NotImplementedException();
        }//todo

        public GuestRequest GetGuestRequestByKey(long guestRequestKey)
        {
            GuestRequest result = null;
            XElement findGuestRequest = (from o in DataSourceXml.GuestRequests.Elements("GuestRequest")
                                  where Int32.Parse(o.Element("GuestRequestKey").Value) == guestRequestKey
                                  select o).FirstOrDefault();
            if (findGuestRequest != null)
            {
                result = findGuestRequest.ToString().ToObject<GuestRequest>();
            }

            return result;
        }

        public List<GuestRequest> GetGuestRequestsById(long id)
        {

            return (from gr in DataSourceXml.GuestRequests.Elements("GuestRequest")
                    where Int32.Parse(gr.Element("Id").Value) == id
                    select gr.ToString().ToObject<GuestRequest>()).ToList();

        }

        public List<GuestRequest> GetGuestRequestsList()
        {
            return (from gr in DataSourceXml.GuestRequests.Elements("GuestRequest")
                    select gr.ToString().ToObject<GuestRequest>()).ToList();
        }

        public HostingUnit GetHostingUnitByKey(long hostingUnitKey)
        {

            HostingUnit result = null;
            XElement findHostUnit = (from hu in DataSourceXml.HostingUnits.Elements("HostingUnit")
                                  where Int32.Parse(hu.Element("HostingUnitKey").Value) == hostingUnitKey
                                  select hu).FirstOrDefault();
            if (findHostUnit != null)
            {
                result = findHostUnit.ToString().ToObject<HostingUnit>();
            }

            return result;
        }

        public List<HostingUnit> GetHostingUnitsByOwnerId(long id)
        {
            return (from hu in DataSourceXml.HostingUnits.Elements("HostingUnit")
                    where Int32.Parse(hu.Element("Owner").Element("ID").Value) == id
                    select hu.ToString().ToObject<HostingUnit>()).ToList();

        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            return (from hu in DataSourceXml.HostingUnits.Elements("HostingUnit")
                    select hu.ToString().ToObject<HostingUnit>()).ToList();
        }

        public Order GetOrderByKey(long orderKey)
        {

            Order result = null;
            XElement findOrder = (from o in DataSourceXml.Orders.Elements("Order")
                                  where Int32.Parse(o.Element("OrderKey").Value) == orderKey
                                  select o).FirstOrDefault();
            if (findOrder != null)
            {
                result = findOrder.ToString().ToObject<Order>();
            }

            return result;
        }

        public List<Order> GetOrderList()
        {
            return (from o in DataSourceXml.Orders.Elements("Order")
                    select o.ToString().ToObject<Order>()).ToList();
        }

        public bool UpdateGuestRequest(GuestRequest guestRequest)
        {
            XElement findGuestRequest = (from gr in DataSourceXml.GuestRequests.Elements("GuestRequest")
                                         where Int32.Parse(gr.Element("GuestRequestKey").Value) == guestRequest.GuestRequestKey
                                         select gr).FirstOrDefault();

            if (findGuestRequest == null)
            {
                return false;
            }

            findGuestRequest.ReplaceWith(XElement.Parse(guestRequest.ToXMLstring())); 
            DataSourceXml.SaveGuestRequests();

            return true;
        }

        public void UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus)
        {
            XElement findGuestRequest  = (from gr in DataSourceXml.GuestRequests.Elements("GuestRequest")
                                  where Int32.Parse(gr.Element("GuestRequestKey").Value) == GuestRequestKey
                                          select gr).FirstOrDefault();

            if (findGuestRequest == null)
            {
                return;
            }

            findGuestRequest.Element("Status").Value = requestStatus.ToString();
            DataSourceXml.SaveGuestRequests();
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            XElement findHostingUnit = (from hu in DataSourceXml.HostingUnits.Elements("HostingUnit")
                                  where Int32.Parse(hu.Element("HostingUnitKey").Value) == hostingUnit.HostingUnitKey
                                        select hu).FirstOrDefault();

            if (findHostingUnit == null)
            {
                return;
            }

            findHostingUnit.ReplaceWith(XElement.Parse(hostingUnit.ToXMLstring()));
            DataSourceXml.SaveHostingUnits();
   
        }

        public void UpdateOrder(long orderKey, OrderStatuses orderStatuses)
        {
            XElement findOrder = (from o in DataSourceXml.Orders.Elements("Order")
                                  where Int32.Parse(o.Element("OrderKey").Value) == orderKey
                                  select o).FirstOrDefault();

            if (findOrder == null)
            {
                return;
            }

            findOrder.Element("Status").Value = orderStatuses.ToString();
            DataSourceXml.SaveOrders();

        }
    }
}
