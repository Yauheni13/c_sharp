using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {

        public GroupData(string groupname)
        {
            Groupname = groupname;
        }

        public GroupData()
        {
        }

        public string Groupname { get; set; }

        public string Groupheader { get; set; }

        public string Groupfooter { get; set; }

        public string Id { get; set; }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Groupname == other.Groupname;
        }
        public override int GetHashCode()
        {
            return Groupname.GetHashCode();
        }

        public int CompareTo(GroupData other)
        {
            if(Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Groupname.CompareTo(other.Groupname);
        }

        public override string ToString()
        {
            return "name =" + Groupname + "\n Header = " + Groupheader + "\n Footer = " + Groupfooter;
        }
    }
}
