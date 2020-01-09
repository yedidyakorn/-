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
        bool AddGuestRequest(GuestRequest guestRequest); //הוספת דרישת לקוח 

        bool UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus);//עדכון דרישת לקוח

        bool AddHostingUnit(HostingUnit hostingUnit);//הוספת יחידת אירוח

        bool DeleteHostingUnit(long HostingUnitKey);

        bool UpdateHostingUnit(HostingUnit hostingUnit);

        bool AddOrder(Order order);

        bool UpdateOrder(long orderKey, OrderStatuses orderStatuses);

        List<HostingUnit> GetHostingUnitsList();

        List<GuestRequest> GetGuestRequestsList();

        List<Order> GetOrderList();

        List<BankBranch> GetBankBranchesList();

        List<HostingUnit> GetAllAvailableUnitsForDate(DateTime startDate, int daysNum);

        int GetDaysBetweenDates(DateTime startDate, [Optional]DateTime? endDate);

        List<Order> GetOrdersForDays(int days);

        int GetOrdersNumForGR(GuestRequest guestRequest);

        int GetApprovedOrdersNumForHU(HostingUnit hostingUnit);

        List<IGrouping<VecationAreas, GuestRequest>> GetGRListGroupByArea();

        List<IGrouping<int, GuestRequest>> GetGRListGroupByVacationers();

        List<dynamic> GetHostsByUnitsNum();

        List<IGrouping<VecationAreas, HostingUnit>> GetHUListGroupByArea();
   
        List<HostingUnit> GetAllHostUnitsWithPool();
    }
}
