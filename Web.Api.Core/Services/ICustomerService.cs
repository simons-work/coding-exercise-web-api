using Web.Api.Core.Models;

namespace Web.Api.Core.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto?> CreateAsync(CustomerDto customerDto);
    }
}