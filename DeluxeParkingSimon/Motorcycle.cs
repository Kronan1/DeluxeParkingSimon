using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSimon
{
    internal class Motorcycle : Vehicle
    {
        public string Brand { get; set; }
        public Motorcycle(string colorOfVehicle, string brand)
        {
            ColorOfVehicle = colorOfVehicle;
            Brand = brand;
        }
    }
}
