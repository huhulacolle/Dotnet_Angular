using FluentMigrator;

namespace Dotnet_Angular.Migrations
{
    [Migration(1)]
    public class AddUserTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("USER")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("email").AsString().Unique()
                .WithColumn("password").AsString();
        }
    }
}
