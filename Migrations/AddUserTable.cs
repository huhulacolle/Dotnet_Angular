using FluentMigrator;

namespace Dotnet_Angular.Migrations
{
    [Migration(1)]
    public class AddUserTable : Migration
    {
        public override void Down()
        {
            Delete.Table("USER");
        }

        public override void Up()
        {
            Create.Table("USER")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("email").AsString().Unique()
                .WithColumn("password").AsString();
        }
    }
}
