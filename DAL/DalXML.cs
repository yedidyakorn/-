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
    class DalXML : IDAL
    {
        public bool AddGuestRequest(GuestRequest guestRequest)
        {
            XElement guestRequestElement = XElement.Parse(guestRequest.ToXMLstring());
            DataSourceXml.GuestRequests.Element("lastSerial").Value = guestRequestElement.Element("GuestRequestKey").Value;
            DataSourceXml.SaveGuestRequests();
            DataSourceXml.GuestRequests.Add(guestRequestElement);
            DataSourceXml.SaveGuestRequests();
            return true;
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGuestRequestByKey(long guestRequestKey)
        {
            throw new NotImplementedException();
        }

        public void DeleteHostingUnit(long HostingUnitKey)
        {
            throw new NotImplementedException();
        }

        public List<BankBranch> GetBankBranchesList()
        {
            throw new NotImplementedException();
        }

        public GuestRequest GetGuestRequestByKey(long guestRequestKey)
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> GetGuestRequestsById(long id)
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> GetGuestRequestsList()
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnitByKey(long hostingUnitKey)
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> GetHostingUnitsByOwnerId(long id)
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByKey(long orderKey)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrderList()
        {
            throw new NotImplementedException();
        }

        public bool UpdateGuestRequest(GuestRequest guestRequest)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(long orderKey, OrderStatuses orderStatuses)
        {
            throw new NotImplementedException();
        }
    }
}
