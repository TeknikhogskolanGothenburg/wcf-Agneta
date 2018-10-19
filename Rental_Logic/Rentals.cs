using Rental_Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rental_Logic
{
    public class Rentals
    {
        public List<Car> Cars = new List<Car>();
        public List<Customer> Customers = new List<Customer>();
        public List<Booking> Bookings = new List<Booking>();

        public void AddCar(string regNumber, string brand, int year, string model)
        {
            Car newCar = new Car()
            {
                RegNumber = regNumber,
                Brand = brand,
                Year = year,
                Model = model,
                IsRented = false
            };
            Cars.Add(newCar);
        }

        public void RemoveCar(string regNumber)
        {
            Cars.RemoveAll(b => b.RegNumber == regNumber);
        }

        public void AddCustomer(string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            Customer newCustomer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                EmailAddress = emailAddress

            };
            Customers.Add(newCustomer);
        }

        public void EditCustomer(Customer customer)
        {
            foreach (var cust in Customers.Where(c => c.Id == customer.Id))
            {
                cust.FirstName = customer.FirstName;
                cust.LastName = customer.LastName;
                cust.PhoneNumber = customer.PhoneNumber;
                cust.EmailAddress = customer.EmailAddress;
            }
        }

        public void RemoveCustomer(string firstName, string lastName, int id)
        {
            Customers.RemoveAll(c => c.FirstName == firstName && c.LastName == lastName && c.Id == id);
        }

        public void AddBooking(Car rentalCar, Customer renter, DateTime startTime, DateTime endTime)
        {
            Booking newBooking = new Booking()
            {
                Id = renter.Id + rentalCar.RegNumber + startTime,    // för lite med bara namn och id. kunden kan återkomma med flera bokningar.
                RentalCar = rentalCar,
                Renter = renter,
                StartTime = startTime,
                EndTime = endTime,
                IsReturned = false                              

            };
            Bookings.Add(newBooking);
        }

        public void RemoveBooking(string bookingId)
        { 
            Bookings.RemoveAll(b => b.Id == bookingId);
        }

        public void GetCar(Booking booking)          // När kunden kvitterar ut sin hyrbil.
        {
            booking.RentalCar.IsRented = true;
        }

        public void ReturnCar(Booking booking)
        {
            if (booking.IsReturned == false && booking.RentalCar.IsRented == true)// IsRented sätts aldrig till True i koden. Behövs den alls? lägger till på rad 84
            {
                booking.IsReturned = true;
                booking.RentalCar.IsRented = false;
            }

            if (DateTime.Now < booking.EndTime)
            {
                booking.EndTime = DateTime.Now;
            }
        }

        public List<Car> CheckDate( DateTime startDate, DateTime endDate)
        {
            List<Car> availableCars = Cars.ToList();
            foreach (var booking in Bookings)
            {
                if (startDate < booking.StartTime && endDate > booking.StartTime)
                {
                    availableCars.RemoveAll(b => b.RegNumber == booking.RentalCar.RegNumber);                 
                }        
            }
            return availableCars;
        }

    }
}