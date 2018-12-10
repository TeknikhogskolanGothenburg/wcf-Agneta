using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;

namespace Rental_Data
{
    [MessageContract(IsWrapped =true, WrapperName = "CustomerRequestObject", WrapperNamespace = "http://tempuri.org/Customer")]
    public class CustomerRequest
    {
        [MessageHeader(Namespace = "http://tempuri.org/Customer", ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        public string LicenseKey { get; set; }

        [MessageBodyMember(Namespace = "http://tempuri.org/Customer")]
        public string CustomerId { get; set; }
    }
    [MessageContract(IsWrapped = true, WrapperName = "CustomerInfoObject", WrapperNamespace = "http://tempuri.org/Customer")]
    public class CustomerInfo
    {
        public CustomerInfo()
        {

        }
        public CustomerInfo(Customer customer)
        {
            this.CustomerId = customer.Id;
            this.FirstName = customer.FirstName;
            this.LastName = customer.LastName;
            this.PhoneNumber = customer.PhoneNumber;
            this.EmailAddress = customer.EmailAddress;
        }

        [MessageBodyMember(Order = 1, Namespace = "http://tempuri.org/Customer")]
        public string CustomerId { get; set; }

        [MessageBodyMember(Order = 2, Namespace = "http://tempuri.org/Customer")]
        public string FirstName { get; set; }

        [MessageBodyMember(Order = 3, Namespace = "http://tempuri.org/Customer")]
        public string LastName { get; set; }

        [MessageBodyMember(Order = 4, Namespace = "http://tempuri.org/Customer")]
        public string PhoneNumber { get; set; }

        [MessageBodyMember(Order = 5, Namespace = "http://tempuri.org/Customer")]
        public string EmailAddress { get; set; }
    } 

    [DataContract]
    public class Customer 
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        [DataMember(Order = 2)]
        public string FirstName { get; set; }
        [DataMember(Order = 3)]
        public string LastName { get; set; }
        [DataMember(Order = 4)]
        public string PhoneNumber { get; set; }
        [DataMember(Order = 5)]
        public string EmailAddress { get; set; }
       

        public string FullName                    //används inte
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
