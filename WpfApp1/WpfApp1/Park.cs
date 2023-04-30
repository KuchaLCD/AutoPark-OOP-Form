using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Park
    {
        public string Name { get; set; }
        public void AddTrasport(Transport t)
        {
            ListsDB.transports.Add(t);
        }
        public void RemoveTransport(Transport t)
        {
            ListsDB.transports.Remove(t);
        }
        public virtual string About()
        {
            string inf = $"\n++++++++++++++++++++++++++++++++++++++++++++++++++\n---Парк---\nНаименование: {Name}" +
            "\nСодержание парка:";
            for (int i = 0; i < ListsDB.transports.Count; i++)
            {
                inf += ListsDB.transports[i].InfoString();
            }
            inf += $"\n++++++++++++++++++++++++++++++++++++++++++++++++++";
            return inf;
        }
        public Park(string name, List<Transport> transport)
        {
            this.Name = name;
            ListsDB.transports = transport;
        }
        public static Park park = new Park("LuxuryPark", ListsDB.transports);
    }
}

