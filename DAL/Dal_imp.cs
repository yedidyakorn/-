using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    class Dal_imp : IDal
        {
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            Func <HostingUnit,bool> func = myfunc;
            static bool myfunc(HostingUnit a)
            {
                if (a.HostingUnitKey == hostingUnit.HostingUnitKey)
                    return true;
            }
            foreach (var item in DataSource.hostingUnits)
            {
                if (myfunc(item))
                {
                    Console.WriteLine("ERROR - alrady exsit");
                    return;
                }
            }


            HostingUnit[] temp = new HostingUnit[1] { hostingUnit };
            DataSource.hostingUnits.CopyTo(temp);
        }

        public void AddOrder(Order order)
        {
            Order[] temp = new Order[1] { order };
            DataSource.orders.CopyTo(temp);
        }

        public void DeleteHostingUnit(long HostingUnitKey)
        {
            throw new NotImplementedException();
        }

        public List<BankBranch> GetBankBranchesList()
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> GetGuestRequestsList()
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrderList()
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequestStatus(RequestStatus requestStatus)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(OrderStatuses orderStatuses)
        {
            throw new NotImplementedException();
        }

        void AddGuestRequest(GuestRequest guestRequest)
        {
            GuestRequest[] temp = new GuestRequest[1] { guestRequest };            
            DataSource.guestRequests.CopyTo(temp);
        }

        void IDal.AddGuestRequest(GuestRequest guestRequest)
        {
            throw new NotImplementedException();
        }
    }
}
