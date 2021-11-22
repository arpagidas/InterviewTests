using BlazorApp.Dtos;
using BlazorApp.Services.Customers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Customers
{
    public partial class EditCustomer : ComponentBase
    {
        [Parameter]
        public string CustomerId { get; set; }

        [Inject]
        public ICustomerService CustomerService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private CustomerDto Customer;
        private bool _finishedLoading = false;

        private bool _submittedCustomer = false;
        private const string _invalidBorderColor = "border-color: rgb(224,52,15);";
        private const string _validBorderColor = "border-color: rgba(0,0,0,.1);";

        protected override async Task OnInitializedAsync()
        {
            if (Guid.TryParse(CustomerId, out var id))
                Customer = await CustomerService.GetCustomerByIdAsync(id);
            _finishedLoading = true;
        }

        private async Task UpdateCustomer()
        {
            if (Customer.IsValid())
            {
                await CustomerService.UpdateCustomerAsync(Customer);
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
