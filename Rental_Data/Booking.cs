using System;

namespace Rental_Data
{
    public class Booking
    {
        public string Id { get; set; }
        public Car RentalCar { get; set; }
        public Customer Renter { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Time { get; set; }        // DENNA KANSKE INTE BEHÖVS HAR DEN FÖR ATT KOLLA OM ENDTIME ÄR STÖRRE ÄN TIME
        public bool IsReturned { get; set; }
    }
}
