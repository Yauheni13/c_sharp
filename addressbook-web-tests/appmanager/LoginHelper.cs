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
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(LoginData logindata)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(logindata))
                {
                    return;
                }
                LogOut();
            }
            Type(By.Name("user"), logindata.Username);
            Type(By.Name("pass"), logindata.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            //wait.Until(ExpectedConditions.ElementExists(By.CssSelector("input[value='Send e-Mail']")));

        }

        public bool IsLoggedIn(LoginData logindata)
        {
            return IsLoggedIn()
                && GetLoggetUser() == logindata.Username;
        }

        public string GetLoggetUser()
        {
            string text = driver.FindElement(By.XPath(".//form[@name='logout']/b")).Text;
            return text.Substring(1, text.Length - 2);
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.LinkText("Logout"));
        }

        public LoginHelper LogOut()
        {
            if (IsElementPresent(By.LinkText("Logout")))
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
            return this;
        }


    }
}
