using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParkForm
{
    class Park
    {
        string name;
        List<Transport> transport;
        public string Name { get { return name; } }
        public List<Transport> ListTransport { get { return transport; } }
        public void AddTrasport(int position, Transport t)
        {
            ListTransport.Insert(position, t);
        }
        public void RemoveTransport(Transport t)
        {
            ListTransport.Remove(t);
        }
        public virtual string About()
        {
            string inf = $"\n++++++++++++++++++++++++++++++++++++++++++++++++++\n---Парк---\nНаименование: {name}" +
            "\nСодержание парка:"; 
            if (ListTransport.Count == 0)
            {
                inf += "Парк пуст";
            }
            for (int i = 0; i < ListTransport.Count; i++)
            {
                inf += ListTransport[i].InfoString();
            }
            inf += $"\n++++++++++++++++++++++++++++++++++++++++++++++++++";
            return inf;
        }
        public Park(string name, List<Transport> transport)
        {
            this.name = name;
            this.transport = transport;
        }
    }
}
