using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlazorApp.Services.Customers;
using System;
using BlazorApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(
            ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomers([FromQuery(Name = "page")]int page, [FromQuery(Name = "pageSize")] int pageSize)
        {
            var filters = new FiltersDto();
            if (page > 0 && pageSize > 0)
            {
                filters.Page = page;
                filters.PageSize = pageSize;
                return Ok(await _customerService.GetAllCustomersAsync(filters));
            }
            return Ok(await _customerService.GetAllCustomersAsync());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerDto customer)
        {
            if (customer == null || id != customer.Id)
            {
                return BadRequest();
            }
            await _customerService.UpdateCustomerAsync(customer);

            return NoContent();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostCustomer(CustomerDto customer)
        {
            var newCustomer = await _customerService.CreateClientAsync(customer);
            return CreatedAtAction("GetCustomer", new { id = customer.Id }, newCustomer);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            if (!await _customerService.CustomerExistsAsync(id))
            {
                return NotFound();
            }
            await _customerService.DeleteCustomerAsync(id);

            return Ok();
        }

    }
}
