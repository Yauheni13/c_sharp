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
    public class ContactRemovalTests : LoginTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contacthelper.IsThereAnyContact())
            {
                app.Contacthelper.CreateContact(new ContactData("AnyContact"));
            }
            List<ContactData> oldcontacts = app.Contacthelper.GetContactList();
            app.Contacthelper.RemoveContact(0);

            Assert.AreEqual(oldcontacts.Count - 1, app.Contacthelper.GetContactCount());

            List<ContactData> newcontacts = app.Contacthelper.GetContactList();
            Assert.AreEqual(oldcontacts.Count() - 1, newcontacts.Count());
            oldcontacts.RemoveAt(0);
            oldcontacts.Sort();
            newcontacts.Sort();
            Assert.AreEqual(oldcontacts, newcontacts);
        }

    }
}
