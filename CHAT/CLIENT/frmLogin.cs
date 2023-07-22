using CLIENT.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENT
{
    public partial class frmLogin : Form
    {

        List<User> users = new List<User>();


        public frmLogin()
        {
            InitializeComponent();
            users = new List<User>();

            users.Add(new User("trongps", "123", "Phan Sỹ Trọng", GetLocalIPAddress()));
            users.Add(new User("kha", "123", "Cao Văn Kha", GetLocalIPAddress()));
            users.Add(new User("truc", "123", "Ngô Thị Tuyết Trúc", GetLocalIPAddress()));
            users.Add(new User("thinh", "123", "Trần Khánh Thịnh", GetLocalIPAddress()));
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


        private string GetLocalIPAddress()
        {
            string localIP = "?";
            try
            {
                // Lấy địa chỉ IP của client sử dụng Dns.GetHostEntry
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return localIP;
        }
    }
}
