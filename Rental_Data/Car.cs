using System.Runtime.Serialization;
using System.ServiceModel;

namespace Rental_Data
{
    [MessageContract(IsWrapped = true, WrapperName = "CarRequestObject", WrapperNamespace = "http://tempuri.org/Car")]
    public class CarRequest
    {
        [MessageHeader(Namespace = "http://tempuri.org/Car", ProtectionLevel = System.Net.Security.ProtectionLevel.Sign)]
        public string LicenseKey { get; set; }

        [MessageBodyMember(Namespace = "http://tempuri.org/Car")]
        public string CarId { get; set; }
    }
    [MessageContract(IsWrapped = true, WrapperName = "CarInfoObject", WrapperNamespace = "http://tempuri.org/Car")]
    public class CarInfo
    {

        public CarInfo()
        {
        }

        public CarInfo(Car car)
        {
            this.CarId = car.RegNumber;
            this.Brand = car.Brand;
            this.Year = car.Year;
            this.Model = car.Model;
            this.IsRented = car.IsRented;  //ev bort

        }

        [MessageBodyMember(Order = 1, Namespace = "http://tempuri.org/Car")]
        public string CarId { get; set; }

        [MessageBodyMember(Order = 2, Namespace = "http://tempuri.org/Car")]
        public string Brand { get; set; }

        [MessageBodyMember(Order = 3, Namespace = "http://tempuri.org/Car")]
        public int Year { get; set; }

        [MessageBodyMember(Order = 4, Namespace = "http://tempuri.org/Car")]
        public string Model { get; set; }

      //  [MessageBodyMember(Order = 5, Namespace = "http://tempuri.org/Car")]  // ev ska denna inte ha bodymember
        public bool IsRented { get; set; }  // Tänkte att denna inte var väsentlig för klienten utan används bara för att styra om bilen är i bruk eller inte.
                                            //(Namnvalet kanske inte är det bästa...)
    }

    [DataContract]
    public class Car
    {
        [DataMember(Order = 1)]
        public string RegNumber { get; set; }  
        [DataMember(Order = 2)]
        public string Brand { get; set; }
        [DataMember(Order = 3)]
        public int Year { get; set; }
        [DataMember(Order = 4)]
        public string Model { get; set; }
      //  [DataMember(Order = 5)]
        public bool IsRented { get; set; }

    }
}