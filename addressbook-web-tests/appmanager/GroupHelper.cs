using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        
        public GroupHelper(ApplicationManager manager) : base(manager)
            {
            }

        private List<GroupData> groupscache = null;

        public List<GroupData> GetGroupList()
        {
            if (groupscache == null)
            {
                groupscache = new List<GroupData>();
                manager.Navigator.GoToGroupPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupscache.Add(new GroupData()
                    {
                        Id = element.FindElement(By.TagName("input")).
                        GetAttribute("value")});
                }
                string allgroups = driver.FindElement(By.CssSelector("div#content form"))
                                        .Text;
                string[] parts = allgroups.Split('\n');
                int shift = groupscache.Count - parts.Length;
                for (int i=0; i<groupscache.Count; i++)
                {
                    if (i < shift)
                    {
                        groupscache[i].Groupname = "";
                    }else
                    {
                        groupscache[i].Groupname = parts[i - shift].Trim();
                    }
                }
            }
            return new List<GroupData>(groupscache);
        }

        public int GetGroupCount()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span.group")));
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public GroupHelper CreateGroup(GroupData group)
        {
            manager.Navigator.GoToGroupPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            manager.Navigator.GoToGroupPage();

            return this;
        }

        public GroupHelper Modify(int index, GroupData group)
        {
            manager.Navigator.GoToGroupPage();
            SelectGroup(index);
            InitGroupModification();
            FillGroupForm(group);
            SubmitGroupModification();
            manager.Navigator.GoToGroupPage();
            
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupscache = null;
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("edit"))).Click();
            return this;
        }

        public GroupHelper Remove(int index)
        {
            manager.Navigator.GoToGroupPage();
            SelectGroup(index);
            RemoveGroup();
            manager.Navigator.GoToGroupPage();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupscache = null;
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            wait.Until(ExpectedConditions.ElementIsVisible
                (By.XPath(".//*[@id='content']/form/span[" + (index + 1) + "]/input")))
                .Click();
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupscache = null;
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("group_name")));
            Type(By.Name("group_name"), group.Groupname);
            Type(By.Name("group_header"), group.Groupheader);
            Type(By.Name("group_footer"), group.Groupfooter);
            return this;
        }


        public GroupHelper InitGroupCreation()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("new"))).Click();
            return this;
        }

        public bool IsThereAnyGroup()
        {
            manager.Navigator.GoToGroupPage();
            return IsElementPresent(By.XPath(".//*[@id='content']/form/span/input"));
        }
    }
}
