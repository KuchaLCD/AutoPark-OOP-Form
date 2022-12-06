using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParkForm
{
    public class Transport
    {
        public string Naming { get; }
        public int RegisterNumberForPark { get; }
        public double Mass { get; }
        public double Whidth { get; }
        public static DateTime TimeOfRegistrForPark { get; set; }
        public static DateTime StayTime { get; set; }
        public string Picture { get; }
        public string Notes { get; }
        public virtual string InfoString()
        {
            string inf = $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::\n---Транспорт---\nНаименование: {Naming}" +
                         $"\nНомер регистрации в парке: {RegisterNumberForPark}" +
                         $"\nВремя регистрации в парке: {TimeOfRegistrForPark}" +
                         $"\nВремя пребывания(до): {StayTime}" +
                         $"\nМасса: {Mass} кг." +
                         $"\nШирина: {Whidth} м." +
                         $"\nПримечания: {Notes}" +
                         $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::";

            return inf;
        }
        public string Calculate()
        {
            double Count1 = TimeOfRegistrForPark.Year * 8760 + TimeOfRegistrForPark.Month * 720 + TimeOfRegistrForPark.Day * 24 + TimeOfRegistrForPark.Hour + TimeOfRegistrForPark.Minute * 0.017 + TimeOfRegistrForPark.Second * 0.00028;
            double Count2 = StayTime.Year * 8760 + StayTime.Month * 720 + StayTime.Day * 24 + StayTime.Hour + StayTime.Minute * 0.017 + StayTime.Second * 0.00028;
            double billForHour = 2;
            double hours = Count2 - Count1;
            double result = billForHour * hours;
            string inf = $"\nСовокупное количество часов стоянки для выбранного транспорта = {hours} ч." +
                         $"\nСумма стоянки = {result} BYN";
            return inf;
        }
        public override string ToString()
        {
            string st = string.Format("Транспорт {0}", Naming);
            return st;
        }
        public Transport(string naming, int registerNumberForPark, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime, string picture, string notes)
        {
            this.Naming = naming;
            this.RegisterNumberForPark = registerNumberForPark;
            this.Mass = mass;
            this.Whidth = whidth;
            TimeOfRegistrForPark = timeOfRegistrForPark;
            StayTime = stayTime;
            this.Picture = picture;
            this.Notes = notes;
        }
    }
}