using DS.XML_DATA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DS
{
    public class DataSourceXml
    {
        private static string solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

        private static string filePath = System.IO.Path.Combine(solutionDirectory, "DS", "XML_DATA");

        private static XElement orderRoot = null;
        private static XElement guestRequestRoot = null;
        private static XElement hostingUnitRoot = null;


        private static string orderPath = Path.Combine(filePath, "OrderXml.xml");
        private static string guestRequestPath = Path.Combine(filePath, "GuestRequestXml.xml");
        private static string hostingUnitPath = Path.Combine(filePath, "HostingUnitXml.xml");


        static DataSourceXml()
        {
            bool exists = Directory.Exists(filePath);
            if (!exists)
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(orderPath))
            {
                CreateFile(new OrdersXml(), orderPath);
            }
            orderRoot = LoadData(orderPath);

            if (!File.Exists(hostingUnitPath))
            {
                CreateFile(new HostingUnitsXml(), hostingUnitPath);

            }
            hostingUnitRoot = LoadData(hostingUnitPath);


            if (!File.Exists(guestRequestPath))
            {
                CreateFile(new GuestRequestsXml(), guestRequestPath);
            }
            guestRequestRoot = LoadData(guestRequestPath);

        }

        private static void CreateFile<T>(T obj, string path)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, obj);
                    XElement root = XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                    root.Save(path);
                }
            }    
          
        }

        public static void SaveOrders()
        {
            orderRoot.Save(orderPath);
        }

        public static void SaveHostingUnits()
        {
            hostingUnitRoot.Save(hostingUnitPath);
        }

        public static void SaveGuestRequests()
        {
            guestRequestRoot.Save(guestRequestPath);
        }

        public static XElement Orders
        {
            get
            {
                orderRoot = LoadData(orderPath);
                return orderRoot;
            }
        }

        public static XElement HostingUnits
        {
            get
            {
                hostingUnitRoot = LoadData(hostingUnitPath);
                return hostingUnitRoot;
            }
        }

        public static XElement GuestRequests
        {
            get
            {
                guestRequestRoot = LoadData(guestRequestPath);
                return guestRequestRoot;
            }
        }

        private static XElement LoadData(string path)
        {
            XElement root;
            try
            {
                root = XElement.Load(path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
            return root;
        }
    }
}
