using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Web.Api.Core.Services;
using Web.Api.Core.Validators;

namespace Web.Api.Core
{
    public static class ConfigureCore
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            return services
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters()
                    .AddValidatorsFromAssemblyContaining<CustomerDtoValidator>();
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
