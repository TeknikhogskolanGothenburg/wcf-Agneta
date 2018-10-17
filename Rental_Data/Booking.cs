using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Data
{
    public class Booking
    {
        Car RentalCar { get; set; }
        Customer Renter { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
    }
}
