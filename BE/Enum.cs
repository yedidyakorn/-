using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum RequestStatus
    {
        Active,

        Inactive,
    }

    public enum VecationAreas
    {
        All,

        North,

        South,

        Center,

        Jerusalem,
    }

    public enum HostingUnitTypes
    {
        Zimmer,

        Hotel,

        Camping,
    }

    public enum Additions
    {
        Necessary,

        Possible,

        NotInterested,
    }

    public enum OrderStatuses
    {
        NotYetAddressed,

        MailSent,

        Closed_NoCustomerResponse,

        Closed_ApprovedByCustomer,
    }
}
