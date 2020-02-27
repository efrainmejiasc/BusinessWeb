namespace BusinessWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INIT : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserApi", "Company");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserApi", "Company", c => c.String());
        }
    }
}
