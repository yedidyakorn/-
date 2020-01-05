using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest : IClonable
    {
        public long GuestRequestKey { get; set; }

        public long Id { get; set; }

        public String PrivateName { get; set; }

        public String FamilyName { get; set; }

        public String MailAddress { get; set; }

        public RequestStatus Status { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public VecationAreas Area { get; set; }

        public HostingUnitTypes Type { get; set; }

        public int Adults { get; set; }

        public int Children { get; set; }

        public Additions Pool { get; set; }

        public Additions Jacuzzi { get; set; }

        public Additions Garden { get; set; }

        public Additions ChildrensAttractions { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
