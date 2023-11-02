using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSimon
{
    internal class Vehicle
    {
        public string Registration { get; set; } = CreateRegistration();
        public string ColorOfVehicle { get; set; } = "";
        public DateTime parkingStarted { get; set; } = DateTime.Now;


        private static string CreateRegistration()
        {
            string registration = "";
            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                char newChar = (char)random.Next(65, 91);
                registration += newChar.ToString();
            }

            for (int i = 0; i < 3; i++)
            {
                registration += random.Next(0, 10).ToString();
            }

            return registration;
        }
    }
}
