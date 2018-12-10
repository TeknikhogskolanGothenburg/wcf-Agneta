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
    
    public interface IBackoffice
    {

        [OperationContract]
        string AddCar(Car car);

        [OperationContract]
        string DeleteCar(string id); 

        [OperationContract]
        List <Booking> GetAllBookings(DateTime from, DateTime to);

        [OperationContract (Name= "GetCustomersFullSearch")]
        List<Customer> GetCustomers(string searchString);

        [OperationContract]
        void EditCustomer(Customer customer);

        [OperationContract]
        void DeleteCustomer(string id);

    }

    [ServiceContract]
    public interface ICarService
    {
        [OperationContract]
        void AddInitialCar();

        [OperationContract]
        List<Car> GetAllCars(DateTime from, DateTime to);

        [OperationContract]
        List<Customer> GetAllCustomers();

        //Egentligen behöver jag inte sätta Name på varje, men i första designen tänkte jag mig att ha en overload på varje,
        //en där kunden kunde söka på sitt id och en där Callcenter kunde söka på valfri property. Detta frångick jag sen men lät Name stå kvar.
        [OperationContract(Name = "GetCustomerString")]
        CustomerInfo GetCustomer(CustomerRequest request);

        [OperationContract(Name = "SaveCustomerString")]
        void SaveCustomer(CustomerInfo customer);

        [OperationContract(Name = "AddBookingInt")]
        string AddBooking(string id, string car, DateTime from, DateTime to);

        [OperationContract(Name = "GetCarObject")]
        CarInfo GetCar(CarRequest request);

        [OperationContract(Name = "DeleteBookingString")]
        void DeleteBooking(string id);

        [OperationContract(Name = "GetBookingString")]
        Booking GetBooking(string id);
    }
}
