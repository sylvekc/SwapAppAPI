namespace SwapApp
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
