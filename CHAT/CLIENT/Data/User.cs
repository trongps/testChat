using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIENT.Data
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string IP { get; set; }

        public User(string username, string password, string fullName, string iP)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            IP = iP;
        }
    }
}
