using System;
using System.ServiceModel;

namespace CarRentalServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WCFCarRentalService.CarService)))
            {
                host.Open(); Console.WriteLine("Host started @ " + DateTime.Now.ToString());

                Console.ReadLine();
            }


        }
    }
}
