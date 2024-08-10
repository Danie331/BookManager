using BooksService;
using ConfigurationDataProvider;
using ConfigurationDataProvider.Contract;
using ConfigurationDataProvider.Models;
using Microsoft.Extensions.Options;

namespace BookManager
{
    public static class ServiceRegistration
    {
        public static void Register(IServiceCollection services)
        {
            // This is a dirty (but necessary) workaround if we need to inject a config provider in a multi-tier architecture that needs access to the webroot
            services.AddSingleton<IBookDataStoreConfiguration>(sp =>
            {
                var appSettings = (IOptions<BookDataStoreSettings>)sp.GetService(typeof(IOptions<BookDataStoreSettings>));
                var hostEnvironment = sp.GetRequiredService<IWebHostEnvironment>();
                var dataStoreFilePath = Path.Combine(hostEnvironment.WebRootPath, appSettings.Value.DataStoreFilename);

                return new BookDataStoreConfiguration(dataStoreFilePath);
            });
            services.AddTransient<IBookService, BookService>();            

            Persistence.ServiceRegistration.Register(services);
        }
    }
}
