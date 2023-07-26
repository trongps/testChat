using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIENT.Data
{
    public class User
    {
        #region Properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string IP { get; set; }
        public bool Status { get; set; }
        #endregion

        #region Constructor
        public User(string username, string password, string fullName, string iP, bool status)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            IP = iP;
            Status = status;
        } 
        #endregion
    }
}
