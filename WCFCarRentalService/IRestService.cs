using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Rental_Data;
using Rental_Logic;

namespace WCFCarRentalService
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Welcome",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        string BookingDisplay();

        [OperationContract]
        [WebInvoke(Method = "PUT",
         UriTemplate = "Pickup",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        string CarReturned(string bookingId);

        [OperationContract]
        [WebInvoke(Method = "PUT",
         UriTemplate = "Return",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        string CarPickedUp(string bookingId);

        [OperationContract]     //denna hör inte hit enligt min design. Lade den här för jag ville testa en POST
        [WebInvoke(Method = "POST",   // och för att det stod i projektbeskrivningen.
         UriTemplate = "Customer",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        void NewCustomer();
    }
}
