using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSimon
{
    internal class ParkingSpace
    {
        public double Space { get; set; } = 1;
        public List<Vehicle> VehicleList { get; set; } = new List<Vehicle>();
    }
}
