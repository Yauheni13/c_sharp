using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allphones;
        private string allemails;

        public ContactData(string firstname)
        {
            Firstname = firstname;
        }

        public ContactData()
        {
        }

        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Homephone { get; set; }
        public string Mobilephone { get; set; }
        public string Workphone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public int Bday { get; set; }
        public int Bmonth { get; set; }
        public string Byear { get; set; }
        public int Aday { get; set; }
        public int Amonth { get; set; }
        public string Ayear { get; set; }
        public int Group { get; set; }
        public string Secondaddress { get; set; }
        public string Secondhome { get; set; }
        public string Notes { get; set; }
        public string Allphones
        {
            get
            {
                if (allphones != null)
                {
                    return allphones;
                }
                return (CleanUp(Homephone) + CleanUp(Mobilephone) + CleanUp(Workphone)).Trim();
            }
            set
            {
                allphones = value;
            }
        }

        public string Allemails
        {
            get
            {
                if (allemails != null)
                {
                    return allemails;
                }
                return (Email + "\r\n" + Email2 + "\r\n" + Email3).Trim();
            }
            set
            {
                allemails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (Firstname == other.Firstname && Lastname == other.Lastname)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Lastname.CompareTo(other.Lastname) == 0)
            {
                return Firstname.CompareTo(other.Firstname);
            }
            return Lastname.CompareTo(other.Lastname);
        }
    }
}
