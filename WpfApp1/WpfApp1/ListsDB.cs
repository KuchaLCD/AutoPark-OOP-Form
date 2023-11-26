using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ListsDB
    {
        //Application consisting
        public static List<Transport> transports = new List<Transport> { };
        public static List<Customer> customers = new List<Customer> { };
        public static List<UserDB> users = new List<UserDB> { };
        public static List<Order> orders = new List<Order> { };
        //UDP-Chat consisting
        public static List<UserDB> members = new List<UserDB> { };
        public static List<ChatGroupMes> chart = new List<ChatGroupMes> { };
    }
}
