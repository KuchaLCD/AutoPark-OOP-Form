using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Customer
    {
        string id;
        string firstName;
        string sureName;
        string lastName;
        string position;

        public string ID
        {
            get { return id; }
        }

        public string FirstName
        {
            get { return firstName; }
        }

        public string SureName
        {
            get { return sureName; }
        }

        public string LastName
        {
            get { return lastName; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        // Конструктор
        public Customer(string id, string firstName, string sureName, string lastName, string pos)
        {
            this.id = id;
            this.firstName = firstName;
            this.sureName = sureName;
            this.lastName = lastName;
            this.position = pos;
        }

        // Переопределение метода ToString()
        override public string ToString()
        {
            // формирование строки с помощью метода Format() класса string
            string st = string.Format("{0}{1}{2}{3}{4}",
                id.ToString().PadRight(4),
                firstName.PadRight(10),
                sureName.ToString().PadRight(12),
                lastName.ToString().PadRight(14),
                position.ToString().PadRight(4));
            return st;
        }
    }
}
