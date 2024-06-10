using ITT.CRM.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITT.CRM.Infrastructure
{ 
    public static class ContactDependencyInjection
    {
        public static IServiceCollection AddContactService(this IServiceCollection services
            , IConfiguration configuration)
        {
            // Add services to the container.
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<DatabaseManagement>();
            services.AddScoped<ContactService>();


            return services;
        }
    }
}
