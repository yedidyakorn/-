using System;
using System.Linq;
using BE;
using BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BL_TESTS
{
    [TestClass]
    public class BLUnitTest
    {
        [TestMethod]
        public void AddGuestRequestTest()
        {

            BL_Singletone.Instance.AddGuestRequest(new GuestRequest()
            {

                Adults = 2,
                Children = 15,
                EntryDate = DateTime.Now.AddDays(8),
                FamilyName = "goldberg2",
                Garden = Additions.Necessary,
                Jacuzzi = Additions.Necessary,
                Pool = Additions.Necessary,
                ChildrensAttractions = Additions.NotInterested,
                PrivateName = "Dooobi",
                RegistrationDate = DateTime.Now,
                MailAddress = "dovi4344@gmail.com",
                Area = VecationAreas.North,
                Status = RequestStatus.Active,
                Type = HostingUnitTypes.Hotel,
                ReleaseDate = DateTime.Now.AddDays(10)

            });

        }
        [TestMethod]
        public void AddHostingUnit()
        {
            BL_Singletone.Instance.AddHostingUnit(new HostingUnit()
            {

                HostingUnitName = "צימר",

                Owner = new Host()
                {
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

                    FamilyName = "Goldberg2",

                    FhoneNumber = 0527684344,

                    HostKey = 12345678,

                    MailAddress = "dubon@gmail.com",

                    PrivateName = "Israel Dov",

                }

            });
        }

        [TestMethod]
        public void AddOrder()
        {
            BL_Singletone.Instance.AddOrder(new Order()
            { 
                CreateDate = DateTime.Now,

                GuestRequestKey = 2,

                HostingUnitKey = 10000000,

                OrderDate = DateTime.Now.AddDays(1),

                OrderKey = 1234,

                Status = OrderStatuses.MailSent,
            });
        }

        [TestMethod]
        public void DeleteHostingUnit()
        {
            BL_Singletone.Instance.DeleteHostingUnit(10000000);
        }

        [TestMethod]
        public void GetBankBranchesList()
        {
            var t =BL_Singletone.Instance.GetBankBranchesList();
        }

        [TestMethod]
        public void GetGuestRequestsList()
        {
            var t = BL_Singletone.Instance.GetGuestRequestsList();
        }

        [TestMethod]
        public void GetHostingUnitsList()
        {
            var t = BL_Singletone.Instance.GetHostingUnitsList();
        }

        [TestMethod]
        public void GetOrderList()
        {
            var t = BL_Singletone.Instance.GetOrderList();
        }

        [TestMethod]
        public void UpdateGuestRequestStatus()
        {
            BL_Singletone.Instance.UpdateGuestRequestStatus(1, RequestStatus.Inactive);
        }

        [TestMethod]
        public void UpdateHostingUnit()
        {
            var host = BL_Singletone.Instance.GetHostingUnitsList().First();
            host.HasGarden = !host.HasGarden;
            BL_Singletone.Instance.UpdateHostingUnit(host);
        }

        [TestMethod]
        public void UpdateOrder()
        {
            var order = BL_Singletone.Instance.GetOrderList().First();
            BL_Singletone.Instance.UpdateOrder(order.OrderKey , OrderStatuses.Closed_ApprovedByCustomer);
            order = BL_Singletone.Instance.GetOrderList().First();
            Assert.IsTrue(order.Status ==OrderStatuses.Closed_ApprovedByCustomer);
        }

        [TestMethod]
        public void GetAllAvailableUnitsForDate()
        {
            var units = BL_Singletone.Instance.GetAllAvailableUnitsForDate(DateTime.Now, 5);
            Assert.IsTrue(units.Count() > 0);
        }

        [TestMethod]
        public void GetDaysBetweenDates()
        {
            var days = BL_Singletone.Instance.GetDaysBetweenDates(DateTime.Now.AddDays(-5));
            Assert.IsTrue(days == 5);
        }

        [TestMethod]
        public void GetOrdersForDays()
        {
            var orders = BL_Singletone.Instance.GetOrdersForDays(5);
            //Assert.IsTrue(orders.Count() > 0 );
        }

        [TestMethod]
        public void GetOrdersNumForGR()
        {
            var orders = BL_Singletone.Instance.GetOrdersNumForGR(BL_Singletone.Instance.GetGuestRequestsList().First());
           // Assert.IsTrue(orders > 0);
        }

        [TestMethod]
        public void GetApprovedOrdersNumForHU()
        {
            var orders = BL_Singletone.Instance.GetApprovedOrdersNumForHU(BL_Singletone.Instance.GetHostingUnitsList().First());
            //Assert.IsTrue(orders > 0);
        }

        [TestMethod]
        public void getGRListGroupByArea()
        {
            var gr = BL_Singletone.Instance.GetGRListGroupByArea();
            Assert.IsTrue(gr.Count()>0);
        }

        [TestMethod]
        public void getGRListGroupByVacationers()
        {
            var gr = BL_Singletone.Instance.GetGRListGroupByVacationers();
            Assert.IsTrue(gr.Count() > 0);
        }

        [TestMethod]
        public void getHostsByUnitsNum()
        {
            var gr = BL_Singletone.Instance.GetHostsByUnitsNum();
            Assert.IsTrue(gr.Count() > 0);
        }

        [TestMethod]
        public void getHUListGroupByArea()
        {
            var gr = BL_Singletone.Instance.GetHUListGroupByArea();
            Assert.IsTrue(gr.Count() > 0);
        }

        [TestMethod]
        public void getAllHostUnitsWithPool()
        {
            var gr = BL_Singletone.Instance.GetAllHostUnitsWithPool();
           Assert.IsTrue(gr.Count() > 0);
        }
    }
}
