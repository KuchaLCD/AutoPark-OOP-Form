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
        public List<Transport> ListTransport { get; set; }
        public void AddTrasport(Transport t)
        {
            transport.Add(t);
        }
        public void RemoveTransport(Transport t)
        {
            transport.Remove(t);
        }
        public virtual string About()
        {
            string inf = $"\n++++++++++++++++++++++++++++++++++++++++++++++++++\n---Парк---\nНаименование: {name}" +
            "\nСодержание парка:"; 
            if (transport.Count == 0)
            {
                inf += "Парк пуст";
            }
            for (int i = 0; i < transport.Count; i++)
            {
                inf += transport[i].InfoString();
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
