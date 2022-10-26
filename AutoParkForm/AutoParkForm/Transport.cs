using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParkForm
{
    class Transport
    {
        string naming;
        int registerNumberForPark; //номер регистрации в парке
        double mass;
        double whidth;
        DateTime timeOfRegistrForPark; //время регистрации транспорта в парке
        DateTime stayTime; //время прибывания(до какого включительно)
        string picture; //изображение транспорта(путь к файлу с изображением)
        string notes; //примечания
        public string Naming { get { return naming; } }
        public int RegisterNumberForPark { get { return registerNumberForPark; } }
        public double Mass { get { return mass; } }
        public double Whidth { get { return whidth; } }
        public DateTime TimeOfRegistrforPark { get { return timeOfRegistrForPark; } }
        public DateTime StayTime { get { return stayTime; } }
        public string Picture { get { return picture; } }
        public string Notes { get { return notes; } }
        public virtual string InfoString()
        {
            string inf = $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::\n---Транспорт---\nНаименование: {naming}" +
                         $"\nномер регистрации в парке: {registerNumberForPark}" +
                         $"\nвремя регистрации в парке: {timeOfRegistrForPark}" +
                         $"\nВремя пребывания(до): {stayTime}" +
                         $"\nПримечания: {notes}" +
                         $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::";

            return inf;
        }
        public Transport(string naming, int registerNumberForPark, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime, string picture, string notes)
        {
            this.naming = naming;
            this.registerNumberForPark = registerNumberForPark;
            this.mass = mass;
            this.whidth = whidth;
            this.timeOfRegistrForPark = timeOfRegistrForPark;
            this.stayTime = stayTime;
            this.picture = picture;
            this.notes = notes;
        }
    }
}