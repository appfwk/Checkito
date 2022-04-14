namespace Checkito.Apis.Models
{
    public class AuthenticationDefinition
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Url { get; set; }
        public string? Token { get; set; }
        public AuthenticationType Type { get; set; }
    }
}
