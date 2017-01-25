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
    public class NavigationHelper : HelperBase
    {
        string baseURL = "http://localhost/addressbook/";
 
        public NavigationHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void GoToHomePage() {

            if (driver.Url == baseURL && 
                IsElementPresent(By.CssSelector("input[value = 'Send e-Mail']")))
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL);
        }

        public NavigationHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public NavigationHelper ReturnToGroupPageAfterRemovalGroup()
        {
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".msgbox>i>a"))).Click();
            return this;
        }

        public void GoToGroupPage()
        {
            if (driver.Url == baseURL + "group.php" &&
                IsElementPresent(By.Name("new")))
            {
                return;
            }
                driver.FindElement(By.LinkText("groups")).Click();
        }

        public NavigationHelper ReturnToHomePageAfterNewContactCreation()
        {
            wait.Until(ExpectedConditions.ElementExists(By.LinkText("home page"))).Click();
            return this;
        }

        public NavigationHelper ReturnToGroupPageAfterCreationGroup()
        {
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".msgbox>i>a"))).Click();
            return this;
        }

    }
}
