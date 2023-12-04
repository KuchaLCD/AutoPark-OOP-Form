using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ChatGroupMes
    {
        public int idChat { get; set; }
        public string avatarPicture { get; set; }
        public string message { get; set; }
        public string time { get; set; }
        public ChatGroupMes(int id, string avatar, string message)
        {
            this.idChat = id;
            this.avatarPicture = avatar;
            this.message = message;
        }
        public ChatGroupMes(int id, string avatar, string message, string time)
        {
            this.idChat = id;
            this.avatarPicture = avatar;
            this.message = message;
            this.time = time;
        }
    }
}
