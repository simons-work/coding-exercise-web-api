﻿using Microsoft.EntityFrameworkCore;
using Web.Api.Infrastructure.Data.Entities;

namespace Web.Api.Infrastructure.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InsuranceDbContext dbContext;

        public CustomerRepository(InsuranceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CustomerEntity customerEntity)
        {
            await dbContext.Customers.AddAsync(customerEntity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<CustomerEntity?> GetCustomerByEmailAsync(string email)
        {
            if (email == null) return null;
            var customer = await dbContext.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
            return customer;
        }
    }
}
