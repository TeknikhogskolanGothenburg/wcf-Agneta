using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Rental_Data
{
    [MessageContract(IsWrapped = true, WrapperName = "BookingRequestObject", WrapperNamespace = "http://tempuri.org/Booking")]
    public class BookingRequest
    {
        [MessageHeader(Namespace = "http://tempuri.org/Booking", ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
        public string LicenseKey { get; set; }

        [MessageBodyMember(Namespace = "http://tempuri.org/Booking")]
        public string BookingId { get; set; }
    }

    [MessageContract(IsWrapped = true, WrapperName = "BookingInfoObject", WrapperNamespace = "http://tempuri.org/Booking")]
    public class BookingInfo
    {
        public BookingInfo()
        {
        }

        public BookingInfo(Booking booking)
        {
            this.BookingId = booking.Id;
            this.Renter = booking.Renter;
            this.RentalCar = booking.RentalCar;
            this.StartTime = booking.StartTime;
            this.EndTime = booking.EndTime;
            this.IsReturned = booking.IsReturned;
        }

        [MessageBodyMember(Order = 1, Namespace = "http://tempuri.org/Booking")]
        public string BookingId { get; set; }

        [MessageBodyMember(Order = 2, Namespace = "http://tempuri.org/Booking")]
        public Car RentalCar { get; set; }

        [MessageBodyMember(Order = 3, Namespace = "http://tempuri.org/Booking")]
        public Customer Renter { get; set; }

        [MessageBodyMember(Order = 4, Namespace = "http://tempuri.org/Booking")]
        public DateTime StartTime { get; set; }

        [MessageBodyMember(Order = 5, Namespace = "http://tempuri.org/Booking")]
        public DateTime EndTime { get; set; }

        [MessageBodyMember(Order = 6, Namespace = "http://tempuri.org/Booking")]  // ev ska denna bodymember bort.
        public bool IsReturned { get; set; }
    }

    [DataContract]
    public class Booking
    {
        [DataMember(Order =1)]
        public string Id { get; set; }
        [DataMember(Order = 2)]
        public Car RentalCar { get; set; }
        [DataMember(Order = 3)]
        public Customer Renter { get; set; }
        [DataMember(Order = 4)]
        public DateTime StartTime { get; set; }
        [DataMember(Order = 5)]
        public DateTime EndTime { get; set; }
        [DataMember(Order = 6)]
        public bool IsReturned { get; set; }
    }
}
