using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSimon
{
    internal class ParkingGarage
    {
        public List<ParkingSpace> ParkingSpaces { get; set; } = new List<ParkingSpace>();

        public ParkingGarage()
        {
            for (int i = 0; i < 15; i++)
            {
                ParkingSpaces.Add(new ParkingSpace());
            }
        }

        public void CreateNewVehicle()
        {
            Random random = new Random();
            double size;
            List<int> index;

            switch (random.Next(3))
            {
                case 0:
                    Car car = new Car(GetStringInput("Color"), GetElectricInput());
                    car.parkingStarted = DateTime.Now;
                    size = 1;
                    index = CheckAvailability(car, size);
                    if (!index.Any()) { break; };
                    ParkingSpaces[index.First()].VehicleList.Add(car);
                    ParkingSpaces[index.First()].Space -= size;
                    break;

                case 1:
                    Bus bus = new Bus(GetStringInput("Color"), GetPassangerInput());
                    bus.parkingStarted = DateTime.Now;
                    size = 1;
                    index = CheckAvailability(bus, size);
                    if (!index.Any()) { break; };
                    ParkingSpaces[index.First()].VehicleList.Add(bus);
                    ParkingSpaces[index.First()].Space -= size;
                    ParkingSpaces[index[1]].VehicleList.Add(bus);
                    ParkingSpaces[index[1]].Space -= size;
                    break;

                case 2:
                    Motorcycle motorcycle = new Motorcycle(GetStringInput("Color"), GetStringInput("Brand"));
                    motorcycle.parkingStarted = DateTime.Now;
                    size = 0.5;
                    index = CheckAvailability(motorcycle, size);
                    if (!index.Any()) { break; };
                    ParkingSpaces[index.First()].VehicleList.Add(motorcycle);
                    ParkingSpaces[index.First()].Space -= size;
                    break;
            }

        }
        private List<int> CheckAvailability(Vehicle vehicle, double size) 
        {
            List<int> index = new();

            if (vehicle is Bus bus)
            {
                for (int i = 0; i < ParkingSpaces.Count - 1; i++)
                {
                    if (ParkingSpaces[i].Space == 1 && ParkingSpaces[i + 1].Space == 1)
                    {
                        index.Add(i);
                        index.Add(i + 1);
                        break;
                    }
                }
            }
            else
            {
                if (ParkingSpaces.Any(x => x.Space >= size))
                {
                    index.Add(ParkingSpaces.FindIndex(x => x.Space >= size));
                }
            }

            if (!index.Any())
            {
                Console.Clear();
                Console.WriteLine("Det finns ingen ledig plats.");
                Thread.Sleep(1000);
            }

            return index;
        }

        private string GetStringInput(string type)
        {
            switch (type)
            {
                case "Color":
                    Console.Write("Färg: ");
                    break;
                case "Brand":
                    Console.Write("Märke: ");
                    break;
                default:
                    return "Error Type";
            }

            bool validInput = false;
            string input = string.Empty;
            while (!validInput)
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) { input = "123"; }

                switch (input.All(Char.IsLetter))
                {
                    case true:
                        Console.Clear();
                        validInput = true;
                        break;

                    case false:
                        Console.Clear();
                        Console.WriteLine("Enbart bokstäver!");
                        break;
                }
            }

            return input;
        }

        private bool GetElectricInput()
        {
            Console.Write("Elbil: ");

            bool input;
            while (!bool.TryParse(Console.ReadLine().ToLower(), out input))
            {
                Console.Clear();
                Console.WriteLine("Alternativ: \"true\" eller \"false\"");
            }

            return input;
        }

        private int GetPassangerInput()
        {
            Console.Write("Passagerare: ");

            bool validInput = false;
            int input = 0;

            while (!validInput)
            {
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input >= 0 && input <= 300)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("En siffra mellan 0 och 300");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("En siffra mellan 0 och 300");
                }
            }

            return input;
        }
    }
}
