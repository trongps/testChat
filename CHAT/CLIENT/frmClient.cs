using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using CLIENT.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using CLIENT.Models;

namespace CLIENT
{
    public partial class frmCLIENT : Form
    {
        private string ServerIP;
        private User User;
        public frmCLIENT(User User)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;//tránh việc đụng độ khi sử dụng tài nguyên giữa các thread
            this.ServerIP = GetLocalIPAddress(); //192.168.51.220
            this.User = User;
            Connect();
        }

        private void frmCLIENT_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        IPEndPoint IP;
        Socket client;

        //kết nối đến server
        //void Connect()
        //{
        //    //IP là địa chỉ của server.Khởi tạo địa chỉ IP và socket để kết nối
        //    IP = new IPEndPoint(IPAddress.Parse("10.86.7.67"), 1997);
        //    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    //bắt đầu kết nôi. Nếu ko kết nối được thì hiện thông báo
        //    try
        //    {
        //        client.Connect(IP);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Lỗi kết nối", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    //tạo luồng lắng nghe server khi vừa kết nối tới
        //    Thread listen = new Thread(Receive);
        //    listen.IsBackground = true;
        //    listen.Start();
        //}

        private void Connect()
        {
            // Khởi tạo địa chỉ IP và socket để kết nối tới máy chủ
            IP = new IPEndPoint(IPAddress.Parse(GetLocalIPAddress()), 1997);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            // Bắt đầu kết nối. Nếu không kết nối được, hiện thông báo lỗi
            try
            {
                client.Connect(IP);
                MessageBox.Show("Successfully connected to the server.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tạo luồng lắng nghe server khi vừa kết nối thành công
                Thread listenThread = new Thread(Receive);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (SocketException ex)
            {
                // Nếu có lỗi kết nối, hiện thông báo lỗi cụ thể
                MessageBox.Show($"Connection Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //đóng kết nối đến server
        void Close()
        {
            client.Close();
        }

        //gửi dữ liệu
        void Send()
        {
            //nếu textboc khác rỗng thì mới gửi tin
            if (txbMessage.Text != string.Empty && IsServerOnline())
            {  
                client.Send(Serialize(new UserMessage(User.Username, txbMessage.Text, DateTime.Now)));
            }
            else
            {
                MessageBox.Show($"Connection Error: Server is offline!, Exit now!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        //check status server
        private bool IsServerOnline()
        {
            try
            {
                // IP và cổng của server
                string serverIP = GetLocalIPAddress(); // Thay bằng địa chỉ IP của server
                int serverPort = 1997; // Thay bằng cổng của server

                // Tạo socket để kết nối tới server
                using (Socket checkSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPAddress ipAddress = IPAddress.Parse(serverIP);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, serverPort);

                    // Thử kết nối tới server
                    checkSocket.Connect(ipEndPoint);

                    // Nếu kết nối thành công, server đang hoạt động
                    return true;
                }
            }
            catch (SocketException)
            {
                // Nếu có lỗi khi kết nối, server không hoạt động
                return false;
            }
        }

        //nhận dữ liệu
        void Receive()
        {
            try
            {
                while (true)
                {
                    //khai báo mảng byte để nhận dữ liệu dưới mảng byte
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);
                    //chuyển data từ dạng byte sang dạng string
                    string content = (string)Deseriliaze(data);
                    UserMessage message = new UserMessage(User.Username, content, DateTime.Now);
                    AddMessage(message);
                }
            }
            catch
            {
                Close();
            }
        }

        //add mesage vào khung chat
        void AddMessage(UserMessage msg)
        {
            lsvMessage.Items.Add(new ListViewItem() { Text = msg.Content });
            txbMessage.Clear();
        }

        //Hàm phân mảnh dữ liệu cần gửi từ dạng string sang dạng byte để gửi đi
        byte[] Serialize(UserMessage obj)
        {
            //khởi tạo stream để lưu các byte phân mảnh
            MemoryStream stream = new MemoryStream();
            //khởi tạo đối tượng BinaryFormatter để phân mảnh dữ liệu sang kiểu byte
            BinaryFormatter formatter = new BinaryFormatter();
            //phân mảnh rồi ghi vào stream
            formatter.Serialize(stream, obj.SenderName +":"+ obj.Content);
            //từ stream chuyển các các byte thành dãy rồi cbi gửi đi
            return stream.ToArray();
        }

        //Hàm gom mảnh các byte nhận được rồi chuyển sang kiểu string để hiện thị lên màn hình
        object Deseriliaze(byte[] data)
        {
            //khởi tạo stream đọc kết quả của quá trình phân mảnh 
            MemoryStream stream = new MemoryStream(data);
            //khởi tạo đối tượng chuyển đổi
            BinaryFormatter formatter = new BinaryFormatter();
            //chuyển đổi dữ liệu và lưu lại kết quả 
            return formatter.Deserialize(stream);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send();
            AddMessage(new UserMessage(User.Username, txbMessage.Text, DateTime.Now));
        }

        private void frmCLIENT_Load(object sender, EventArgs e)
        {
            txtNameUser.Text = User.Username;
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
