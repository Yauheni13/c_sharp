using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Threading;

namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        protected bool acceptNextAlert = true;
        protected WebDriverWait wait;

        protected LoginHelper loginhelper;
        protected NavigationHelper navigationhelper;
        protected GroupHelper grouphelper;
        protected ContactHelper contacthelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            FirefoxBinary binary = new FirefoxBinary(@"c:\Program Files\Mozilla Firefox\firefox.exe");
            driver = new FirefoxDriver(binary, new FirefoxProfile());
            //driver = new FirefoxDriver();
            verificationErrors = new StringBuilder();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));

            loginhelper = new LoginHelper(this);
            navigationhelper = new NavigationHelper(this);
            grouphelper = new GroupHelper(this);
            contacthelper = new ContactHelper(this);

        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if(! app.IsValueCreated)
            {
                ApplicationManager newInstanse = new ApplicationManager();
                newInstanse.Navigator.GoToHomePage();
                app.Value = newInstanse;

            }
            return app.Value;

        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public WebDriverWait Wait
        {
            get
            {
                return wait;
            }
        }

        public LoginHelper Loginhelper
        {
            get {return loginhelper;}
        }

        public NavigationHelper Navigator
        {
            get { return navigationhelper; }
        }

        public ContactHelper Contacthelper
        {
            get { return contacthelper; }
        }

        public GroupHelper Grouphelper
        {
            get { return grouphelper; }
        }

    }
}
