using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : LoginTestBase
    {
        [Test]
        public void ValidDataSearchTest()
        {
            string searchtext = "Adam";
            List<ContactData> allcontacts = app.Contacthelper.GetAllMatchingContacts(searchtext);
            if (allcontacts != null)
            {
                foreach (ContactData con in allcontacts)
                {
                    Assert.True(con.Firstname.Contains(searchtext) || con.Lastname.Contains(searchtext)
                           || con.Address.Contains(searchtext) || con.Allemails.Contains(searchtext) ||
                           con.Allphones.Contains(searchtext));
                }
            }
            Assert.AreEqual(allcontacts.Count, app.Contacthelper.GetSearchCountFromScreen(searchtext));
        }

        [Test]
        public void InValidDataSearchTest()
        {
            string searchtext = "kkkkk";
            List<ContactData> allcontacts = app.Contacthelper.GetAllMatchingContacts(searchtext);
            if (allcontacts != null)
            {
                foreach (ContactData con in allcontacts)
                {
                    Assert.True(con.Firstname.Contains(searchtext) || con.Lastname.Contains(searchtext)
                           || con.Address.Contains(searchtext) || con.Allemails.Contains(searchtext) ||
                           con.Allphones.Contains(searchtext));
                }
            }
            Assert.AreEqual(allcontacts.Count, app.Contacthelper.GetSearchCountFromScreen(searchtext));
        }
    }
}
