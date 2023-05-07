using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Order
    {
        public int IDOrd { get; }
        public string IDCustomer { get; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
        public int IDTransp { get; }
        public double Bill { get; set; }
        public override string ToString()
        {
            string st = string.Format("Заказ № {0}, ИН Заказчика: {1}", IDOrd, IDCustomer);
            return st;
        }
        public double CalculateBill(DateTime startRent, DateTime endRent)
        {
            double billForHour = 5;
            double Count1 = 0;      //промежуточное значение
            double Count2 = 0;      //и это тоже
            //Этот "сложный" алгоритм считает общее количество часов 
            Count1 += startRent.Year * 8760 + startRent.Month * 720 + startRent.Day * 24 + startRent.Hour + startRent.Minute * 0.017 + startRent.Second * 0.00028;
            Count2 += endRent.Year * 8760 + endRent.Month * 720 + endRent.Day * 24 + endRent.Hour + endRent.Minute * 0.017 + endRent.Second * 0.00028;
            //Считаем часы и результат ("дата пребытия в часах" - "дата отъезда")
            double hours = Count2 - Count1;
            double result = billForHour * hours;
            return result;
        }
        public string InfoString()
        {
            string inf = $"\n:::::::::::::*************************::::::::::::::::\n$---Заказ---$\nНомер: {IDOrd}" +
                         $"\nИН Заказчика: {IDCustomer}" +
                         $"\nНачало аренды: {StartRent}" +
                         $"\nОкончание аренды: {EndRent}" +
                         $"\nИН Транспорта: {IDTransp}" +
                         $"\nИтог к оплате: {Bill}" +
                         $"\n:::::::::::::*************************::::::::::::::::";

            return inf;
        }

        public Order(int idOrd, string idCust, DateTime startRent, DateTime endRent, int idTr, double bill)
        {
            this.IDOrd = idOrd;
            this.IDCustomer = idCust;
            this.StartRent = startRent;
            this.EndRent = endRent;
            this.IDTransp = idTr;
            this.Bill = bill;
        }
    }
}
