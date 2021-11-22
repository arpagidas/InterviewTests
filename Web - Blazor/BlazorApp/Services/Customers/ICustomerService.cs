using BlazorApp.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services.Customers
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<List<CustomerDto>> GetAllCustomersAsync(FiltersDto filters);
        Task<PagedCustomerDtos> GetPagedCustomerAsync(FiltersDto filters);
        Task<CustomerDto> GetCustomerByIdAsync(Guid id);
        Task<CustomerDto> CreateClientAsync(CustomerDto customer);
        Task<bool> CustomerExistsAsync(Guid id);
        Task DeleteCustomerAsync(Guid id);
        Task UpdateCustomerAsync(CustomerDto editedCustomer);
    }
}
