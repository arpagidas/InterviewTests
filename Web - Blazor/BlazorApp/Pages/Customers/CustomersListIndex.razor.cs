using BlazorApp.Dtos;
using BlazorApp.Services.Customers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Customers
{
    public partial class CustomersListIndex : ComponentBase
    {
        [Inject]
        private ICustomerService CustomerService { get; set; }
        private PagedCustomerDtos PagedCustomers;
        private CustomerListDto CustomerToDelete;
        private CustomerDto CustomerDetails;
        private readonly FiltersDto Filters;

        public CustomersListIndex()
        {
            Filters = new FiltersDto()
            {
                Page = 1,
                PageSize = 12
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await GetCustomers();
        }

        private async Task GetCustomers()
        {
            PagedCustomers = await CustomerService.GetPagedCustomerAsync(Filters);
        }

        private async Task SelectedPage(int page)
        {
            Filters.Page = page;
            await GetCustomers();
        }

        private static string GetCustomerLink(Guid id)
            => $"/customers/edit/{id}";

        private void OnDeleteCustomerClicked(CustomerListDto customer)
        {
            CustomerToDelete = customer;
        }

        private async Task DeleteCustomer(Guid id)
        {
            await CustomerService.DeleteCustomerAsync(id);
            await GetCustomers();
        }

        private void OnCustomerDeleteClosed()
            => CustomerToDelete = null;

        private void OnCustomerDetailsClosed()
            => CustomerDetails = null;

        private async Task GetCustomerDetailsById(Guid id)
        {
            CustomerDetails = await CustomerService.GetCustomerByIdAsync(id);
        }
    }
}
