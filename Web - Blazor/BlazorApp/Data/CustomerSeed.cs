using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class CustomerSeed
    {
        public static async Task SeedCustomersAsync(BlazorAppContext context)
        {
            bool hasAnyNewCustomer = false;
            for (int i = 1; i <= 50; i++)
            {
                if (!await context.Customers.AnyAsync(c => c.ContactName.Equals($"Customer {i}")))
                {
                    var customer = new Customer
                    {
                        ContactName = $"Customer {i}",
                        CompanyName = $"Company {i % 2 + 1}",
                        City = $"City {i % 3 + 1}",
                        Address = $"City {i % 4 + 1}",
                        Country = $"City {i % 5 + 1}",
                        Phone = $"691234567{i % 10}",
                        PostalCode = $"1234{i % 10}",
                        Region = $"Region {i % 6 + 1}"
                    };
                    context.Customers.Add(customer);
                    hasAnyNewCustomer = true;
                }
            }

            if (hasAnyNewCustomer)
                await context.SaveChangesAsync();
        }
    }
}
