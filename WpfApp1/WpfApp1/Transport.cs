﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Transport : ICalc
    {
        public int RegisterNumberForPark { get; }
        public string Naming { get; set; }
        public double Mass { get; }
        public double Whidth { get; set; }
        public DateTime TimeOfRegistrForPark { get; set; }
        public DateTime StayTime { get; set; }
        public string Picture { get; set; }
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
        public string CalculateOwn()
        {
            double Count1 = TimeOfRegistrForPark.Year * 8760 + TimeOfRegistrForPark.Month * 720 + TimeOfRegistrForPark.Day * 24 + TimeOfRegistrForPark.Hour + TimeOfRegistrForPark.Minute * 0.017 + TimeOfRegistrForPark.Second * 0.00028;
            double Count2 = StayTime.Year * 8760 + StayTime.Month * 720 + StayTime.Day * 24 + StayTime.Hour + StayTime.Minute * 0.017 + StayTime.Second * 0.00028;
            double billForHour = 2;
            double hours = Count2 - Count1;
            double result = billForHour * hours;
            string inf = $"Совокупное количество часов стоянки для выбранного транспорта = {hours} ч." +
                         $"\nСумма стоянки = {result} BYN";
            return inf;
        }
        public string CalculateIncome()
        {
            double k = 0;       // Число для подсчёта транспорта в парке 
            double billForHour = 2;
            double Count1 = 0;      //промежуточное значение
            double Count2 = 0;      //и это тоже
            //Этот "сложный" алгоритм считает общее количество часов 
            for (int i = 0; i < ListsDB.transports.Count; i++)
            {
                Count1 += ListsDB.transports[i].TimeOfRegistrForPark.Year * 8760 + ListsDB.transports[i].TimeOfRegistrForPark.Month * 720 + ListsDB.transports[i].TimeOfRegistrForPark.Day * 24 + ListsDB.transports[i].TimeOfRegistrForPark.Hour + ListsDB.transports[i].TimeOfRegistrForPark.Minute * 0.017 + ListsDB.transports[i].TimeOfRegistrForPark.Second * 0.00028;
                Count2 += ListsDB.transports[i].StayTime.Year * 8760 + ListsDB.transports[i].StayTime.Month * 720 + ListsDB.transports[i].StayTime.Day * 24 + ListsDB.transports[i].StayTime.Hour + ListsDB.transports[i].StayTime.Minute * 0.017 + ListsDB.transports[i].StayTime.Second * 0.00028;
                k++;
            }
            //Считаем часы и результат ("дата пребытия в часах" - "дата отъезда")
            double hours = Count2 - Count1;
            double result = billForHour * hours;
            string inf = $"Совокупное количество транспорта в парке = {k}" +
                         $"\nПрибыль = {result} BYN";
            return inf;
        }
        public string OurIncome()
        {
            double ourIncome = 0;
            double k = 0;       // Число для подсчёта транспорта в парке 
            double billForHour = 2;
            double Count1 = 0;      //промежуточное значение
            double Count2 = 0;      //и это тоже
            //Этот "сложный" алгоритм считает общее количество часов 
            for (int i = 0; i < ListsDB.transports.Count; i++)
            {
                Count1 += ListsDB.transports[i].TimeOfRegistrForPark.Year * 8760 + ListsDB.transports[i].TimeOfRegistrForPark.Month * 720 + ListsDB.transports[i].TimeOfRegistrForPark.Day * 24 + ListsDB.transports[i].TimeOfRegistrForPark.Hour + ListsDB.transports[i].TimeOfRegistrForPark.Minute * 0.017 + ListsDB.transports[i].TimeOfRegistrForPark.Second * 0.00028;
                Count2 += ListsDB.transports[i].StayTime.Year * 8760 + ListsDB.transports[i].StayTime.Month * 720 + ListsDB.transports[i].StayTime.Day * 24 + ListsDB.transports[i].StayTime.Hour + ListsDB.transports[i].StayTime.Minute * 0.017 + ListsDB.transports[i].StayTime.Second * 0.00028;
                k++;
            }
            //Считаем часы и результат ("дата пребытия в часах" - "дата отъезда")
            double hours = Count2 - Count1;
            double result = billForHour * hours;
            for (int i = 0; i < ListsDB.orders.Count; i++)
            {
                ourIncome += ListsDB.orders[i].Bill;
            }

            return ListsDB.transports[0].CalculateIncome() + $"\nСовокупная прибыль = {ourIncome + result}";
        }
        public override string ToString()
        {
            string st = string.Format("Транспорт {0}, ID {1}", Naming, RegisterNumberForPark);
            return st;
        }
        public Transport(int registerNumberForPark, string naming, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime, string picture, string notes)
        {
            this.RegisterNumberForPark = registerNumberForPark;
            this.Naming = naming;
            this.Mass = mass;
            this.Whidth = whidth;
            TimeOfRegistrForPark = timeOfRegistrForPark;
            StayTime = stayTime;
            this.Picture = picture;
            this.Notes = notes;
        }
        public Transport(string naming, double whidth, string picture)
        {
            this.Naming = naming;
            this.Whidth = whidth;
            this.Picture = picture;
        }
    }
}
