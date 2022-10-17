using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPark
{
    class Car : EngineTransport
    {
        public Car(double volumeOfEngine, double maxSpeed, string roadNumber, string naming, int registerNumberForPark, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime)
            : base(volumeOfEngine, maxSpeed, roadNumber, naming, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime)
        { }
        public override string InfoString()
        {
            string inf = $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::\n---Машина(Автомобиль)---\nНаименование: {Naming}" +
                         $"\nномер регистрации в парке: {RegisterNumberForPark}" +
                         $"\nвремя регистрации в парке: {TimeOfRegistrforPark}" +
                         $"\nномера: {RoadNumber}" +
                         $"\n---Дополнительная информация---\nОбъём двигателя: {VolumeOfEngine} см. куб." +
                         $"\nМаксимальная скорость: {MaxSpeed} км/ч" +
                         $"\nВес: {Mass} кг." +
                         $"\nВремя пребывания(до): {StayTime}" +
                         $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::";

            return inf;
        }
    }
}
