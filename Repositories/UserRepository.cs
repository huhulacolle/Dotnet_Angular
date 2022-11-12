using Dotnet_Angular.Services;

namespace Dotnet_Angular.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public UserRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task<string?> Login(User user)
        {
            string sql = "SELECT email, password FROM user WHERE email = @email";

            try
            {
                using var connec = defaultSqlConnectionFactory.Create();
                var result = await connec.QueryFirstAsync<User>(sql, user);

                if (BCrypt.Net.BCrypt.Verify(user.Password, result.Password))
                {
                    return user.Email;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            string sql = "INSERT INTO user (email, password) VALUES (@email, @password)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, user);
        }
    }
}
