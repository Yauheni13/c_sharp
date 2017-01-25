using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : LoginTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            if (!app.Contacthelper.IsThereAnyContact())
            {
                app.Contacthelper.CreateContact(new ContactData("AnyContact"));
            }

            ContactData fromtable = app.Contacthelper.GetContactInformationFromTable(0);
            ContactData fromform = app.Contacthelper.GetContactInformationFromForm(0);

            Assert.AreEqual(fromtable, fromform);
            Assert.AreEqual(fromtable.Address, fromform.Address);
            Assert.AreEqual(fromtable.Allphones, fromform.Allphones);


        }

        [Test]
        public void CompareContactInformationTest()
        {
            ContactData contactin = app.Contacthelper.GetContactInformationFromForm(2);
            ContactData contactout = app.Contacthelper.GetContactInformationFromScreen(2, contactin);

            Assert.AreEqual(contactin, contactout);
            Assert.AreEqual(contactin.Address, contactout.Address);
            Assert.AreEqual(contactin.Homephone, contactout.Homephone);
            Assert.AreEqual(contactin.Workphone, contactout.Workphone);
            Assert.AreEqual(contactin.Bday, contactout.Bday);
            Assert.AreEqual(contactin.Bmonth, contactout.Bmonth);
            Assert.AreEqual(contactin.Byear, contactout.Byear);
            Assert.AreEqual(contactin.Company, contactout.Company);
            Assert.AreEqual(contactin.Email, contactout.Email);
            Assert.AreEqual(contactin.Mobilephone, contactout.Mobilephone);
            Assert.AreEqual(contactin.Notes, contactout.Notes);
        }
        [Test]
        public void CompareContactInformationTest2()
        {
            string fromform = app.Contacthelper.GetAllContactInformationFromForm(2);
            string fromscreen = app.Contacthelper.GetAllContactInformationFromScreen(2);
            
            Assert.AreEqual(fromform, fromscreen);
        }

    }
}
