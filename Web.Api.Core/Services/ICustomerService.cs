using Web.Api.Core.Models;

namespace Web.Api.Core.Services
{
    public interface ICustomerService
    {
        Task<ResponseBase> CreateAsync(CustomerDto customerDto);
    }
}