using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Positions
    {
        int idPos;
        string name;
        int salary;

        public int ID
        {
            get { return idPos; }
        }

        public string Name
        {
            get { return name; }
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        // Конструктор
        public Positions(int id, string name, int salary)
        {
            this.idPos = id;
            this.name = name;
            this.salary = salary;
        }

        // Переопределение метода ToString()
        override public string ToString()
        {
            // формирование строки с помощью метода Format() класса string
            string st = string.Format("{0}{1}{2}",
                idPos.ToString().PadRight(4),
                name.PadRight(10),
                salary.ToString().PadRight(4));
            return st;
        }
    }
}
