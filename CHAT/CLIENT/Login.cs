using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENT
{
    public partial class Login : Form
    {
        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }

            public User(string username, string password)
            {
                Username = username;
                Password = password;
            }
        }

        List<User> users = new List<User>();


        public Login()
        {
            InitializeComponent();
            users = new List<User>();

            users.Add(new User("kha", "123"));
            users.Add(new User("trong", "123"));
            users.Add(new User("truc", "123"));
            users.Add(new User("thinh", "123"));
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        public static bool CheckLogin(List<User> userList, string enteredUsername, string enteredPassword)
        {
            foreach (User user in userList)
            {
                if (user.Username == enteredUsername && user.Password == enteredPassword)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            bool check = CheckLogin(users, txtUserName.Text, txtPassword.Text);
            if (check)
            {
                frmCLIENT frmClient = new frmCLIENT(txtUserName.Text);
                frmClient.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Username or password, wrong!");
            }
        }
    }
}
