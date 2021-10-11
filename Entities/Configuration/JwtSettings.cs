namespace Entities.Configuration
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string validIssuer { get; set; }
        public string validAudience { get; set; }
        public int expires { get; set; }
    }
}