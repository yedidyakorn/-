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
        void AddGuestRequest(GuestRequest guestRequest); //הוספת דרישת לקוח 

        void UpdateGuestRequestStatus(long GuestRequestKey, RequestStatus requestStatus);//עדכון דרישת לקוח

        void AddHostingUnit(HostingUnit hostingUnit);//הוספת יחידת אירוח

        void DeleteHostingUnit(long HostingUnitKey);

        void UpdateHostingUnit(HostingUnit hostingUnit);

        void AddOrder(Order order);

        void UpdateOrder(long orderKey, OrderStatuses orderStatuses);

        List<HostingUnit> GetHostingUnitsList();

        List<GuestRequest> GetGuestRequestsList();

        List<Order> GetOrderList();

        List<BankBranch> GetBankBranchesList();
    }
}
