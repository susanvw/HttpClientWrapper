using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace SvwDesign.HttpClientWrapper
{
    public static class DependencyInjection
    {
        public static void AddHttpClientWrapperModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var section = configuration.GetSection(HttpClientWrapperOptions.ConfigSection);
            services.Configure<HttpClientWrapperOptions>(section);

            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
        }
    }
}
