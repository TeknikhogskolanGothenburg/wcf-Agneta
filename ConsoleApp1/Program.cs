using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;

namespace RestHost
{
    public class Program
    {
       static void Main(String[] args)
        {

            WebServiceHost restHost = new WebServiceHost(typeof(WCFCarRentalService.CarService),
         new Uri("http://localhost:8081"));

            ServiceEndpoint ep = restHost.AddServiceEndpoint(
                typeof(WCFCarRentalService.IRestService), new WebHttpBinding(), "");

            restHost.Description.Endpoints[0].Behaviors.Add(
                new WebHttpBehavior { HelpEnabled = true });

            ServiceDebugBehavior sdb = restHost.Description.Behaviors.Find<ServiceDebugBehavior>();

            sdb.IncludeExceptionDetailInFaults = true;
            //sdb.HttpsHelpPageEnabled = true;

            restHost.Open();
            Console.WriteLine("Service is running");
            Console.ReadLine();
            restHost.Close();
           
        }
    }
}
