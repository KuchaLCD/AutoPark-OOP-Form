using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPark
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
        public virtual void About()
        {
            Console.WriteLine($"\n++++++++++++++++++++++++++++++++++++++++++++++++++\n---Парк---\nНаименование: {name}");
            Console.WriteLine("Содержание парка:");
            if (ListTransport.Count == 0)
            {
                Console.WriteLine("Парк пуст");
            }
            for (int i = 0; i < ListTransport.Count; i++)
            {
                Console.WriteLine(ListTransport[i].InfoString());
            }
            Console.WriteLine($"\n++++++++++++++++++++++++++++++++++++++++++++++++++");
        }
        public Park(string name, List<Transport> transport)
        {
            this.name = name;
            this.transport = transport;
        }
    }
}
