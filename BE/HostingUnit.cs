using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }

        public Host Owner { get; set; }
       
        public string HostingUnitName { get; set; }

        public bool[,] Diary;

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
