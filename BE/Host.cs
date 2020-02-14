using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public Host()
        {
            BankBranchDetails = new BankBranch() { };
        }

        public long ID { get; set; }

        public long HostKey { get; set; }

        public String PrivateName { get; set; }

        public String FamilyName { get; set; }

        public long FhoneNumber { get; set; }

        public String MailAddress { get; set; }

        public BankBranch BankBranchDetails { get; set; }

        public long BankAccountNumber { get; set; }

        public bool CollectionClearance { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
