using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        /// <summary>
        /// add Guest Request
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <returns>bool</returns>
        bool AddGuestRequest(GuestRequest guestRequest);

        /// <summary>
        /// update Guest Request
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <returns>bool</returns>
        bool UpdateGuestRequest(GuestRequest guestRequest);

        /// <summary>
        /// Delete Guest Request
        /// </summary>
        /// <param name="guestRequestKey"></param>
        /// <returns>bool</returns>
        bool DeleteGuestRequestByKey(long guestRequestKey);

        /// <summary>
        /// Update Guest Request Status
        /// </summary>
        /// <param name="GuestRequestKey"></param>
        /// <param name="requestStatus"></param>
        /// <returns>bool</returns>
        void UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus);

        /// <summary>
        /// Add Hosting Unit
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        void AddHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// Delete Hosting Unit
        /// </summary>
        /// <param name="HostingUnitKey"></param>
        /// <returns></returns>
        void DeleteHostingUnit(long HostingUnitKey);

        /// <summary>
        /// Update Hosting Unit
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns>bool</returns>
        void UpdateHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns>bool</returns>
        void AddOrder(Order order);

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="orderKey"></param>
        /// <param name="orderStatuses"></param>
        /// <returns>bool</returns>
        void UpdateOrder(long orderKey, OrderStatuses orderStatuses);


        /// <summary>
        /// Get Hosting Units By Owner ID
        /// </summary>
        /// <returns>Hosting Unit list</returns>
        List<HostingUnit> GetHostingUnitsByOwnerId(long id);

        /// <summary>
        /// Get Hosting Units List
        /// </summary>
        /// <returns>Hosting Unit list</returns>
        List<HostingUnit> GetHostingUnitsList();

        /// <summary>
        /// Get Guest Requests List
        /// </summary>
        /// <returns>Guest Requests List</returns>
        List<GuestRequest> GetGuestRequestsList();

        /// <summary>
        /// GetOrderList
        /// </summary>
        /// <returns>Order List</returns>
        List<Order> GetOrderList();

        /// <summary>
        /// GetBankBranchesList
        /// </summary>
        /// <returns>Bank Branches List</returns>
        List<BankBranch> GetBankBranchesList();

        /// <summary>
        /// get order by key
        /// </summary>
        /// <param name="orderKey"></param>
        /// <returns>order</returns>
        Order GetOrderByKey(long orderKey);

        /// <summary>
        /// Get Guest Request By Key
        /// </summary>
        /// <param name="guestRequestKey"></param>
        /// <returns>Guest Request</returns>
        GuestRequest GetGuestRequestByKey(long guestRequestKey);

        /// <summary>
        /// Get Guest Requests By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<GuestRequest> GetGuestRequestsById(long id);

        /// <summary>
        /// Get Hosting Unit By Key
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        /// <returns>Hosting Unit</returns>
        HostingUnit GetHostingUnitByKey(long hostingUnitKey);


    }
}
