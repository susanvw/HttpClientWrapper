namespace SvwDesign.HttpClientWrapper
{
    public class HttpClientWrapperOptions
    {
        public const string ConfigSection = "HttpClientWrapperOptions";
        public string BaseAddress { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
