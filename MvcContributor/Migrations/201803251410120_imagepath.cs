namespace MvcContributor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagepath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contributors", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contributors", "ImagePath");
        }
    }
}
