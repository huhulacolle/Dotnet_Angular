namespace Dotnet_Angular.Interfaces.Repositories
{
    public interface IPostsRepository
    {
        public Task<IEnumerable<Posts>> GetPostsAsync();
        public Task PostPostsAsync(Posts posts);
        public Task PostPostsAsyncDict(string title, string content);

    }
}
