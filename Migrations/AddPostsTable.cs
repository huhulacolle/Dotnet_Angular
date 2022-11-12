using FluentMigrator;

namespace Dotnet_Angular.Migrations
{
    [Migration(2)]
    public class AddPostsTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("POSTS")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("title").AsString()
                .WithColumn("content").AsString()
                .WithColumn("date").AsDate();
        }
    }
}
