using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Data
{
    public class Booking
    {
        private Car RentalCar { get; set; }
        private Customer Renter { get; set; }
        private DateTime StartTime { get; set; }
        private DateTime EndTime { get; set; }
    }
}
