using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class LoginData
    {
        private string username;
        private string password;

        public LoginData(string username, string password)
            {
            this.username = username;
            this.password = password;
            }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
    }
}
