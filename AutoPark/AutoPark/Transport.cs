using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPark
{
    class Transport : ICalculatePrice
    {
        string naming;
        int registerNumberForPark; //номер регистрации в парке
        double mass;
        double whidth;
        DateTime timeOfRegistrForPark; //время регистрации транспорта в парке
        DateTime stayTime; //время прибывания(до какого включительно)
        public string Naming { get { return naming; } }
        public int RegisterNumberForPark { get { return registerNumberForPark; } }
        public double Mass { get { return mass; } }
        public double Whidth { get { return whidth; } }
        public DateTime TimeOfRegistrforPark { get { return timeOfRegistrForPark; } }
        public DateTime StayTime { get { return stayTime; } }
        void ICalculatePrice.Calculate()
        {
            double Count1 = TimeOfRegistrforPark.Year * 8760 + TimeOfRegistrforPark.Month * 720 + TimeOfRegistrforPark.Day * 24 + TimeOfRegistrforPark.Hour + TimeOfRegistrforPark.Minute * 0.017 + TimeOfRegistrforPark.Second * 0.00028;
            double Count2 = StayTime.Year * 8760 + StayTime.Month * 720 + StayTime.Day * 24 + StayTime.Hour + StayTime.Minute * 0.017 + StayTime.Second * 0.00028;
            double billForHour = 2;
            double hours = Count2 - Count1;
            double result = billForHour * hours;
            Console.WriteLine($"\nСовокупное количество часов стоянки машины {Naming} = {hours} ч.");
            Console.WriteLine($"Сумма стоянки для машины = {result} BYN");
        }
        public virtual string InfoString()
        {
            string inf = $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::\n---Транспорт---\nНаименование: {naming}" +
                         $"\nномер регистрации в парке: {registerNumberForPark}" +
                         $"\nвремя регистрации в парке: {timeOfRegistrForPark}" +
                         $"\nВремя пребывания(до): {stayTime}" +
                         $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::";

            return inf;
        }
        public Transport(string naming, int registerNumberForPark, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime)
        {
            this.naming = naming;
            this.registerNumberForPark = registerNumberForPark;
            this.mass = mass;
            this.whidth = whidth;
            this.timeOfRegistrForPark = timeOfRegistrForPark;
            this.stayTime = stayTime;
        }
    }
}
