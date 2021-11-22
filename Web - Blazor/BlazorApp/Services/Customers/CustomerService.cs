using BlazorApp.Data;
using BlazorApp.Dtos;
using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly BlazorAppContext _context;

        public CustomerService(
            BlazorAppContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerDto(c))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync(FiltersDto filters)
        {
            int page = (filters.Page - 1) * filters.PageSize;

            return await _context.Customers
                .Skip(page)
                .Take(filters.PageSize)
                .Select(c => new CustomerDto(c))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PagedCustomerDtos> GetPagedCustomerAsync(FiltersDto filters)
        {
            PagedCustomerDtos pagedCustomers = new PagedCustomerDtos();

            int page = (filters.Page - 1) * filters.PageSize;

            pagedCustomers.Customers = await _context.Customers
                .OrderBy(c => c.ContactName)
                .Skip(page)
                .Take(filters.PageSize)
                .Select(c => new CustomerListDto
                {
                    Id = c.Id,
                    ContactName = c.ContactName,
                    CompanyName = c.CompanyName,
                    City = c.City
                })
                .AsNoTracking()
                .ToListAsync();
            int totalCustomers = await _context.Customers.CountAsync();
            pagedCustomers.UpdateMetadata(totalCustomers, filters.Page, filters.PageSize);

            return pagedCustomers;
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
        {
            return await _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDto(c))
                .FirstOrDefaultAsync();
        }

        public async Task<CustomerDto> CreateClientAsync(CustomerDto customer)
        {
            var newCustomer = new Customer()
            {
                ContactName = customer.ContactName,
                CompanyName = customer.CompanyName,
                Phone = customer.Phone,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            customer.Id = newCustomer.Id;
            return customer;
        }

        public async Task<bool> CustomerExistsAsync(Guid id)
        {
            return await _context.Customers.AnyAsync(e => e.Id == id);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(e => e.Id == id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCustomerAsync(CustomerDto editedCustomer)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == editedCustomer.Id);

            if (!customer.CompanyName.Equals(editedCustomer.CompanyName))
                customer.CompanyName = editedCustomer.CompanyName;
            if (!customer.ContactName.Equals(editedCustomer.ContactName))
                customer.ContactName = editedCustomer.ContactName;
            if (!customer.Address.Equals(editedCustomer.Address))
                customer.Address = editedCustomer.Address;
            if (!customer.City.Equals(editedCustomer.City))
                customer.City = editedCustomer.City;
            if (!customer.Region.Equals(editedCustomer.Region))
                customer.Region = editedCustomer.Region;
            if (!customer.PostalCode.Equals(editedCustomer.PostalCode))
                customer.PostalCode = editedCustomer.PostalCode;
            if (!customer.Country.Equals(editedCustomer.Country))
                customer.Country = editedCustomer.Country;
            if (!customer.Phone.Equals(editedCustomer.Phone))
                customer.Phone = editedCustomer.Phone;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await CustomerExistsAsync(editedCustomer.Id))
                {
                    throw;
                }
            }
        }
    }
}
