using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENT.Data
{
    public class Room
    {
        #region Properties
        public int RoomID { get; set; }
        public string Type { get; set; }
        public List<User> Users { get; set; }
        List<Message> Messages { get; set; }
        #endregion

        #region Constructor
        public Room(int roomID, string type, List<User> users, List<Message> messages)
        {
            this.RoomID = roomID;
            this.Type = type;
            this.Users = users;
            this.Messages = messages;
        } 
        #endregion
    }
}
