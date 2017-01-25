using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public List<ContactData> GetAllMatchingContacts(string searchtext)
        {
            IList<IWebElement> rows = GetCountOfSearchingContacts(searchtext);
            int search = rows.Count;
            List<ContactData> allmatchingcontacts = new List<ContactData>();
            for (int i = 0; i < search; i++)
            {
                IList<IWebElement> cells = rows[i].FindElements(By.TagName("td"));
                string lastname = cells[1].Text;
                string firstname = cells[2].Text;
                string address = cells[3].Text;
                string allemails = cells[4].Text;
                string allphones = cells[5].Text;
                ContactData contact = new ContactData()
                {
                    Firstname = firstname,
                    Lastname = lastname,
                    Address = address,
                    Allemails = allemails,
                    Allphones = allphones
                };
                allmatchingcontacts.Add(contact);
            }
            return allmatchingcontacts;
        }

        public int GetSearchCountFromScreen(string searchtext)
        {
            manager.Navigator.GoToHomePage();
            driver.FindElement(By.Name("searchstring")).Clear();
            driver.FindElement(By.Name("searchstring")).SendKeys(searchtext);
            return Int32.Parse(driver.FindElement(By.Id("search_count")).Text);
        }

        public IList<IWebElement> GetCountOfSearchingContacts(string searchtext)
        {
            manager.Navigator.GoToHomePage();
            driver.FindElement(By.Name("searchstring")).Clear();
            driver.FindElement(By.Name("searchstring")).SendKeys(searchtext);
            IList<IWebElement> rows = driver.FindElements(By.Name("entry"));
            List<IWebElement> newrows = new List<IWebElement>();
            foreach (IWebElement element in rows)
            {
                if (element.Displayed) newrows.Add(element);
            }
            return newrows;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].
                                            FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allemails = cells[4].Text;
            string allphones = cells[5].Text;
            return new ContactData()
            {
                Firstname = firstname,
                Lastname = lastname,
                Address = address,
                Allemails = allemails,
                Allphones = allphones
            };
        }

        public ContactData GetContactInformationFromScreen(int index, ContactData contactin)
        {
            ContactData contactout = new ContactData();
            manager.Navigator.GoToHomePage();
            InitContactInformation(index);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div#content")));
            string allinfo = driver.FindElement(By.CssSelector("div#content")).Text;
            if (allinfo == "") return contactout;
            string[] allinfoset;
            int nextindex = 0;
            if (allinfo != "")
            {
                allinfoset = allinfo.Split('\n');
            }
            else return contactout;

            int i = 0;
            int len = allinfoset.Length;
            do
            {
                if (allinfoset[i] == "\r")
                {
                    allinfoset = allinfoset.Where((source, thisindex) => thisindex != i).ToArray();
                    len = allinfoset.Length;
                }
                else i++;

            } while (i < len);

            string names = contactin.Firstname + " " + contactin.Middlename + " " + contactin.Lastname;

            if (names != "")
            {
                string[] namesparts = allinfoset[nextindex].Split(' ');
                nextindex++;
                if (contactin.Firstname != "")
                {
                    contactout.Firstname = namesparts[0].Replace("\r", "");
                    if (contactin.Middlename != "")
                    {
                        contactout.Middlename = namesparts[1].Replace("\r", "");
                        if (contactin.Lastname != "")
                        {
                            contactout.Lastname = namesparts[2].Replace("\r", "");
                        }
                        else contactout.Lastname = "";
                    }
                    else
                    {
                        contactout.Middlename = "";
                        if (contactin.Lastname != "")
                        {
                            contactout.Lastname = namesparts[1].Replace("\r", "");
                        }
                        else contactout.Lastname = "";
                    }
                }else
                {
                    contactout.Firstname = "";
                    if (contactin.Middlename != "")
                    {
                        contactout.Middlename = namesparts[0].Replace("\r", "");
                        if (contactin.Lastname != "")
                        {
                            contactout.Lastname = namesparts[1].Replace("\r", "");
                        }
                    }else
                    {
                        contactout.Middlename = "";
                        if (contactin.Lastname != "")
                        {
                            contactout.Lastname = namesparts[0].Replace("\r", "");
                        }
                        else contactout.Lastname = "";
                    }
                }
            }
            if (contactin.Nickname != "")
            {
                contactout.Nickname = allinfoset[nextindex].Replace("\r", "");
                nextindex++;
            }
            else contactout.Nickname = "";
            if (contactin.Title != "")
            {
                contactout.Title = allinfoset[nextindex].Replace("\r", "");
                nextindex++;
            }
            else contactout.Title = "";
            if (contactin.Company != "")
            {
                contactout.Company = allinfoset[nextindex].Replace("\r", "");
                nextindex++;
            }
            else contactout.Company = "";
            if (contactin.Address != "")
            {
                contactout.Address = allinfoset[nextindex].Replace("\r", "");
                nextindex++;
            }
            else contactout.Address = "";
            if (contactin.Homephone!= "")
            {
                contactout.Homephone = CleanPhone(allinfoset[nextindex]).Replace("\r", "");
                nextindex++;
            }
            else contactout.Homephone = "";
            if (contactin.Mobilephone != "")
            {
                contactout.Mobilephone = CleanPhone(allinfoset[nextindex]).Replace("\r", "");
                nextindex++;
            }
            else contactout.Mobilephone= "";
            if (contactin.Workphone != "")
            {
                contactout.Workphone = CleanPhone(allinfoset[nextindex]).Replace("\r", "");
                nextindex++;
            }
            else contactout.Workphone = "";
            if (contactin.Fax != "")
            {
                contactout.Fax = CleanPhone(allinfoset[nextindex]).Replace("\r", "");
                nextindex++;
            }
            else contactout.Fax = "";
            if (contactin.Email != "")
            {
                contactout.Email = CleanEmail(allinfoset[nextindex]).Replace("\r", "");
                nextindex++;
            }
            else contactout.Email = "";
            if (contactin.Email2 != "")
            {
                contactout.Email2 = CleanEmail(allinfoset[nextindex]).Replace("\r", "");
                nextindex++;
            }
            else contactout.Email2 = "";
            if (contactin.Email3 != "")
            {
                contactout.Email3 = CleanEmail(allinfoset[nextindex]).Replace("\r", "");
                nextindex++;
            }
            else contactout.Email3 = "";
            if (contactin.Homepage != "")
            {
                nextindex++;
                contactout.Homepage = allinfoset[nextindex].Replace("\r", "");
                nextindex++;
            }
            else contactout.Homepage = "";
            if (contactin.Bday != 0 || contactin.Bmonth != 0 || contactin.Byear != "")
            {
                int indexin = 1;
                string[] partsin = allinfoset[nextindex].Split(' ');
                if (contactin.Bday != 0)
                {
                    contactout.Bday = int.Parse(partsin[indexin].Replace(".", ""));
                    indexin++;
                }
                else contactout.Bday = 0;
                if (contactin.Bmonth != 0)
                {
                    contactout.Bmonth = GetSelectedMonth(partsin[indexin]);
                    indexin++;
                }
                else contactout.Bmonth = 0;
                if (contactin.Byear != "")
                {
                    contactout.Byear = partsin[indexin].Replace("\r", "");
                    indexin++;
                }
                else contactout.Byear = "";
                nextindex++;
            }

            if (contactin.Aday != 0 || contactin.Amonth != 0 || contactin.Ayear != "")
            {
                int indexin = 1;
                string[] partsin = allinfoset[nextindex].Split(' ');
                if (contactin.Aday != 0)
                {
                    contactout.Aday = int.Parse(partsin[indexin].Replace(".", ""));
                    indexin++;
                }
                else contactout.Aday = 0;
                if (contactin.Amonth != 0)
                {
                    contactout.Amonth = GetSelectedMonth(partsin[indexin]);
                    indexin++;
                }
                else contactout.Amonth = 0;
                if (contactin.Ayear != "")
                {
                    contactout.Ayear = partsin[indexin].Replace("\r", "");
                    indexin++;
                }
                else contactout.Ayear = "";
                nextindex++;
            }
            if (contactin.Secondaddress != "")
            {
                contactout.Secondaddress = allinfoset[nextindex].Replace("\r", "");
                nextindex++;
            }
            else contactout.Secondaddress = "";

            if (contactin.Secondhome != "")
            {
                contactout.Secondhome = CleanPhone(allinfoset[nextindex]).Replace("\r", "");
                nextindex++;
            }
            else contactout.Secondhome = "";
            if (contactin.Notes != "")
            {
                contactout.Notes = allinfoset[nextindex].Replace("\r", "");
                nextindex++;
            }
            else contactout.Notes = "";
            return contactout;
        }

        public string GetAllContactInformationFromScreen(int v)
        {
            manager.Navigator.GoToHomePage();
            InitContactInformation(v);
            return driver.FindElement(By.CssSelector("div#content")).Text;
        }

        public string GetAllContactInformationFromForm(int v)
        {
            ContactData contact = GetContactInformationFromForm(v);
            return contact.Firstname + " " + contact.Middlename + " " + contact.Lastname + "\r\n" +
                contact.Nickname + "\r\n" +
                contact.Title + "\r\n" +
                contact.Company + "\r\n" +
                contact.Address + "\r\n" +
                "\r\n" +
                "H: " + contact.Homephone + "\r\n" +
                "M: " + contact.Mobilephone + "\r\n" +
                "W: " + contact.Workphone + "\r\n" +
                "F: " + contact.Fax + "\r\n" +
                "\r\n" +
                contact.Email + " " + AddDomain(contact.Email) + "\r\n" +
                contact.Email2 + " " + AddDomain(contact.Email2) + "\r\n" +
                contact.Email3 + " " + AddDomain(contact.Email3) + "\r\n" +
                "Homepage:" + "\r\n" +
                contact.Homepage + "\r\n" +
                "\r\n" +

                "Birthday " + contact.Bday + ". " + GetMonth(contact.Bmonth) + " " + contact.Byear 
                +" " + GetAge(new DateTime(int.Parse(contact.Byear), contact.Bmonth, contact.Bday)) + "\r\n" +

                "Anniversary " + contact.Aday + ". " + GetMonth(contact.Amonth) + " " + contact.Ayear + " " 
                + GetAge(new DateTime(int.Parse(contact.Ayear), contact.Amonth, contact.Aday)) + "\r\n" +
                
                "\r\n" +
                contact.Secondaddress + "\r\n" +
                "\r\n" +
                "P: " + contact.Secondhome + "\r\n" +
                "\r\n" +
                contact.Notes;

        }

        public string GetMonth(int bmonth)
        {
            if (bmonth == 1) return "January";
            if (bmonth == 2) return "February";
            if (bmonth == 3) return "March";
            if (bmonth == 4) return "April";
            if (bmonth == 5) return "May";
            if (bmonth == 6) return "June";
            if (bmonth == 7) return "July";
            if (bmonth == 8) return "August";
            if (bmonth == 9) return "September";
            if (bmonth == 10) return "October";
            if (bmonth == 11) return "November";
            if (bmonth == 12) return "December";
            return "";
        }

        public string GetAge(DateTime date)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - date.Year;
            if (date > now.AddYears(-age)) age--;
            
            return "(" + age.ToString() + ")";
        }

        public string AddDomain(string email)
        {
            string[] emailparts = email.Split('@');
            if (emailparts.Length == 1)
            {
                if (email != "")
                {
                    return "(www." + email.Remove(0, 1) + ")";
                }
                else return "";
            }
            else return "(www." + emailparts[1] + ")";
        }

        public string CleanEmail(string v)
        {
            string[] s = v.Split(' ');
            return s[0];
        }

        public string CleanPhone(string v)
        {
            string[] s = v.Split(' ');
            return s[1];
        }

        public ContactData GetContactInformationFromForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);

            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("firstname")));
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homephone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilephone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workphone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string faxphone = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            int bday;
            string v = driver.FindElement(By.CssSelector
                ("select[name=bday] option[selected=selected]")).GetAttribute("value");
            if (v != "0") { bday = int.Parse(v);}
            else { bday = 0; }
            int bmonth;
            v = driver.FindElement(By.CssSelector
                ("select[name=bmonth] option[selected=selected]")).Text;
            if (v != "-") { bmonth = GetSelectedMonth(v); }
            else { bmonth = 0; }
            string byear = driver.FindElement(By.Name("byear")).GetAttribute("value");

            int aday;
            v = driver.FindElement(By.CssSelector
                ("select[name=aday] option[selected=selected]")).GetAttribute("value");
            if (v != "0") { aday = int.Parse(v); }
            else { aday = 0; }
            int amonth;
            v = driver.FindElement(By.CssSelector
                ("select[name=amonth] option[selected=selected]")).Text;
            if (v != "-") { amonth = GetSelectedMonth(v); }
            else { amonth = 0; }

            string ayear = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            int group = 0;
            string secondaddress = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string secondhome = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return new ContactData()
            {
                Firstname = firstname,
                Lastname = lastname,
                Middlename = middlename,
                Nickname = nickname,
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Homepage = homepage,
                Title = title,
                Company = company,
                Homephone = homephone,
                Mobilephone = mobilephone,
                Workphone = workphone,
                Fax = faxphone,
                Bday = bday,
                Bmonth = bmonth,
                Byear = byear,
                Aday = aday,
                Amonth = amonth,
                Ayear = ayear,
                Group = group,
                Secondaddress = secondaddress,
                Secondhome = secondhome,
                Notes = notes
            };
        }

        private int GetSelectedMonth(string v)
        {
            if (v == "January") return 1;
            if (v == "February") return 2;
            if (v == "March") return 3;
            if (v == "April") return 4;
            if (v == "May") return 5;
            if (v == "June") return 6;
            if (v == "July") return 7;
            if (v == "August") return 8;
            if (v == "September") return 9;
            if (v == "October") return 10;
            if (v == "November") return 11;
            if (v == "December") return 12;
            return 0;
        }

        public bool IsThereAnyContact()
        {
            manager.Navigator.GoToHomePage();
            return IsElementPresent(By.CssSelector("tr[name=entry]"));
        }
        private List<ContactData> contactcache = null;

        public List<ContactData> GetContactList()
        {
            if (contactcache == null)
            {
                contactcache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements
                    (By.CssSelector("tr[name=entry]"));
                foreach (IWebElement element in elements)
                {
                    ContactData contact = new ContactData();
                    contact.Firstname = element.FindElement(By.XPath("td[3]")).Text;
                    contact.Lastname = element.FindElement(By.XPath("td[2]")).Text;
                    contactcache.Add(contact);
                }
            }
            return new List<ContactData>(contactcache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name=entry]")).Count;
        }

        public ContactHelper RemoveContact(int index)
        {
            SelectContact(index);
            InitContactRemoval();
            SubmitContactRemoval();
            if (IsElementPresent(By.CssSelector("tr[name=entry]")))
            {
                wait.Until(ExpectedConditions.
                            StalenessOf(driver.FindElement(By.CssSelector("tr[name=entry]"))));
            }
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper SubmitContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            contactcache = null;
            return this;
        }

        public ContactHelper InitContactRemoval()
        {
            driver.FindElement(By.CssSelector("input[value=Delete]")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            wait.Until(ExpectedConditions.ElementIsVisible
                (By.XPath(".//*[@id='maintable']/tbody/tr[" + (index + 1) + "]")));
            driver.FindElement(By.XPath(".//*[@id='maintable']/tbody/tr[" + (index + 2) + "]"))
                .FindElement(By.XPath(".//input[@name='selected[]']")).Click();
            return this;
        }

        public ContactHelper CreateContact(ContactData newcontact)
        {

            InitCreationNewContact();
            FillNewContactForm(newcontact);
            SubmitNewContactCreation();
            manager.Navigator.ReturnToHomePage();

            return this;
        }

        public ContactHelper ModifyContact(int index, ContactData newcontact)
        {
            InitContactModification(index);
            FillNewContactForm(newcontact);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactcache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            wait.Until(ExpectedConditions.ElementIsVisible
                (By.XPath(".//*[@id='maintable']/tbody/tr[" + (index + 1) +"]")));
            driver.FindElement(By.XPath(".//*[@id='maintable']/tbody/tr[" + (index + 2) + "]"))
                .FindElement(By.XPath(".//*[@title='Edit']")).Click();
            return this;
        }

        public ContactHelper InitContactInformation(int index)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("entry")));
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6].Click();
            return this;
        }

        public ContactHelper FillNewContactForm(ContactData contact)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("firstname")));
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Homephone);
            Type(By.Name("mobile"), contact.Mobilephone);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            if (contact.Bday != 0) new SelectElement(driver.FindElement(By.Name("bday")))
                    .SelectByIndex(contact.Bday + 1);

            if (contact.Bmonth != 0)
            {
                if (IsElementPresent(By.CssSelector("select[name=bmonth] option[selected=selected]")))
                {
                    new SelectElement(driver.FindElement(By.Name("bmonth")))
                    .SelectByIndex(contact.Bmonth + 1);
                }
                else
                {
                    new SelectElement(driver.FindElement(By.Name("bmonth")))
                    .SelectByIndex(contact.Bmonth);
                }
            }

            Type(By.Name("byear"), contact.Byear);
            if (contact.Aday != 0) new SelectElement(driver.FindElement(By.Name("aday"))).SelectByIndex(contact.Aday + 1);

            if (contact.Amonth != 0)
            {
                if (IsElementPresent(By.CssSelector("select[name=amonth] option[selected=selected]")))
                {
                    new SelectElement(driver.FindElement(By.Name("amonth")))
                    .SelectByIndex(contact.Amonth + 1);
                }
                else
                {
                    new SelectElement(driver.FindElement(By.Name("amonth")))
                    .SelectByIndex(contact.Amonth);
                }
            }

            Type(By.Name("ayear"), contact.Ayear);
            if ((contact.Group != 0) && (IsElementPresent(By.Name("new_group"))))
            {
                new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByIndex(contact.Group);
            }
            Type(By.Name("address2"), contact.Secondaddress);
            Type(By.Name("phone2"), contact.Secondhome);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }

        public ContactHelper SubmitNewContactCreation()
        {
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            contactcache = null;
            return this;
        }

        public ContactHelper InitCreationNewContact()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("add new"))).Click();
            return this;
        }

    }
}
