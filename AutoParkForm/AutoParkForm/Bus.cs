using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParkForm
{
    class Bus : EngineTransport
    {
        double CountOfWheels { get; set; }
        public Bus(double volumeOfEngine, double maxSpeed, string roadNumber, string naming, int registerNumberForPark, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime, string picture, string notes, double countOfWhells)
            : base(volumeOfEngine, maxSpeed, roadNumber, naming, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes)
        {
            this.CountOfWheels = countOfWhells;
        }
        public override string InfoString()
        {
            string inf = $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::\n---Автобус---\nНаименование: {Naming}" +
                         $"\nНомер регистрации в парке: {RegisterNumberForPark}" +
                         $"\nВремя регистрации в парке: {TimeOfRegistrForPark}" +
                         $"\nНомера: {RoadNumber}" +
                         $"\n---Дополнительная информация---\nОбъём двигателя: {VolumeOfEngine} см. куб." +
                         $"\nМаксимальная скорость: {MaxSpeed} км/ч" +
                         $"\nШирина: {Whidth} м." +
                         $"\nВес: {Mass} кг." +
                         $"\nКоличество колёс: {CountOfWheels}" +
                         $"\nВремя пребывания(до): {StayTime}" +
                         $"\nПримечания: {Notes}" +
                         $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::";

            return inf;
        }
    }
}
