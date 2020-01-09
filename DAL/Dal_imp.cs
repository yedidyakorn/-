﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class Dal_imp : IDAL
    {

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            hostingUnit.HostingUnitKey = Config.HOSTING_UNIT_COUNTER;
            
            if (DataSource.hostingUnits.Any(hu => hu.HostingUnitKey == hostingUnit.HostingUnitKey))
            {
                throw new LogicException("HostingUnit ID already exists");
            }

            DataSource.hostingUnits.Add(hostingUnit);

        }

        public void AddOrder(Order order)
        {
            order.OrderKey = Config.ORDER_COUNTER;

            if (DataSource.orders.Any(o => o.OrderKey == order.OrderKey))
            {
                throw new LogicException("Order ID already exists");
            }

            DataSource.orders.Add(order);

        }

        public void DeleteHostingUnit(long HostingUnitKey)
        {
            if (!DataSource.hostingUnits.Any(o => o.HostingUnitKey == HostingUnitKey))
            {
                throw new LogicException($"hosting {HostingUnitKey} does not exist");
            }

            DataSource.hostingUnits.Remove(DataSource.hostingUnits.Where(hu => hu.HostingUnitKey == HostingUnitKey).FirstOrDefault());
        }

        public List<BankBranch> GetBankBranchesList()
        {
            return new List<BankBranch>()
            {
               new BankBranch()
               {
                   BankName = "לאומי",
                   BankNumber = 10,
                   BranchAddress = "השלושה 3",
                   BranchCity = "בני ברק",
                   BranchNumber = 856,
               },
               new BankBranch()
               {
                   BankName = "דיסקונט",
                   BankNumber = 11,
                   BranchAddress = "טבריה 21",
                   BranchCity = "בני ברק",
                   BranchNumber = 789,
               },
               new BankBranch()
               {
                   BankName = "מזרחי",
                   BankNumber = 09,
                   BranchAddress = "אבני נזר 18",
                   BranchCity = "בני ברק",
                   BranchNumber = 847,
               },
               new BankBranch()
               {
                   BankName = "הפועלים",
                   BankNumber = 12,
                   BranchAddress = "רבי עקיבא 30",
                   BranchCity = "בני ברק",
                   BranchNumber = 231,
               },
               new BankBranch()
               {
                   BankName = "יהב",
                   BankNumber = 15,
                   BranchAddress = "צפת 50",
                   BranchCity = "בני ברק",
                   BranchNumber = 593,
               },
            }.Select(b => (BankBranch)b.clone()).ToList();
        }

        public List<GuestRequest> GetGuestRequestsList()
        {
            return DataSource.guestRequests.Select(gr => (GuestRequest)Cloning.clone(gr)).ToList();
        }

        public List<GuestRequest> GetGuestRequestsById(long id)
        {
            return DataSource.guestRequests.Where(gr=>gr.Id == id)
                .Select(gr => (GuestRequest)gr.clone()).ToList();
         }

        public GuestRequest GetGuestRequestByKey(long guestRequestKey)
        {
            return (GuestRequest)DataSource.guestRequests.Where(gr => gr.GuestRequestKey == guestRequestKey)
                .FirstOrDefault().clone();
        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            return DataSource.hostingUnits.Select(hu => (HostingUnit)Cloning.clone(hu)).ToList();
        }

        public HostingUnit GetHostingUnitByKey(long hostingUnitKey)
        {
            return (HostingUnit)DataSource.hostingUnits.Where(hu => hu.HostingUnitKey == hostingUnitKey)
                .FirstOrDefault().clone();
        }

        public List<Order> GetOrderList()
        {
            return DataSource.orders.Select(o => (Order)Cloning.clone(o)).ToList();
        }

        public Order GetOrderByKey(long orderKey)
        {
          return (Order)DataSource.orders
          .Where(o => o.OrderKey == orderKey).FirstOrDefault().clone();

        }

        public void UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus)
        {
            if (!DataSource.guestRequests.Any(gr => gr.GuestRequestKey == GuestRequestKey))
            {
                throw new LogicException($"Guest Requests {GuestRequestKey} does not exist");
            }

            DataSource.guestRequests
               .Where(gr => gr.GuestRequestKey == GuestRequestKey).ToList()
               .ForEach(gr => gr.Status = requestStatus);
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {

            if (!DataSource.hostingUnits.Any(hu => hu.HostingUnitKey == hostingUnit.HostingUnitKey))
            {
                throw new LogicException($"Hosting Unit {hostingUnit.HostingUnitKey} does not exist");
            }

            DataSource.hostingUnits
               .Where(hu => hu.HostingUnitKey == hostingUnit.HostingUnitKey).ToList()
               .ForEach(hu => hu = hostingUnit);
        }

        public void UpdateOrder(long orderKey, OrderStatuses orderStatuses)
        {
        
            if (!DataSource.orders.Any(o => o.OrderKey == orderKey))
            {
                throw new LogicException($"Order {orderKey} does not exist");
            }

            DataSource.orders
             .Where(o => o.OrderKey == orderKey).ToList()
             .ForEach(o => o.Status = orderStatuses);
        }

        public bool AddGuestRequest(GuestRequest guestRequest)
        {
            guestRequest.GuestRequestKey = Config.GUEST_REQUEST_COUNTER;

            try
            {
                int exist = DataSource.guestRequests.Where(gr => gr.GuestRequestKey == guestRequest.GuestRequestKey).Count();

                if (exist > 0)

                    throw new LogicException("ID already exists");

                DataSource.guestRequests.Add(guestRequest);

            }
            catch {
                return false;
            }

            return true;
            
        }

    }
}
