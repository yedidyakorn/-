using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit : IClonable
    {
        public long HostingUnitKey { get; set; }

        public Host Owner { get; set; }
       
        public string HostingUnitName { get; set; }

        public bool[,] Diary { get; set; } = new bool[12,31];

        public VecationAreas Area { get; set; }

        public bool HasPool { get; set; }

        public bool HasJacuzzi { get; set; }

        public bool HasGarden { get; set; }

        public bool HasChildrensAttractions { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
