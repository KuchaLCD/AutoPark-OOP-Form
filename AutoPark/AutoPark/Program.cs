using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPark
{
    class Program
    {
        static void Main(string[] args)
        {
            //можно сгенерировать случайные числа для номера регитрации транспорта
            Random n = new Random();
            int random1 = n.Next(100000, 999999);
            int random2 = n.Next(100000, 999999);

            DateTime forPark1 = new DateTime(2022, 10, 12, 9, 0, 0);
            DateTime stayT1 = new DateTime(2022, 10, 13, 15, 0, 0);
            //номер регистрации также можно ввести самому
            Transport machine = new Transport("BebraMachine", random1, 2000, 130, forPark1, stayT1);
            Console.WriteLine(machine.InfoString());
            Transport car1 = new Car(5000, 215, "1923 AD-2", "Tesla", random2, 2000, 130, new DateTime(2022, 10, 12, 12, 0, 0), new DateTime(2022, 10, 19, 12, 0, 0));
            Console.WriteLine(car1.InfoString());
            Transport car2 = new Car(6000, 235, "9823 EI-2", "Ford", 254616, 1600, 130, new DateTime(2022, 10, 12, 12, 0, 0), new DateTime(2022, 10, 12, 13, 0, 0));
            //exp
            List<Transport> gg = new List<Transport> { car1, car2 };
            Park park = new Park("LuxuryPark", gg);
            park.About();
            gg.Add(machine);
            park.About();
            //string rentConv = Convert.ToString(machine.StayTime.Subtract(machine.TimeOfRegistrforPark));
            ((ICalculatePrice)car1).Calculate();
            ((ICalculatePrice)car2).Calculate();
            Console.ReadLine();
        }
    }
}
