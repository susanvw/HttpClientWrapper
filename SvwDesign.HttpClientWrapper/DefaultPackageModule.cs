using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace SvwDesign.HttpClientWrapper
{
    public static class DefaultPackageModule
    {
        public static void AddHttpClientWrapperModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var section = configuration.GetSection(ServiceBaseOptions.ServiceBase);
            services.Configure<ServiceBaseOptions>(section);

            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
        }
    }
}
