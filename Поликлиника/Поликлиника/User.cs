using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Поликлиника
{

    public class User
    {

        private string login;
        private string status;

        public string Login
        {
            get { return login; }
        }
        public string Status
        {
            get { return status; }
        }

        public User(string login, string role)
        {
            this.login = login.Trim();
            this.status = role.Trim();
        }
    }
}
