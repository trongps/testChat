using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIENT.Models
{
    public class UserMessage
    {
        #region Properties
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region Constructor
        public UserMessage(string sendername, string content, DateTime time)
        {
            this.SenderName = sendername;
            this.Content = content;
            this.TimeStamp = time;
        }
        #endregion
    }
}
