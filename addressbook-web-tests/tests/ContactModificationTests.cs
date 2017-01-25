using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : LoginTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            if (!app.Contacthelper.IsThereAnyContact())
            {
                app.Contacthelper.CreateContact(new ContactData("AnyContact"));
            }
            ContactData newcontact = new ContactData("ModifiedName");
            newcontact.Lastname = "MSmith";
            newcontact.Bday = 23;
            newcontact.Bmonth = 10;
            newcontact.Byear = "1977";
            newcontact.Company = "Roga&Copyta";
            newcontact.Email = "Adam.MSmith@RogaAndCopyta.com";
            newcontact.Homepage = "RogaAndCopyte.com";
            newcontact.Group = 1;
            newcontact.Notes = "There is some not too long text, but it depence on point of view";

            List<ContactData> oldcontacts = app.Contacthelper.GetContactList();

            app.Contacthelper.ModifyContact(0, newcontact);

            Assert.AreEqual(oldcontacts.Count, app.Contacthelper.GetContactCount());

            List<ContactData> newcontacts = app.Contacthelper.GetContactList();
            oldcontacts[0].Firstname = newcontact.Firstname;
            oldcontacts[0].Lastname = newcontact.Lastname;
            newcontacts.Sort();
            oldcontacts.Sort();
            Assert.AreEqual(oldcontacts, newcontacts);

        }

    }
}
