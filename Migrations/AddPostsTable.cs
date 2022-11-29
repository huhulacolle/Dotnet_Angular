using FluentMigrator;

namespace Dotnet_Angular.Migrations
{
    [Migration(2)]
    public class AddPostsTable : Migration
    {
        public override void Down()
        {
            Delete.Table("POSTS");
        }

        public override void Up()
        {
            Create.Table("POSTS")
                .WithColumn("id2").AsInt32().PrimaryKey().Identity()
                .WithColumn("title").AsString()
                .WithColumn("content").AsString()
                .WithColumn("date").AsDate();
        }
    }
}
