using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit : IClonable
    {
        public HostingUnit()
        {
            Owner = new Host(){};
        }

        public long HostingUnitKey { get; set; }

        public Host Owner { get; set; }
       
        public string HostingUnitName { get; set; }

        [XmlIgnore]
        public bool[,] Diary { get; set; } = new bool[12,31];

        public VecationAreas Area { get; set; }

        public bool HasPool { get; set; }

        public bool HasJacuzzi { get; set; }

        public int NumberOfBeds { get; set; }

        public bool HasGarden { get; set; }

        public HostingUnitTypes Type { get; set; }

        public bool HasChildrensAttractions { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
