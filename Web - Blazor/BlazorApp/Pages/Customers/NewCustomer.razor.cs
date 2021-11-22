using BlazorApp.Dtos;
using BlazorApp.Services.Customers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Customers
{
    public partial class NewCustomer : ComponentBase
    {
        [Inject]
        public ICustomerService CustomerService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public CustomerDto Customer { get; set; }

        private bool _submittedCustomer = false;
        private const string _invalidBorderColor = "border-color: rgb(224,52,15);";
        private const string _validBorderColor = "border-color: rgba(0,0,0,.1);";

        public NewCustomer()
        {
            Customer = new CustomerDto();
        }

        public async Task CreateCustomer()
        {
            if (Customer.IsValid())
            {
                Customer = await CustomerService.CreateClientAsync(Customer);
                NavigationManager.NavigateTo("/customers");
            }
            else 
                _submittedCustomer = true;
        }

        private string GetCompanyNameStyle()
            => string.IsNullOrEmpty(Customer.CompanyName) && _submittedCustomer
                ? _invalidBorderColor : _validBorderColor;

        private string GetContactNameStyle()
            => string.IsNullOrEmpty(Customer.ContactName) && _submittedCustomer
                ? _invalidBorderColor : _validBorderColor;

        private string GetAddressStyle()
            => string.IsNullOrEmpty(Customer.Address) && _submittedCustomer
                ? _invalidBorderColor : _validBorderColor;

        private string GetCityStyle()
            => string.IsNullOrEmpty(Customer.City) && _submittedCustomer
                ? _invalidBorderColor : _validBorderColor;

        private string GetRegionStyle()
            => string.IsNullOrEmpty(Customer.Region) && _submittedCustomer
                ? _invalidBorderColor : _validBorderColor;

        private string GetPostalCodeStyle()
            => string.IsNullOrEmpty(Customer.PostalCode) && _submittedCustomer
                ? _invalidBorderColor : _validBorderColor;

        private string GetCountryStyle()
            => string.IsNullOrEmpty(Customer.Country) && _submittedCustomer
                ? _invalidBorderColor : _validBorderColor;

        private string GetPhoneStyle()
            => string.IsNullOrEmpty(Customer.Phone) && _submittedCustomer
                ? _invalidBorderColor : _validBorderColor;
    }
}
