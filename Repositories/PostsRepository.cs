using System.Collections;
using System.Collections.Generic;

namespace Dotnet_Angular.Repositories
{
    public class PostsRepository : IPostsRepository
    {

        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public PostsRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task<IEnumerable<Posts>> GetPostsAsync()
        {
            string sql = "SELECT * FROM posts ORDER BY id DESC";

            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<Posts>(sql);
        }

        public async Task PostPostsAsync(Posts posts)
        {
            string sql = "INSERT INTO posts (title, content) VALUES (@title, @content)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, posts);
        }

        public async Task PostPostsAsyncDict(string title, string content)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@title", title },
                { "@content", content }
            };
            var parameters = new DynamicParameters(dictionary);

            string sql = "INSERT INTO posts (title, content) VALUES (@title, @content)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, parameters);
        }
    }
}
