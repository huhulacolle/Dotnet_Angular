namespace Dotnet_Angular.Models
{
    public class Posts
    {
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
