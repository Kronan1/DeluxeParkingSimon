using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSimon
{
    internal class Car : Vehicle
    {
        public bool Electric { get; set; }
        public Car(string colorOfVehicle, bool electric)
        {
            ColorOfVehicle = colorOfVehicle;
            Electric = electric;
        }
    }
}
