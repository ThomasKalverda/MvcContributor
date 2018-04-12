namespace MvcContributor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class birthname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contributors", "BirthName", c => c.String());
            DropColumn("dbo.Contributors", "ImageURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contributors", "ImageURL", c => c.String());
            DropColumn("dbo.Contributors", "BirthName");
        }
    }
}
