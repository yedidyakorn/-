﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch :IClonable
    {
        public int BankNumber { get; set; }

        public String BankName { get; set; }

        public int BranchNumber { get; set; }

        public String BranchAddress { get; set; }

        public String BranchCity { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
