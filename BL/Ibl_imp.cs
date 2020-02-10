using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using Util;

namespace BL
{
    internal class Ibl_imp : IBL
    {
        internal Ibl_imp()
        {
            Jobs jobs = new Jobs(UpdateOrdersStatus);
        }

        #region guest request manager
        
        public bool AddGuestRequest(GuestRequest guestRequest)
        {
            try
            {
                guestRequest.RegistrationDate = DateTime.Now;

                if (guestRequest.EntryDate.Date < DateTime.Now.Date)
                    throw new LogicException("guestRequest.EntryDate", "Date not valid");

                if (guestRequest.EntryDate.Date >= guestRequest.ReleaseDate.Date)
                    throw new LogicException("guestRequest.EntryDate", "Release date must be greater then Entry Date date");

                DAL_Singletone.Instance.AddGuestRequest(guestRequest);
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

        public bool UpdateGuestRequest(GuestRequest guestRequest)
        {
            try
            {
                DAL_Singletone.Instance.UpdateGuestRequest(guestRequest);           
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

        public List<GuestRequest> GetGuestRequestsById(long id) {
            try
            {
                return DAL_Singletone.Instance.GetGuestRequestsById(id);
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

        public GuestRequest GetGuestRequestsByKey(long key)
        {
            try
            {
                return DAL_Singletone.Instance.GetGuestRequestByKey(key);
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

        public bool DeleteGuestRequestsByKey(long key)
        {
            try
            {
                return DAL_Singletone.Instance.DeleteGuestRequestByKey(key);
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

        #endregion
      
        #region order manager

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

                if(hasOpenApprovedOrder(guestRequest.GuestRequestKey,hostingUnit.HostingUnitKey))
                    throw new LogicException($"order has been created.");

                DateTime cureentDate = guestRequest.RegistrationDate;

                while (cureentDate.Date != guestRequest.ReleaseDate.Date.AddDays(1))
                {

                    if (hostingUnit.Diary[cureentDate.Month - 1, cureentDate.Day - 1] == true)

                        return false;

                    cureentDate = cureentDate.AddDays(1);
                }

                order.CreateDate = DateTime.Now;

                order.OrderDate = DateTime.Now.Date;

                SendMail(hostingUnit.Owner.MailAddress, guestRequest.MailAddress);

                DAL_Singletone.Instance.AddOrder(order);

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

            //return true;

        }

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

        public bool UpdateOrder(long orderKey, OrderStatuses orderStatuses)
        {
            try
            {
                Order order = DAL_Singletone.Instance.GetOrderByKey(orderKey);

                if (order.OrderKey == 0 )
                    return false;

                if (order.Status == OrderStatuses.Closed_ApprovedByCustomer)
                    throw new LogicException("Status cannot be updated after order has been approved");

                switch (orderStatuses)
                {

                    case OrderStatuses.MailSent:


                    case OrderStatuses.Closed_ApprovedByCustomer:

                        DAL_Singletone.Instance.UpdateOrder(orderKey, orderStatuses);

                        var guestRequest = DAL_Singletone.Instance.GetGuestRequestByKey(order.GuestRequestKey);

                        if (guestRequest == null)
                        {
                            throw new LogicException($"Guest Request {order.GuestRequestKey} does not exist");
                        }

                        DAL_Singletone.Instance.GetGuestRequestsById(guestRequest.Id).ForEach(gr =>
                        {
                            DAL_Singletone.Instance.UpdateGuestRequestStatus(order.GuestRequestKey, RequestStatus.Inactive);

                        });

                        var hostingUnit = DAL_Singletone.Instance.GetHostingUnitByKey(order.HostingUnitKey);

                        if (!IsUnitAvailableForDates(hostingUnit, guestRequest.EntryDate, (guestRequest.ReleaseDate - guestRequest.EntryDate).Days)){
                            throw new LogicException($"Unit no longer available for these dates.");
                        }

                        DateTime cureentDate = guestRequest.EntryDate;

                        while (cureentDate.Date != guestRequest.ReleaseDate.Date.AddDays(1))
                        {
                            hostingUnit.Diary[cureentDate.Month - 1, cureentDate.Day - 1] = true;

                            cureentDate = cureentDate.AddDays(1);
                        }

                        var fee = Config.FEE_RATE * (guestRequest.ReleaseDate - guestRequest.RegistrationDate).TotalDays;

                        DAL_Singletone.Instance.UpdateHostingUnit(hostingUnit);

                        BL_Singletone.Instance.UpdateGuestRequestStatus(guestRequest.GuestRequestKey, RequestStatus.Inactive);

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

        public List<dynamic> GetOrdersStatusCount()
        {
            return DAL_Singletone.Instance.GetOrderList()
                .GroupBy(o => o.Status)
                .Select(o => new { status = o.Key.ToString(), count = o.Count() }).ToList<dynamic>();
        }
   

        #endregion

        #region host unit manager

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

        public List<HostingUnit> GetHostingUnitsByOwnerId(long id)
        {
            try
            {
                return DAL_Singletone.Instance.GetHostingUnitsByOwnerId(id);                   
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

        public bool DeleteHostingUnit(long HostingUnitKey)
        {
            try
            {
                var hostingUnit = DAL_Singletone.Instance.GetHostingUnitByKey(HostingUnitKey);

                if (HostHasOpenOrders(HostingUnitKey))
                    throw new LogicException("Unit has open orders and cannot be deleted.");

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

        public List<dynamic> GetHostsByUnitsNum()
        {
            try
            {
                return DAL_Singletone.Instance.GetHostingUnitsList()
                    .Select(hu => hu.Owner)
                    .GroupBy(o => o.ID)
                    .Select(g => new { count = g.Count(), hosts = g })
                    .GroupBy(u => u.count)
                    .ToList<dynamic>();

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

        public List<HostingUnit> GetAllHostUnitsWithPool()
        {

            try
            {
                return GetAllHostUnitsByPredicate(hu => hu.HasPool);
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

        public List<GuestRequest> GetAllGuestRequestsForHostUnit(HostingUnit hostingUnit)
        {
            return DAL_Singletone.Instance.GetGuestRequestsList().Where(gr => {

                return gr.Status == RequestStatus.Active
             && CheckAdditionRelevance(gr.Pool, hostingUnit.HasPool)
             && CheckAdditionRelevance(gr.Jacuzzi, hostingUnit.HasJacuzzi)
             && CheckAdditionRelevance(gr.ChildrensAttractions, hostingUnit.HasChildrensAttractions)
             && CheckAdditionRelevance(gr.Garden, hostingUnit.HasGarden)
             && (gr.Adults + gr.Children) <= hostingUnit.NumberOfBeds
             && IsUnitAvailableForDates(hostingUnit, gr.EntryDate, (gr.ReleaseDate - gr.EntryDate).Days)
             && gr.Type == hostingUnit.Type
             && (gr.Area == hostingUnit.Area || gr.Area == VecationAreas.All)
             && !hasOpenApprovedOrder(gr.GuestRequestKey, hostingUnit.HostingUnitKey);

            }).ToList(); 
        }


        public bool hasOpenApprovedOrder(long guestRequestKey, long hostingUnitKey) {

         return DAL_Singletone.Instance.GetOrderList().Any(o => o.GuestRequestKey == guestRequestKey && o.HostingUnitKey == hostingUnitKey && o.Status != OrderStatuses.Closed_NoCustomerResponse);

        }

        public void LoadHostingUnitsDairy()
        {
                try
                {
                    DAL_Singletone.Instance.GetHostingUnitsList().ForEach(h =>

                    DAL_Singletone.Instance.GetOrderList().Where(o => o.Status == BE.OrderStatuses.Closed_ApprovedByCustomer
                    && o.HostingUnitKey == h.HostingUnitKey).ToList()
                    .ForEach(o =>
                    {
                        var gr = GetGuestRequestsByKey(o.GuestRequestKey);

                        if (gr.EntryDate.Date > DateTime.Now.AddMonths(-1).Date &&
                        gr.EntryDate.Date < DateTime.Now.AddMonths(11).Date)
                        {
                            DateTime cureentDate = gr.EntryDate;

                            while (cureentDate.Date != gr.ReleaseDate.Date.AddDays(1))
                            {
                                h.Diary[cureentDate.Month - 1, cureentDate.Day - 1] = true;

                                cureentDate = cureentDate.AddDays(1);
                            }

                        }

                        BL_Singletone.Instance.UpdateHostingUnit(h);
                    }));
                }
                catch (LogicException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new LogicException("General error while loading Hosting Units dairy");
                }
            }
        
        #endregion

        #region general

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

        public int GetDaysBetweenDates(DateTime startDate, [Optional]DateTime? endDate)
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

        #endregion

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

        private delegate bool Predicate<in T>(HostingUnit hu); 

        private List<HostingUnit> GetAllHostUnitsByPredicate(Predicate<HostingUnit> hostHasPred)
        {
            try
            {
                return DAL_Singletone.Instance.GetHostingUnitsList().TakeWhile(hu =>hostHasPred(hu)).ToList();
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

        private bool CheckAdditionRelevance(Additions addition , bool hostUnitHas)
        {
            return ((addition == Additions.Necessary) == hostUnitHas) || (addition == Additions.Possible);
        }

        private bool SendMail(string hostesMail , string mailAddress)
        {
            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            MailMessage mail = new MailMessage();

            mail.To.Add(mailAddress);
            mail.From = new MailAddress("dovi4344@gmail.com");
            mail.Subject = "הזמנה ממארח";
            mail.Body = $"{hostesMail}     :לפרטים";
            mail.IsBodyHtml = true;     

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("dovi4344@gmail.com","Dov301637");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
               
            }
            catch (Exception ex)
            {
                throw new LogicException("Error while sending mail");
            }

            return true;
        }

        private void UpdateOrdersStatus()
        {

            DAL_Singletone.Instance.GetOrderList().Where(o => o.OrderDate < DateTime.Now.Date.AddMonths(-1)).ToList()
                .ForEach(o => DAL_Singletone.Instance.UpdateOrder(o.OrderKey, OrderStatuses.Closed_NoCustomerResponse));

            DAL_Singletone.Instance.GetGuestRequestsList().Where(gr => gr.EntryDate.Date < DateTime.Now.Date)
                .ToList().ForEach(gr => DAL_Singletone.Instance.UpdateGuestRequestStatus(gr.GuestRequestKey, RequestStatus.Inactive));

        }
     
        #endregion

    }
}
