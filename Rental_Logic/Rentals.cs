using Rental_Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rental_Logic
{
    public class Rentals
    {
        private static readonly object customersLock = new object();
        public static List<Car> Cars = new List<Car>();
        public static List<Customer> Customers = new List<Customer>();
        public static List<Booking> Bookings = new List<Booking>();

        public void AddInitialCars()
        {
            Car car = new Car() { RegNumber = "sss222", Brand = "Toyota", Model = "Auris", IsRented = false, Year = 2016 }; Cars.Add(car);
            Car car2 = new Car() { RegNumber = "aaa111", Brand = "Volvo", Model = "xc90", IsRented = false, Year = 2015 }; Cars.Add(car2);
            Car car3 = new Car() { RegNumber = "zzz333", Brand = "Audi", Model = "A9", IsRented = false, Year = 2017 }; Cars.Add(car3);
            Car car4 = new Car() { RegNumber = "xxx444", Brand = "Opel", Model = "Astra", IsRented = false, Year = 2009 }; Cars.Add(car4);
            Car car5 = new Car() { RegNumber = "ccc555", Brand = "Toyota", Model = "Yaris", IsRented = false, Year = 2010 }; Cars.Add(car5);
            Customer customer = new Customer() { Id = "711220-1234", FirstName = "Agneta", LastName = "Gustafsson", EmailAddress = "sdfsd@sdfsfd.com", PhoneNumber = "12345645" }; Customers.Add(customer);
            Customer customer1 = new Customer() { Id = "811220-1234", FirstName = "Elin", LastName = "Gustafsson", EmailAddress = "wwwww@sdfsfd.com", PhoneNumber = "1245" }; Customers.Add(customer1);
            Customer customer2 = new Customer() { Id = "1", FirstName = "Alexander", LastName = "Severin", EmailAddress = "wwwww@sdfsfd.com", PhoneNumber = "1245" }; Customers.Add(customer2);
            Booking booking = new Booking() { Id = customer1.Id + car.RegNumber + "2018-12-03", RentalCar = car2, Renter = customer1, StartTime = new DateTime(2018, 12, 05, 0, 0, 0), EndTime = new DateTime(2018, 12, 15, 0, 0, 0), IsReturned = false }; Bookings.Add(booking);
            Booking booking1 = new Booking() { Id = customer2.Id + car.RegNumber + "2018-11-01", RentalCar = car2, Renter = customer2, StartTime = new DateTime(2018, 11, 05, 0, 0, 0), EndTime = new DateTime(2018, 12, 15, 0, 0, 0), IsReturned = false }; Bookings.Add(booking1);
            Booking booking2 = new Booking() { Id = customer.Id + car.RegNumber + "2018-12-01", RentalCar = car4, Renter = customer, StartTime = new DateTime(2018, 11, 01, 0, 0, 0), EndTime = new DateTime(2018, 11, 15, 0, 0, 0), IsReturned = false }; Bookings.Add(booking2);
            Booking booking3 = new Booking() { Id = customer1.Id + car.RegNumber + "2018-11-05", RentalCar = car, Renter = customer1, StartTime = new DateTime(2018, 11, 05, 0, 0, 0), EndTime = new DateTime(2018, 12, 01, 0, 0, 0), IsReturned = false }; Bookings.Add(booking3);
            Booking booking4 = new Booking() { Id = customer2.Id + car.RegNumber + "2018-12-15", RentalCar = car5, Renter = customer2, StartTime = new DateTime(2018, 12, 15, 0, 0, 0), EndTime = new DateTime(2018, 12, 25, 0, 0, 0), IsReturned = false }; Bookings.Add(booking4);
        }


        public string AddCar(Car car)
        {
            Car newCar = new Car()
            {
                RegNumber = car.RegNumber,
                Brand = car.Brand,
                Year = car.Year,
                Model = car.Model,
                IsRented = false
            };
            Cars.Add(newCar);
            //  Console.WriteLine("car has passed saved");
            return "for certain";
        }

        public string RemoveCar(string regNumber)
        {
            lock (customersLock)
            {
                var car = Cars.Find(b => b.RegNumber == regNumber);
                if (car.IsRented == false)
                {
                    car.IsRented = true;
                    return " is removed";
                }
                else
                {
                    return " is already taken out for service.";
                }
            }
        }


        public Car GetCar(string regNumber)
        {
            lock (customersLock)
            {
                Car cc = Cars.Find(c => c.RegNumber == regNumber);

                if (cc.IsRented == true)
                {
                    cc.RegNumber = regNumber;
                    cc.Brand = "";
                    cc.Year = 0;
                    cc.Model = "";
                    return cc;
                }
                else
                {
                    return cc;
                }
            }
        }


        public void AddCustomers(CustomerInfo c)
        {
            lock (customersLock)
            {
                Customer newCustomer = new Customer()
                {
                    Id = c.CustomerId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    EmailAddress = c.EmailAddress

                };
                Customers.Add(newCustomer);


                List<Customer> list = GetCustomers1();
                foreach(var x in list)
                {
                    Console.WriteLine(x.Id + " " + x.FullName);
                }

            }
        }


        public void EditCustomer(Customer customer)
        {
            lock (customersLock)
            {
                foreach (var cust in Customers.Where(c => c.Id == customer.Id))
                {
                    cust.FirstName = customer.FirstName;
                    cust.LastName = customer.LastName;
                    cust.PhoneNumber = customer.PhoneNumber;
                    cust.EmailAddress = customer.EmailAddress;
                }
            }
        }

        public void RemoveCustomer(string id)
        {
            lock (customersLock)
            {
                Customers.RemoveAll(c => c.Id == id);
            }
        }
        public List<Customer> GetCustomers1()
        {
            return Customers.FindAll(c => c.Id != null);
        }
        public List<Customer> GetCustomers(string searchString)
        {
            return Customers.FindAll(c => c.FirstName.StartsWith(searchString) || c.LastName.StartsWith(searchString) || c.PhoneNumber.StartsWith(searchString) || c.EmailAddress.StartsWith(searchString));
        }

        public Customer GetCustomerById(string id)
        {
            lock (customersLock)
            {
                List<Customer> customer = GetCustomers1();
                Customer cust = customer.Find(c => c.Id == id);

                return cust;

            }

        }

        public string AddBooking(Car car, Customer customer, DateTime startTime, DateTime endTime)
        {
            var from = Convert.ToDateTime(startTime).ToShortDateString();
            Booking newBooking = new Booking()
            {
                Id = customer.Id + car.RegNumber + from,   
                RentalCar = car,
                Renter = customer,
                StartTime = startTime,
                EndTime = endTime,
                IsReturned = false

            };
            Bookings.Add(newBooking);
            return newBooking.Id;
        }
        public Booking GetBookingById(string bookingId)
        {
            lock (customersLock)
            {
                return Bookings.Find(b => b.Id == bookingId || b.Renter.Id == bookingId);
            }
        }

        public void RemoveBooking(string bookingId)
        {
            lock (customersLock)
            {
                Bookings.RemoveAll(b => b.Id == bookingId);
            }
        }

        public void ReturnCar(Booking booking)
        {
            booking.IsReturned = false;

            if (DateTime.Now < Convert.ToDateTime(booking.EndTime))
            {
                booking.EndTime = DateTime.Now;
            }
        }
        public Booking PickUpCar(string bookingId)
        {
            AddInitialCars();
            Booking booking = GetBookingById(bookingId);
            booking.IsReturned = true;

            return booking;

        }

        public List<Car> CheckDate(DateTime startDate, DateTime endDate)
        {
            lock (customersLock)
            {

                List<Booking> book = Bookings.ToList();
                List<Car> availableCars = Cars.ToList();
                List<Customer> cus = Customers.ToList();
                foreach (var booking in book)
                {
                    if (startDate < booking.StartTime && endDate > booking.StartTime && booking.RentalCar.IsRented == false)
                    {
                        availableCars.RemoveAll(b => b.RegNumber == booking.RentalCar.RegNumber);
                    }
                }
                return availableCars;
            }
        }

        public List<Booking> GetBookingsByTime(DateTime start, DateTime end)
        {
            var bookings = Bookings.ToList();
            foreach (var booking in bookings)
            {
                var from = Convert.ToDateTime(booking.StartTime);

                if (start < from && end > from)
                {
                    bookings.Remove(booking);
                }
            }
            return bookings;
        }
    }
}