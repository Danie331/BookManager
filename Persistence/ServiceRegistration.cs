using Microsoft.Extensions.DependencyInjection;
using Persistence.Automapper;
using Persistence.Contract;
using System.Reflection;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IBookDataService, BookDataService>();

            services.AddAutoMapper(new Assembly[] { Assembly.GetAssembly(typeof(EntityMappingProfile)) });
        }
    }
}