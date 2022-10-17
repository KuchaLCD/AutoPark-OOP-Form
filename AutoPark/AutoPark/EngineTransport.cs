using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPark 
{
    class EngineTransport : Transport
    {
        double maxSpeed; //максимальная скорость (в км/ч)
        double volumeOfEngine;  //объём двигателя (в см. куб.)
        string roadNumber; //дорожные номера. Пример(белорусские): 1231 AD-2
        public string RoadNumber { get { return roadNumber; } }
        public double VolumeOfEngine { get { return volumeOfEngine; } }
        public double MaxSpeed { get { return maxSpeed; } }
        public EngineTransport(double volumeOfEngine,double maxSpeed, string roadNumber, string naming, int registerNumberForPark, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime)
            : base(naming, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime)
        {
            this.roadNumber = roadNumber;
            this.maxSpeed = maxSpeed;
            this.volumeOfEngine = volumeOfEngine;
        }
    }
}
