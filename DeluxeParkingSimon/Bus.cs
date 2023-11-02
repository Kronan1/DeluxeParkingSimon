using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSimon
{
    internal class Bus : Vehicle
    {
        public int Passangers { get; set; }
        public Bus(string colorOfVehicle, int passangers)
        {
            ColorOfVehicle = colorOfVehicle;
            Passangers = passangers;
        }
    }
}
