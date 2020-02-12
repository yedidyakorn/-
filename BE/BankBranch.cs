using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    [XmlRoot(ElementName = "ATM")]
    public class BankBranch :IClonable
    {
        [XmlElement("קוד_בנק")]
        public int BankNumber { get; set; }

        [XmlElement("שם_בנק")]
        public String BankName { get; set; }

        [XmlElement("קוד_סניף")]
        public int BranchNumber { get; set; }

        [XmlElement("כתובת_ה-ATM")]
        public String BranchAddress { get; set; }

        [XmlElement("ישוב")]
        public String BranchCity { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
