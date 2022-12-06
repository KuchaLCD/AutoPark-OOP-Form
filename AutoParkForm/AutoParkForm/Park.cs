using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParkForm
{
    class Park
    {
        public string Name { get; set; }
        public void AddTrasport(Transport t)
        {
            ListForTransport.transports.Add(t);
        }
        public void RemoveTransport(Transport t)
        {
            ListForTransport.transports.Remove(t);
        }
        public virtual string About()
        {
            string inf = $"\n++++++++++++++++++++++++++++++++++++++++++++++++++\n---Парк---\nНаименование: {Name}" +
            "\nСодержание парка:"; 
            for (int i = 0; i < ListForTransport.transports.Count; i++)
            {
                inf += ListForTransport.transports[i].InfoString();
            }
            inf += $"\n++++++++++++++++++++++++++++++++++++++++++++++++++";
            return inf;
        }
        public Park(string name, List<Transport> transport)
        {
            this.Name = name;
            ListForTransport.transports = transport;
        }
    }
}
