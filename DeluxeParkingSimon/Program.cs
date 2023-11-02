namespace DeluxeParkingSimon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Hittade inget ställe i programmet där det skulle bli bättre med en generisk metod.
            ParkingGarage parkingGarage = new ParkingGarage();
            double waitTime = 10;
            DateTime dateTime = DateTime.Now;

            parkingGarage.NewVehicle();
            dateTime = DateTime.Now;
            while (true)
            {
                if ((DateTime.Now - dateTime).TotalSeconds > waitTime)
                {
                    parkingGarage.NewVehicle();
                    dateTime = DateTime.Now;
                }

                Console.Clear();
                Helpers.PrintVehicles(parkingGarage);
                Console.WriteLine();

                if (Helpers.WaitForInput(waitTime))
                {
                    string input = Helpers.ValidateRegistration(parkingGarage);
                    Console.WriteLine(Helpers.RemoveVehicle(parkingGarage, input));
                    Thread.Sleep(2000);
                    dateTime = DateTime.Now;
                    Console.Clear();
                }
            }
        }
    }
}