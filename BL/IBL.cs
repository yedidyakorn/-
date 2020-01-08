using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        void AddGuestRequest(GuestRequest guestRequest); //הוספת דרישת לקוח 

        void UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus);//עדכון דרישת לקוח

        void AddHostingUnit(HostingUnit hostingUnit);//הוספת יחידת אירוח

        bool DeleteHostingUnit(long HostingUnitKey);

        bool UpdateHostingUnit(HostingUnit hostingUnit);

        bool AddOrder(Order order);

        void UpdateOrder(long orderKey, OrderStatuses orderStatuses);

        List<HostingUnit> GetHostingUnitsList();

        List<GuestRequest> GetGuestRequestsList();

        List<Order> GetOrderList();

        List<BankBranch> GetBankBranchesList();

        List<HostingUnit> GetAllAvailableUnitsForDate(DateTime startDate, int daysNum);

        int GetDaysBetweenDates(DateTime startDate, [Optional]DateTime? endDate);

        List<Order> GetOrdersForDays(int days);

        int GetOrdersNumForGR(GuestRequest guestRequest);

        int GetApprovedOrdersNumForHU(HostingUnit hostingUnit);

        List<IGrouping<VecationAreas, GuestRequest>> getGRListGroupByArea();

        List<IGrouping<int, GuestRequest>> getGRListGroupByVacationers();

        List<dynamic> getHostsByUnitsNum();

        List<IGrouping<VecationAreas, HostingUnit>> getHUListGroupByArea();
   
        List<HostingUnit> getAllHostUnitsWithPool();
    }
}
