using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class LoginTestBase : TestBase
    {
        [SetUp]
        public void SetupLogin()
        {
            app.Loginhelper.Login(new LoginData("admin", "secret"));

        }

    }
}
