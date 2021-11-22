using System;

namespace BlazorApp.Dtos
{
    public class CustomerListDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string City { get; set; }
    }
}
