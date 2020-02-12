using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DataSource
    {
        static public List<GuestRequest> guestRequests = new List<GuestRequest>()
        {
            new GuestRequest()
            {
                GuestRequestKey = 1,
                Adults=2,
                Children= 15,
                EntryDate= DateTime.Now.AddDays(8),
                FamilyName= "goldberg",
                Garden =Additions.Necessary,
                Jacuzzi =Additions.Necessary,
                Pool=Additions.Necessary,
                ChildrensAttractions = Additions.NotInterested,
                PrivateName="Dooobi",
                RegistrationDate=DateTime.Now,
                MailAddress="dovi4344@gmail.com",
                Area= VecationAreas.All,
                Status=RequestStatus.Active,
                Type=HostingUnitTypes.Hotel,
                ReleaseDate=  DateTime.Now.AddDays(10)
               
            },

            new GuestRequest()
            {
                Id =204282560,
                GuestRequestKey = 2,
                Adults=2,
                Children= 10,
                EntryDate= DateTime.Now.AddDays(-5),
                FamilyName= "goldberg",
                Garden =Additions.Necessary,
                Jacuzzi =Additions.Necessary,
                Pool=Additions.Necessary,
                ChildrensAttractions = Additions.NotInterested,
                PrivateName="Chaya",
                RegistrationDate = DateTime.Now,
                MailAddress="chaya1771@gmail.com",
                Area= VecationAreas.North,
                Status=RequestStatus.Active,
                Type=HostingUnitTypes.Hotel,
                ReleaseDate=  DateTime.Now.AddDays(7)
            },

            new GuestRequest()
            {
                Id =2222222,
                GuestRequestKey = 3,
                Adults=2,
                Children= 10,
                EntryDate= DateTime.Now.AddDays(7),
                FamilyName= "goldberg",
                Garden =Additions.Possible,
                Jacuzzi =Additions.Possible,
                Pool=Additions.Possible,
                ChildrensAttractions = Additions.Possible,
                PrivateName="Chaya",
                RegistrationDate = DateTime.Now,
                MailAddress="chaya1771@gmail.com",
                Area= VecationAreas.North,
                Status=RequestStatus.Active,
                Type=HostingUnitTypes.Zimmer,
                ReleaseDate=  DateTime.Now.AddDays(13)         
            },

            new GuestRequest()
            {
                Id =204282560,
                GuestRequestKey = 4,
                Adults=2,
                Children= 10,
                EntryDate= DateTime.Now.AddDays(7),
                FamilyName= "goldberg",
                Garden =Additions.Possible,
                Jacuzzi =Additions.Possible,
                Pool=Additions.Possible,
                ChildrensAttractions = Additions.Possible,
                PrivateName="Chaya",
                RegistrationDate = DateTime.Now,
                MailAddress="chaya1771@gmail.com",
                Area= VecationAreas.All,
                Status=RequestStatus.Active,
                Type=HostingUnitTypes.Zimmer,
                ReleaseDate=  DateTime.Now.AddDays(13)
            },

        };

        static public List<HostingUnit> hostingUnits = new List<HostingUnit>
        {
            new HostingUnit()
            {
                Area = VecationAreas.Center,

                HostingUnitKey= 10000000,

                HostingUnitName = "חלום של צימר",

                HasPool =true,

                Owner = new Host()
                {
                     ID=1234567894,

                    BankAccountNumber = 12345,

                    BankBranchDetails = new BankBranch()
                    {
                        BankName = "לאומי",

                        BankNumber = 10,

                        BranchAddress = "חזון איש 80",

                        BranchCity = "בני ברק",

                        BranchNumber = 856,
                    },

                    CollectionClearance = true,

                    FamilyName = "Goldberg",

                    FhoneNumber = 0527684344,

                    HostKey = 12345678,

                    MailAddress = "dubon@gmail.com",

                    PrivateName = "Israel Dov",

                }
            },

            new HostingUnit()
            {
                HostingUnitKey= 10000001,

                HostingUnitName = "לילה בצימר",

                Owner = new Host()
                {
                    ID=1234567894,

                    BankAccountNumber = 12345,

                    BankBranchDetails = new BankBranch()
                    {
                        BankName = "דיסקונט",

                        BankNumber = 11,

                        BranchAddress = "השלושה 3",

                        BranchCity = "בני ברק",

                        BranchNumber = 856,
                    },

                    CollectionClearance = true,

                    FamilyName = "Goldberg",

                    FhoneNumber = 0527684344,

                    HostKey = 123456789,

                    MailAddress = "haya@gmail.com",

                    PrivateName = "chaya",

                }
            },

            new HostingUnit()
            {
                HostingUnitKey= 10000005,

                HostingUnitName = "לילה בצימר",

                Owner = new Host()
                {
                    ID=123456789,

                    BankAccountNumber = 12345,

                    BankBranchDetails = new BankBranch()
                    {
                        BankName = "דיסקונט",

                        BankNumber = 11,

                        BranchAddress = "השלושה 3",

                        BranchCity = "בני ברק",

                        BranchNumber = 856,
                    },

                    CollectionClearance = true,

                    FamilyName = "Goldberg",

                    FhoneNumber = 0527684344,

                    HostKey = 123456789,

                    MailAddress = "haya@gmail.com",

                    PrivateName = "chaya",

                }
            },

            new HostingUnit()
            {
                HostingUnitKey= 10000002,

                HostingUnitName = "חלום של צימר",

                HasPool =true,

                Owner = new Host()
                {
                    ID=123456789, 

                    BankAccountNumber = 12345,

                    BankBranchDetails = new BankBranch()
                    {
                        BankName = "לאומי",

                        BankNumber = 10,

                        BranchAddress = "חזון איש 80",

                        BranchCity = "בני ברק",

                        BranchNumber = 856,
                    },

                    CollectionClearance = true,

                    FamilyName = "Goldberg",

                    FhoneNumber = 0527684344,

                    HostKey = 12345678,

                    MailAddress = "dubon@gmail.com",

                    PrivateName = "Israel Dov",

                }
            },

            new HostingUnit()
            {
                HostingUnitKey= 10000003,

                HostingUnitName = "חלום של צימר",

                HasPool =true,

                NumberOfBeds = 15,

                Area = VecationAreas.North,

                Type= HostingUnitTypes.Zimmer,

                Owner = new Host()
                {
                    ID=1234567894,

                    BankAccountNumber = 12345,

                    BankBranchDetails = new BankBranch()
                    {
                        BankName = "לאומי",

                        BankNumber = 10,

                        BranchAddress = "חזון איש 80",

                        BranchCity = "בני ברק",

                        BranchNumber = 856,
                    },

                    CollectionClearance = true,

                    FamilyName = "Goldberg",

                    FhoneNumber = 0527684344,

                    HostKey = 12345678,

                    MailAddress = "dubon@gmail.com",

                    PrivateName = "Israel Dov",
                    

                }
            },
        };

        static public List<Order> orders = new List<Order>
        {
            new Order()
            {
                CreateDate = DateTime.Now,
                
                GuestRequestKey = 1,

                HostingUnitKey = 10000000,

                OrderDate = DateTime.Now.AddDays(5),

                OrderKey = 123,

                Status = OrderStatuses.MailSent,
            },

            new Order()
            {
                CreateDate = DateTime.Now.AddMonths(-5),

                GuestRequestKey = 5678,

                HostingUnitKey = 345678,

                OrderDate = DateTime.Now.AddDays(1),

                OrderKey = 456,

                Status = OrderStatuses.NotYetAddressed,
            },

            new Order()
            {
                CreateDate =  DateTime.Now.AddMonths(-10),

                GuestRequestKey = 2,

                HostingUnitKey = 10000001,

                OrderDate = DateTime.Now.AddDays(1),

                OrderKey = 4568,

                Status = OrderStatuses.Closed_ApprovedByCustomer,
            },
              new Order()
            {
                CreateDate =  DateTime.Now.AddMonths(-10),

                GuestRequestKey = 2,

                HostingUnitKey = 10000001,

                OrderDate = DateTime.Now.AddDays(1),

                OrderKey = 456899,

                Status = OrderStatuses.Closed_ApprovedByCustomer,
            }
              ,
              new Order()
            {
                CreateDate =  DateTime.Now.AddMonths(10),

                GuestRequestKey = 2,

                HostingUnitKey = 10000001,

                OrderDate = DateTime.Now.AddDays(1),

                OrderKey = 45687799,

                Status = OrderStatuses.Closed_ApprovedByCustomer,
            }
               ,
              new Order()
            {
                CreateDate =  DateTime.Now,

                GuestRequestKey = 2,

                HostingUnitKey = 10000001,

                OrderDate = DateTime.Now.AddDays(1),

                OrderKey = 45687799,

                Status = OrderStatuses.Closed_ApprovedByCustomer,
            }
        };

    }
}
