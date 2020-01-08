using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class Ibl_imp : IBL
    {
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            if (guestRequest.RegistrationDate >= guestRequest.ReleaseDate)
                throw new Exception("Release date must be greater then registration date");

            DAL_Singletone.Instance.AddGuestRequest(guestRequest);

        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            DAL_Singletone.Instance.AddHostingUnit(hostingUnit);
        }

        public bool AddOrder(Order order)
        {
            var hostingUnit = DAL_Singletone.Instance.GetHostingUnitByKey(order.HostingUnitKey);

            if (hostingUnit == null)
                throw new Exception("host unit does not exist");

            if (hostingUnit.Owner.CollectionClearance == false)
                return false;
           
                var guestRequest = DAL_Singletone.Instance.GetGuestRequestByKey(order.GuestRequestKey);

                if (guestRequest == null)
                    throw new Exception("guest Request does not exist");

                DateTime cureentTate = guestRequest.RegistrationDate;

                while (cureentTate.Date != guestRequest.ReleaseDate.Date.AddDays(1))
                {

                    if (hostingUnit.Diary[cureentTate.Month - 1, cureentTate.Day - 1] == true)

                        return false;
                }

                order.CreateDate = DateTime.Now;

                DAL_Singletone.Instance.AddOrder(order);
            
            return true;

        }

        public bool DeleteHostingUnit(long HostingUnitKey)
        {
            var hostingUnit = DAL_Singletone.Instance.GetHostingUnitByKey(HostingUnitKey);

            if (HostHasOpenOrders(HostingUnitKey))
                return false;

            DAL_Singletone.Instance.DeleteHostingUnit(HostingUnitKey);
            return true;
        }

        public List<BankBranch> GetBankBranchesList()
        {
            return DAL_Singletone.Instance.GetBankBranchesList();
        }
        
        public List<GuestRequest> GetGuestRequestsList()
        {
            return DAL_Singletone.Instance.GetGuestRequestsList();
        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            return DAL_Singletone.Instance.GetHostingUnitsList();
        }

        public List<Order> GetOrderList()
        {
            return DAL_Singletone.Instance.GetOrderList();
        }

        public void UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus)
        {
            DAL_Singletone.Instance.UpdateGuestRequestStatus(GuestRequestKey, requestStatus);
        }

        public bool UpdateHostingUnit(HostingUnit hostingUnit)
        {
            if (hostingUnit.Owner.CollectionClearance == false && HostHasOpenOrders(hostingUnit.HostingUnitKey))
                return false;

            DAL_Singletone.Instance.UpdateHostingUnit(hostingUnit);

            return true;
        }

        public bool UpdateOrder(long orderKey, OrderStatuses orderStatuses)
        {
            Order order = DAL_Singletone.Instance.GetOrderByKey(orderKey);

            if (order.OrderKey == 0 && order.Status == OrderStatuses.Closed_ApprovedByCustomer)
                return false;

            switch (orderStatuses)
                {

                    case OrderStatuses.MailSent:
                        //send mail

                    case OrderStatuses.Closed_ApprovedByCustomer:

                        DAL_Singletone.Instance.UpdateOrder(orderKey, orderStatuses);

                        var guestRequest = DAL_Singletone.Instance.GetGuestRequestByKey(order.GuestRequestKey);

                        DAL_Singletone.Instance.GetGuestRequestsById(guestRequest.Id).ForEach(gr =>
                        {
                            DAL_Singletone.Instance.UpdateGuestRequestStatus(order.GuestRequestKey, RequestStatus.Inactive);

                        });


                        var hostingUnit = DAL_Singletone.Instance.GetHostingUnitByKey(order.HostingUnitKey);

                        DateTime cureentTate = guestRequest.RegistrationDate;

                        while (cureentTate.Date != guestRequest.ReleaseDate.Date.AddDays(1))
                        {
                            hostingUnit.Diary[cureentTate.Month - 1, cureentTate.Day - 1] = true;

                            cureentTate.AddDays(1);
                        }

                        var fee = Config.FEE_RATE * (guestRequest.ReleaseDate - guestRequest.RegistrationDate).TotalDays;

                        DAL_Singletone.Instance.UpdateHostingUnit(hostingUnit);

                        break;

                    default:
                        DAL_Singletone.Instance.UpdateOrder(orderKey, orderStatuses);
                        break;

                       
                }


            return true;

        }
        }

        public List<HostingUnit> GetAllAvailableUnitsForDate(DateTime startDate, int daysNum)
        {
            return (from unit in DAL_Singletone.Instance.GetHostingUnitsList()
                               let available = IsUnitAvailableForDates(unit, startDate, daysNum)
                               where available
                               select unit).ToList();                           

        }

        public int GetDaysBetweenDates(DateTime startDate , [Optional]DateTime? endDate)
        {
            endDate = endDate ?? DateTime.Now;
            return (endDate.Value - startDate).Days;
        }

        public List<Order> GetOrdersForDays(int days)
        {
           return DAL_Singletone.Instance.GetOrderList().TakeWhile(o =>
            (DateTime.Now - o.CreateDate).Days >= days).ToList();
        }

        public int GetOrdersNumForGR(GuestRequest guestRequest)
        {
           return DAL_Singletone.Instance.GetOrderList()
                .Where(o => o.GuestRequestKey == guestRequest.GuestRequestKey).Count();        
        }

        public int GetApprovedOrdersNumForHU(HostingUnit hostingUnit)
        {
            return DAL_Singletone.Instance.GetOrderList()
                  .Where(o => o.HostingUnitKey == hostingUnit.HostingUnitKey
                  && o.Status == OrderStatuses.Closed_ApprovedByCustomer).Count();
        }

        public List<IGrouping<VecationAreas, GuestRequest>> getGRListGroupByArea()
        {
            return DAL_Singletone.Instance.GetGuestRequestsList().GroupBy(gr => gr.Area).ToList();
        }

        public List<IGrouping<int, GuestRequest>> getGRListGroupByVacationers()
        {
            return DAL_Singletone.Instance.GetGuestRequestsList().GroupBy(gr => gr.Adults + gr.Children).ToList();
        }

        public List<dynamic> getHostsByUnitsNum()
        {
            return DAL_Singletone.Instance.GetHostingUnitsList()
                .GroupBy(h => new { Host = h.Owner, id = h.Owner.HostKey })
                .Select(h => new { count = h.Count(), host = h.Key }).ToList<dynamic>();
                
        }

        public List<IGrouping<VecationAreas, HostingUnit>> getHUListGroupByArea()
        {
            return DAL_Singletone.Instance.GetHostingUnitsList().GroupBy(hu => hu.Area).
                ToList();
        }     

        public List<HostingUnit> getAllHostUnitsWithPool() {
           return getAllHostUnitsByPredicate(delegate (HostingUnit hu) { return hu.HasPool; });
        }

        #region Private methods
        private bool HostHasOpenOrders(long hostingUnitKey)
        {
            var ordersNum = DAL_Singletone.Instance.GetOrderList().
             Where(o => o.HostingUnitKey == hostingUnitKey
             && o.Status != OrderStatuses.Closed_ApprovedByCustomer
             && o.Status != OrderStatuses.Closed_NoCustomerResponse).Count();

            if (ordersNum > 0)
                return true;

            return false;
        }

        private bool IsUnitAvailableForDates(HostingUnit hu, DateTime startDate, int daysNum)
        {
            while (daysNum != 0)
            {
                var currDate = startDate.AddDays(daysNum--);
                if (hu.Diary[currDate.Month - 1, currDate.Day - 1] == true)
                    return false;
            }
            return true;
        }

        private delegate bool HostHasPred(HostingUnit hu);

        private List<HostingUnit> getAllHostUnitsByPredicate(HostHasPred hostHasPred)
        {
            return DAL_Singletone.Instance.GetHostingUnitsList().TakeWhile(hu => hostHasPred(hu)).ToList();
        }
        #endregion
    }
}
