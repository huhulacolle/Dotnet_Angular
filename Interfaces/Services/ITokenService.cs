namespace Dotnet_Angular.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(string email, string role);
    }
}
