using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDal
    {
        void AddGuestRequest(GuestRequest guestRequest); //הוספת דרישת לקוח 

        void UpdateGuestRequestStatus(RequestStatus requestStatus);//עדכון דרישת לקוח

        void AddHostingUnit(HostingUnit hostingUnit);//הוספת יחידת אירוח

        void DeleteHostingUnit(long HostingUnitKey);

        void UpdateHostingUnit(HostingUnit hostingUnit);

        void AddOrder(Order order);

        void UpdateOrder(OrderStatuses orderStatuses);

        List<HostingUnit> GetHostingUnitsList();

        List<GuestRequest> GetGuestRequestsList();

        List<Order> GetOrderList();

        List<BankBranch> GetBankBranchesList();
    }
}
