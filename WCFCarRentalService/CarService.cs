using Rental_Data;
using Rental_Logic;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Web;
using System.Net;

namespace WCFCarRentalService
{
    public class CarService : IBackoffice, ICarService, IRestService
    {
        Rentals r = new Rentals();


        //BackOffice-metoder:

        public string DeleteCar(string id)
        {
            string message = r.RemoveCar(id);
            return message;
        }

        public void DeleteCustomer(string id)
        {
            r.RemoveCustomer(id);
        }

        public void AddInitialCar()
        {
            r.AddInitialCars();
        }

        public void EditCustomer(Customer customer)
        {
            r.EditCustomer(customer);
        }

        public List<Booking> GetAllBookings(DateTime from, DateTime to)
        {
            List<Booking> list = r.GetBookingsByTime(from, to);

            return list;
        }
        public string AddBooking(string id, string regNr, DateTime from, DateTime to)
        {
            Car car = r.GetCar(regNr);
            Customer customer = r.GetCustomerById(id);
            string bookId = r.AddBooking(car, customer, from, to);
            return bookId;
        }

        public void DeleteBooking(string id)
        {
            r.RemoveBooking(id);
        }

        public string AddCar(Car car)
        {
            string response = r.AddCar(car);
            return response;
        }

        public void SaveCustomer(CustomerInfo customer)
        {
            r.AddCustomers(customer);
        }
        public List<Customer> GetCustomers(string searchString)
        {
            List<Customer> list = r.GetCustomers(searchString);
            return list;
        }

        //Här börjar CarService-metoderna:
        public List<Car> GetAllCars(DateTime from, DateTime to)
        {
            var x = r.CheckDate(from, to);

            return x;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> cc = r.GetCustomers1();

            return cc;
        }

        public CarInfo GetCar(CarRequest request)
        {
            Car car = r.GetCar(request.CarId);

            try
            {
                car.Brand = Convert.ToString(car.Brand);
                car.Model = Convert.ToString(car.Model);
                car.Year = Convert.ToInt32(car.Year);
                car.IsRented = Convert.ToBoolean(car.IsRented);
                return new CarInfo(car);
            }
            catch (FaultException)
            {
                throw new FaultException("Car " + request.CarId + " doesn't exist");
                //Klienten fångar upp ("Car " + request.CarId + " doesn't exist");
            }
        }

        public CustomerInfo GetCustomer(CustomerRequest request)
        {
            if (request.LicenseKey != "CustomerSecret123")
                throw new WebFaultException<string>("wrong license key" , HttpStatusCode.NotFound);

            Customer cust = r.GetCustomerById(request.CustomerId);
            if (cust == null)
            {
                throw new FaultException("The CustomerId : " + request.CustomerId + " is not in our database.");
                //Tänkte här att klienten kan fånga upp: faultexception.Message; när kund sökes som inte finns.

            }
                cust.Id = Convert.ToString(cust.Id);
                cust.FirstName = Convert.ToString(cust.FirstName);
                cust.LastName = Convert.ToString(cust.LastName);
                cust.EmailAddress = Convert.ToString(cust.EmailAddress);
                cust.PhoneNumber = Convert.ToString(cust.PhoneNumber);
         

                return new CustomerInfo(cust);
        }

        public Booking GetBooking(string id)
        {
            try
            {
                Booking booking = r.GetBookingById(id);
                return booking;
            }
            catch (NullReferenceException)
            {
                throw new FaultException();
               
                // Tänkte här att klienten kan fånga upp genom att skriva:"Fel bokningsnummer eller kundnummer. Försök igen.";
            }
        }


                //Här börjar REST-Metoderna
        public string BookingDisplay()
        {
            return "Welcome to our automatic check-in. Enter your booking-id and push Check-In or Check- Out";
        }

        public string CarReturned(string bookingId)
        {
            string JSONstring = OperationContext.Current.RequestContext.RequestMessage.ToString();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(JSONstring);

            string response = " Bookingid: " + bookingId + " is now returned.";


            return response;
        }

        public string CarPickedUp(string bookingId)
        {
            Booking booking = r.PickUpCar(bookingId);

            string response = booking.Id + ", " + booking.RentalCar.RegNumber + ", " + booking.RentalCar.Model + ", " +
               booking.Renter.FullName + ", " + "Pick-up status: " + booking.IsReturned;

            return response;
        }

        public void NewCustomer()
        {
            CustomerInfo c = new CustomerInfo();
            string JSONstring = OperationContext.Current.RequestContext.RequestMessage.ToString();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(JSONstring);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if ("Id" == node.Name)
                { c.CustomerId = node.InnerText; }
                else if ("FirstName" == node.Name)
                { c.FirstName = node.InnerText; }
                else if ("LastName" == node.Name)
                { c.LastName = node.InnerText; }
                else if ("PhoneNumber" == node.Name)
                { c.PhoneNumber = node.InnerText; }
                else if ("EmailAddress" == node.Name)
                { c.EmailAddress = node.InnerText; }
            }

            r.AddCustomers(c);
        }
    }
}

