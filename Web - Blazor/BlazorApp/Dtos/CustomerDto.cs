using BlazorApp.Models;
using System;

namespace BlazorApp.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public CustomerDto() { }
        public CustomerDto(Customer customer)
        {
            Id = customer.Id;
            CompanyName = customer.CompanyName;
            ContactName = customer.ContactName;
            Address = customer.Address;
            City = customer.City;
            Region = customer.Region;
            PostalCode = customer.PostalCode;
            Country = customer.Country;
            Phone = customer.Phone;
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(CompanyName)) return false;
            if (string.IsNullOrEmpty(ContactName)) return false;
            if (string.IsNullOrEmpty(Address)) return false;
            if (string.IsNullOrEmpty(City)) return false;
            if (string.IsNullOrEmpty(Region)) return false;
            if (string.IsNullOrEmpty(PostalCode)) return false;
            if (string.IsNullOrEmpty(Country)) return false;
            if (string.IsNullOrEmpty(Phone)) return false;
            return true;
        }
    }
}
