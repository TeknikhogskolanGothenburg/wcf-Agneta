using System;
using System.Collections.Generic;
using Rental_Data;

namespace Rental_Logic
{
    public class Rentals
    {
        public List<Car> Cars = new List<Car>();
        public List<Customer> Customers = new List<Customer>();
        public List<Booking> Bookings = new List<Booking>();

        public void AddCar(int regNumber, string brand, int year, string model)
        {
            Car newCar = new Car() {
                RegNumber = regNumber,
                Brand = brand,
                Year = year,
                Model = model,
                IsRented = false
            };
            Cars.Add(newCar);
        }
    }
    
}
