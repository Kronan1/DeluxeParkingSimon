using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSimon
{
    internal class Helpers
    {
        public static string ValidateRegistration(ParkingGarage parkingGarage)
        {
            Console.Clear();
            bool correctInput = false;
            string input = "";

            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
            Console.Clear();

            while (!correctInput)
            {
                Console.Write("Registreringsnummer: ");

                input = Console.ReadLine();
                Console.Clear();

                if (input.Length == 6)
                {
                    input = input.ToUpper();
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (!Char.IsLetter(input[i]) && i < 3)
                        {
                            break;
                        }

                        if (!Char.IsDigit(input[i]) && i >= 3)
                        {
                            break;
                        }

                        if (i == 5)
                        {
                            correctInput = true;
                        }
                    }
                }
                Console.WriteLine("Format: ABC123");
            }

            return input;
        }
        public static string RemoveVehicle(ParkingGarage parkingGarage, string input)
        {
            Console.Clear();
            for (int i = 0; i < parkingGarage.ParkingSpaces.Count; i++)
            {
                List<Vehicle> vehicleList = parkingGarage.ParkingSpaces[i].VehicleList;
                for (int j = 0; j < vehicleList.Count; j++)
                {
                    if (vehicleList[j].Registration == input)
                    {
                        double size = 0;
                        string vehicleType = "";
                        switch (vehicleList[j])
                        {
                            case Car car:
                                size = 1;
                                vehicleType = "Bil";
                                break;
                            case Bus bus:
                                vehicleType = "Buss";
                                size = 1;
                                break;
                            case Motorcycle motorcycle:
                                vehicleType = "Motorcykel";
                                size = 0.5;
                                break;
                        }

                        string cost = ((DateTime.Now - vehicleList[j].parkingStarted).TotalMinutes * 1.5).ToString("F" + 2);

                        string removedVehicle =
                            vehicleType +
                            " med registreringsnummer " +
                            vehicleList[j].Registration +
                            " har lämnat garaget. Det kostade "
                            + cost
                            + " kronor.";

                        switch (vehicleList[j])
                        {
                            case Bus bus:
                                parkingGarage.ParkingSpaces[i].VehicleList.Remove(bus);
                                parkingGarage.ParkingSpaces[i].Space += size;
                                parkingGarage.ParkingSpaces[i + 1].VehicleList.Remove(bus);
                                parkingGarage.ParkingSpaces[i + 1].Space += size;
                                break;

                            case Motorcycle motorcycle:
                            case Car car:
                                parkingGarage.ParkingSpaces[i].VehicleList.Remove(vehicleList[j]);
                                parkingGarage.ParkingSpaces[i].Space += size;
                                break;

                        }

                        return removedVehicle;
                    }
                }
            }
            return "Inget fordon med det registreringsnumret finns i garaget.";
        }
        public static void PrintVehicles(ParkingGarage parkingGarage)
        {
            for (int i = 0; i < parkingGarage.ParkingSpaces.Count(); i++)
            {
                if (parkingGarage.ParkingSpaces[i].Space < 1)
                {
                    List<Vehicle> vehicles = parkingGarage.ParkingSpaces[i].VehicleList;
                    if (vehicles.First() is Car car)
                    {
                        Console.WriteLine("Plats  " + (i + 1) + "\tBil\t" + car.Registration + "\t" + car.ColorOfVehicle + "\t" + (car.Electric ? "Elbil" : "Ej Elbil"));
                    }
                    else if (vehicles.First() is Bus bus)
                    {
                        Console.WriteLine("Plats  " + (i + 1) + "-" + (i + 2) + "\tBuss\t" + bus.Registration + "\t" + bus.ColorOfVehicle + "\t" + bus.Passangers);
                        i++;
                    }
                    else if (vehicles.First() is Motorcycle motorcycle)
                    {

                        foreach (Motorcycle motorcycle1 in vehicles)
                        {
                            Console.WriteLine("Plats  " + (i + 1) + "\tMC\t" + motorcycle1.Registration + "\t" + motorcycle1.ColorOfVehicle + "\t" + motorcycle1.Brand);
                        }
                    }
                }
            }
        }
        public static bool WaitForInput(double waitTime)
        {
            Console.WriteLine("Tryck på någon knapp för att ta bort ett fordon.");

            DateTime dateTime = DateTime.Now;
            bool input = false;
            while ((DateTime.Now - dateTime).TotalSeconds <= waitTime && !input)
            {
                switch (Console.KeyAvailable)
                {
                    case true:
                        input = true;
                        break;
                    case false:
                        Thread.Sleep(100);
                        break;
                }
            }

            Console.Clear();
            return input;

            #region Utkommenterade tester
            // ------------------------------------------------
            // Försökte mig på en rekursiv metod som inte fungerade korrekt. 
            // Den gick in i true vid korrekt tillfälle men returnerade inte true efter utan fortsatte köras tills den övre if-satsen var true.

            //if ((DateTime.Now - dateTime).TotalSeconds >= waitTime)
            //{
            //    return false;
            //}

            //switch (Console.KeyAvailable)
            //{
            //    case true:
            //        return true;

            //    case false:
            //        Thread.Sleep(100);
            //        WaitForInput(waitTime);
            //        break;

            //}
            //return false;

            // ---------------------------------------------------------
            // Provade om felet låg i switch-satsen men så var inte fallet.

            //if ((DateTime.Now - dateTime).TotalSeconds >= 60)
            //{
            //    return false;
            //}

            //if (Console.KeyAvailable)
            //{
            //    return true;
            //}
            //else
            //{
            //    Thread.Sleep(100);
            //    WaitForInput(dateTime);
            //    return false;
            //}


            // ---------------------------------------------
            // Provade om felet var Thread.Sleep men det var inte det.

            //if ((DateTime.Now - dateTime).TotalSeconds >= 60)
            //{
            //    return false;
            //}

            //switch (Console.KeyAvailable)
            //{
            //    case true:
            //        return true;

            //    case false:
            //        // Thread.Sleep(100);
            //        DateTime dateTime1 = DateTime.Now;
            //        while ((DateTime.Now - dateTime1).TotalMilliseconds < 100)
            //        {

            //        }
            //        WaitForInput(dateTime);
            //        return false;
            //}

            // -----------------------------------------------------
            // Provade om felet var att värdet returnerades i switch-satsen men det var inte heller problemet.

            //if ((DateTime.Now - dateTime).TotalSeconds >= waitTime)
            //{
            //    return false;
            //}

            //bool value = false;
            //switch (Console.KeyAvailable)
            //{
            //    case true:
            //        value = true;
            //        break;

            //    case false:
            //        Thread.Sleep(100);
            //        WaitForInput(waitTime);
            //        break;

            //}
            //return value;
            #endregion
        }
    }
}
