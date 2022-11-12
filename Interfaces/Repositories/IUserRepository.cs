namespace Dotnet_Angular.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<string?> Login(User user);
        public Task Register(User user);
    }
}
