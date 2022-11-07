﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParkForm
{
    class Car : EngineTransport
    {
        public Car(double volumeOfEngine, double maxSpeed, string roadNumber, string naming, int registerNumberForPark, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime, string picture, string notes)
            : base(volumeOfEngine, maxSpeed, roadNumber, naming, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes)
        { }
        public override string InfoString()
        {
            string inf = $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::\n---Машина(Автомобиль)---\nНаименование: {Naming}" +
                         $"\nНомер регистрации в парке: {RegisterNumberForPark}" +
                         $"\nВремя регистрации в парке: {TimeOfRegistrforPark}" +
                         $"\nНомера: {RoadNumber}" +
                         $"\n---Дополнительная информация---\nОбъём двигателя: {VolumeOfEngine} см. куб." +
                         $"\nМаксимальная скорость: {MaxSpeed} км/ч" +
                         $"\nВес: {Mass} кг." +
                         $"\nВремя пребывания(до): {StayTime}" +
                         $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::";

            return inf;
        }
    }
}
