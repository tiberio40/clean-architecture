namespace Core.DTOs.Security.User
{
    public class TokenDto
    {
        public string Token { get; set; } = null!;
        public double Expiration { get; set; }
        public bool ResetPassword { get; set; }
    }
}
