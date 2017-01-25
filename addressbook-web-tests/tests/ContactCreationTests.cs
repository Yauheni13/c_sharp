using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : LoginTestBase
    {

        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData()
                {
                    Firstname = GenerateRandomString(20),
                    Lastname = GenerateRandomString(20),
                    Middlename = GenerateRandomString(20),
                    Nickname = GenerateRandomString(10),
                    Bday = Convert.ToInt32(rnd.NextDouble() * 30),
                    Bmonth = Convert.ToInt32(rnd.NextDouble() * 12),
                    Byear = Convert.ToString(Convert.ToInt32(rnd.NextDouble() * 10)) + 
                        Convert.ToString(Convert.ToInt32(rnd.NextDouble() * 10))+
                        Convert.ToString(Convert.ToInt32(rnd.NextDouble() * 10))+
                        Convert.ToString(Convert.ToInt32(rnd.NextDouble() * 10)),
                    Company = GenerateRandomString(30),
                    Email2 = GenerateRandomString(30),
                    Homephone = GenerateRandomString(10),
                    Homepage = GenerateRandomString(40),
                    Notes = GenerateRandomString(100),
                    Fax = GenerateRandomString(10)
                });
            }
            return contacts;
        }
        public static IEnumerable<ContactData> XMLFileContactDataProvider()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).
                Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> JSONFileContactDataProvider()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }


        [Test, TestCaseSource("XMLFileContactDataProvider")]
        public void CreationNewContactTest(ContactData newcontact)
        {
           
            List<ContactData> oldcontacts = app.Contacthelper.GetContactList();

            app.Contacthelper.CreateContact(newcontact);

            Assert.AreEqual(oldcontacts.Count + 1, app.Contacthelper.GetContactCount());

            List<ContactData> newcontacts = app.Contacthelper.GetContactList();
            Assert.AreEqual(oldcontacts.Count() + 1, newcontacts.Count());
            oldcontacts.Add(newcontact);
            newcontacts.Sort();
            oldcontacts.Sort();
            Assert.AreEqual(oldcontacts, newcontacts);
        }
    }
}
