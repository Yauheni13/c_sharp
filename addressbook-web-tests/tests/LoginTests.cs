using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            app.Loginhelper.LogOut();
            LoginData user = new LoginData("admin", "secret");
            app.Loginhelper.Login(user);
            Assert.IsTrue(app.Loginhelper.IsLoggedIn());
            Assert.IsTrue(app.Loginhelper.IsLoggedIn(user));

        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            app.Loginhelper.LogOut();
            LoginData user = new LoginData("admin", "123456");
            app.Loginhelper.Login(user);
            Assert.IsFalse(app.Loginhelper.IsLoggedIn());
        }

    }
}
