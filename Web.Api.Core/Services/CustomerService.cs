using Mapster;
using Web.Api.Core.Models;
using Web.Api.Infrastructure.Data.Entities;
using Web.Api.Infrastructure.Data.Repository;

namespace Web.Api.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        /// <summary>
        /// Register (/create) new customer.
        /// </summary>
        /// <param name="customerDto">New customer details.</param>
        /// <returns>null if customer was already registered at that email address if specified, otherwise the customerId of the new customer registration</returns>
        /// <remarks>It's possible for two or more people to share the same name and birthdate but assuming
        /// email address would be expected to be globally unique so if customer already exists then a new one is not created so that correct 400 status can be returned by the Web API layer</remarks>
        public async Task<ResponseBase> CreateAsync(CustomerDto customerDto)
        {
            var customerEntity = customerDto.Adapt<CustomerEntity>();

            if (await CustomerExists(customerEntity))
            {
                return new ErrorResponseDto { Message = $"Cannot register as customer already exists with email '{customerDto.Email}'" };
            }

            await _customerRepository.CreateAsync(customerEntity);
            if (!await _customerRepository.SaveChangesAsync())
            {
                throw new ApplicationException("CustomerService.SaveChanges() unexpected result. The number of state entries written to the database was zero");
            }

            return new CustomerResponseDto { Id = customerEntity.Id, Success = true };
        }

        private async Task<bool> CustomerExists(CustomerEntity customerEntity)
        {
            var existingCustomer = customerEntity.Email is not null ? await _customerRepository.GetCustomerByEmailAsync(customerEntity.Email) : null;
            return existingCustomer is not null;
        }
    }
}
