namespace SvwDesign.HttpClientWrapper
{
    public class  HttpClientApiOptions
    {
        public const string ConfigSection = "RestApi";
        public string BaseAddress { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
