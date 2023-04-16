using Microsoft.EntityFrameworkCore;
using Web.Api.Infrastructure.Data.Entities;

namespace Web.Api.Infrastructure.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public CustomerRepository(InsuranceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateAsync(CustomerEntity customerEntity)
        {
            await _dbContext.Customers.AddAsync(customerEntity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<CustomerEntity?> GetCustomerByEmailAsync(string? email)
        {
            if (email is null) return null;
            var customer = await _dbContext.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
            return customer;
        }
    }
}
