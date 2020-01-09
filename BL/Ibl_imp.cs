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
        public bool AddGuestRequest(GuestRequest guestRequest)
        {
            try
            {
                if (guestRequest.RegistrationDate >= guestRequest.ReleaseDate)
                    throw new LogicException("Release date must be greater then registration date", "guestRequest.RegistrationDate");

                DAL_Singletone.Instance.AddGuestRequest(guestRequest);
            }
            catch(Exception ex)
            {
                throw new LogicException(ex);
            }

            return true;
        }

        public bool AddHostingUnit(HostingUnit hostingUnit)
        {
            try
            {
                DAL_Singletone.Instance.AddHostingUnit(hostingUnit);
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }

            return true;
        }

        public bool AddOrder(Order order)
        {
            try
            {
                var hostingUnit = DAL_Singletone.Instance.GetHostingUnitByKey(order.HostingUnitKey);

                if (hostingUnit == null)
                    throw new LogicException($"host unit {order.HostingUnitKey} does not exist");

                if (hostingUnit.Owner.CollectionClearance == false)
                    return false;

                var guestRequest = DAL_Singletone.Instance.GetGuestRequestByKey(order.GuestRequestKey);

                if (guestRequest == null)
                    throw new LogicException($"guest Request {order.GuestRequestKey} does not exist");

                DateTime cureentDate = guestRequest.RegistrationDate;

                while (cureentDate.Date != guestRequest.ReleaseDate.Date.AddDays(1))
                {

                    if (hostingUnit.Diary[cureentDate.Month - 1, cureentDate.Day - 1] == true)

                        return false;

                    cureentDate = cureentDate.AddDays(1);
                }

                order.CreateDate = DateTime.Now;

                DAL_Singletone.Instance.AddOrder(order);
            }
            catch(LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }




            return true;

        }

        public bool DeleteHostingUnit(long HostingUnitKey)
        {
            try
            {
                var hostingUnit = DAL_Singletone.Instance.GetHostingUnitByKey(HostingUnitKey);

                if (HostHasOpenOrders(HostingUnitKey))
                    return false;

                DAL_Singletone.Instance.DeleteHostingUnit(HostingUnitKey);
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }

            return true;
        }

        public List<BankBranch> GetBankBranchesList()
        {
            try
            {
                return DAL_Singletone.Instance.GetBankBranchesList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }
        
        public List<GuestRequest> GetGuestRequestsList()
        {
            try
            {
                return DAL_Singletone.Instance.GetGuestRequestsList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            try
            {
                return DAL_Singletone.Instance.GetHostingUnitsList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        public List<Order> GetOrderList()
        {
            try
            {
                return DAL_Singletone.Instance.GetOrderList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        public bool UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus)
        {
            try
            {
                DAL_Singletone.Instance.UpdateGuestRequestStatus(GuestRequestKey, requestStatus);

            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }

            return true;
        }

        public bool UpdateHostingUnit(HostingUnit hostingUnit)
        {
            try
            {
                if (hostingUnit.Owner.CollectionClearance == false && HostHasOpenOrders(hostingUnit.HostingUnitKey))
                    return false;

                DAL_Singletone.Instance.UpdateHostingUnit(hostingUnit);
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
            return true;
        }

        public bool UpdateOrder(long orderKey, OrderStatuses orderStatuses)
        {
            try
            {
                Order order = DAL_Singletone.Instance.GetOrderByKey(orderKey);

                if (order.OrderKey == 0 && order.Status == OrderStatuses.Closed_ApprovedByCustomer)
                    return false;

                switch (orderStatuses)
                {

                    case OrderStatuses.MailSent:
                       

                    case OrderStatuses.Closed_ApprovedByCustomer:

                        DAL_Singletone.Instance.UpdateOrder(orderKey, orderStatuses);

                        var guestRequest = DAL_Singletone.Instance.GetGuestRequestByKey(order.GuestRequestKey);

                        if(guestRequest == null)
                        {
                            throw new LogicException($"Guest Request {order.GuestRequestKey} does not exist"); 
                        }

                        DAL_Singletone.Instance.GetGuestRequestsById(guestRequest.Id).ForEach(gr =>
                        {
                            DAL_Singletone.Instance.UpdateGuestRequestStatus(order.GuestRequestKey, RequestStatus.Inactive);

                        });


                        var hostingUnit = DAL_Singletone.Instance.GetHostingUnitByKey(order.HostingUnitKey);

                        DateTime cureentDate = guestRequest.RegistrationDate;

                        while (cureentDate.Date != guestRequest.ReleaseDate.Date.AddDays(1))
                        {
                            hostingUnit.Diary[cureentDate.Month - 1, cureentDate.Day - 1] = true;

                            cureentDate = cureentDate.AddDays(1);
                        }

                        var fee = Config.FEE_RATE * (guestRequest.ReleaseDate - guestRequest.RegistrationDate).TotalDays;

                        DAL_Singletone.Instance.UpdateHostingUnit(hostingUnit);

                        break;

                    default:
                        DAL_Singletone.Instance.UpdateOrder(orderKey, orderStatuses);
                        break;


                }

            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }

            return true;

        }
       
        public List<HostingUnit> GetAllAvailableUnitsForDate(DateTime startDate, int daysNum)
        {
            try
            {
                return (from unit in DAL_Singletone.Instance.GetHostingUnitsList()
                        let available = IsUnitAvailableForDates(unit, startDate, daysNum)
                        where available
                        select unit).ToList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }

        }

        public int GetDaysBetweenDates(DateTime startDate , [Optional]DateTime? endDate)
        {
            try
            {
                endDate = endDate ?? DateTime.Now;
                return (endDate.Value - startDate).Days;
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        #region order manager

        public List<Order> GetOrdersForDays(int days)
        {
            try
            {
                return DAL_Singletone.Instance.GetOrderList().TakeWhile(o =>
                 (DateTime.Now - o.CreateDate).Days >= days).ToList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        public int GetOrdersNumForGR(GuestRequest guestRequest)
        {
            try
            {
                return DAL_Singletone.Instance.GetOrderList()
                     .Where(o => o.GuestRequestKey == guestRequest.GuestRequestKey).Count();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        public int GetApprovedOrdersNumForHU(HostingUnit hostingUnit)
        {
            try
            {
                return DAL_Singletone.Instance.GetOrderList()
                      .Where(o => o.HostingUnitKey == hostingUnit.HostingUnitKey
                      && o.Status == OrderStatuses.Closed_ApprovedByCustomer).Count();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        #endregion

        public List<IGrouping<VecationAreas, GuestRequest>> GetGRListGroupByArea()
        {
            try
            {
                return DAL_Singletone.Instance.GetGuestRequestsList().GroupBy(gr => gr.Area).ToList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        public List<IGrouping<int, GuestRequest>> GetGRListGroupByVacationers()
        {
            try
            {
                return DAL_Singletone.Instance.GetGuestRequestsList().GroupBy(gr => gr.Adults + gr.Children).ToList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }

        public List<dynamic> GetHostsByUnitsNum()
        {
            try
            {
                return DAL_Singletone.Instance.GetHostingUnitsList()
                .GroupBy(h => new { Host = h.Owner, id = h.Owner.HostKey })
                .Select(h => new { count = h.Count(), host = h.Key }).ToList<dynamic>();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }

        }

        public List<IGrouping<VecationAreas, HostingUnit>> GetHUListGroupByArea()
        {
            try
            {
                return DAL_Singletone.Instance.GetHostingUnitsList().GroupBy(hu => hu.Area).
                    ToList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }     

        public List<HostingUnit> GetAllHostUnitsWithPool() {

            try
            {
                return GetAllHostUnitsByPredicate(delegate (HostingUnit hu) { return hu.HasPool; });
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }

        }

        #region Private methods

        private bool HostHasOpenOrders(long hostingUnitKey)
        {
            try
            {
                var ordersNum = DAL_Singletone.Instance.GetOrderList().
                 Where(o => o.HostingUnitKey == hostingUnitKey
                 && o.Status != OrderStatuses.Closed_ApprovedByCustomer
                 && o.Status != OrderStatuses.Closed_NoCustomerResponse).Count();

                if (ordersNum > 0)
                    return true;
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
    
            return false;
        }

        private bool IsUnitAvailableForDates(HostingUnit hu, DateTime startDate, int daysNum)
        {
            try
            {
                while (daysNum != 0)
                {
                    var currDate = startDate.AddDays(daysNum--);
                    if (hu.Diary[currDate.Month - 1, currDate.Day - 1] == true)
                        return false;
                }
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
            return true;
        }

        private delegate bool HostHasPred(HostingUnit hu);

        private List<HostingUnit> GetAllHostUnitsByPredicate(HostHasPred hostHasPred)
        {
            try
            {
                return DAL_Singletone.Instance.GetHostingUnitsList().TakeWhile(hu => hostHasPred(hu)).ToList();
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new LogicException(ex);
            }
        }
       
        #endregion
    }
}
