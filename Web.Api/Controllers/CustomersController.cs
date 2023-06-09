using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Models;
using Web.Api.Core.Services;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
        {
            this._logger = logger;
            this._customerService = customerService;
        }

        /// <summary>
        /// Register (/create) new customer.
        /// </summary>
        /// <param name="customerDto">New customer details.</param>
        /// <returns>New customer Id if the customer can be registered.</returns>
        [HttpPost]
        public async Task<ActionResult> Create(CustomerDto customerDto)
        {
            try
            {
                var responseDto = await _customerService.CreateAsync(customerDto);
                return responseDto.Success ? Ok(responseDto) : BadRequest(responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController.Create() exception: {0}", ex);
                throw;
            }
        }
    }
}