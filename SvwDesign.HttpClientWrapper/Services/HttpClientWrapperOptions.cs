
namespace SvwDesign.HttpClientWrapper.Services
{
    public class  HttpClientWrapperOptions
    {
        public const string ConfigSection = "WebApi";
        public string BaseAddress { get; set; }
        public string MediaType { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
