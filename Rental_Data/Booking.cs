using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Data
{
    public class Booking
    {
        public Car RentalCar { get; set; }
        public Customer Renter { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsReturned { get; set; }
    }
}
