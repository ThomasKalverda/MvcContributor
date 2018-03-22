namespace MvcContributor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contributors", "Name", c => c.String(maxLength: 60));
            AlterColumn("dbo.Contributors", "BirthPlace", c => c.String(maxLength: 60));
            AlterColumn("dbo.Contributors", "Biography", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contributors", "Biography", c => c.String());
            AlterColumn("dbo.Contributors", "BirthPlace", c => c.String());
            AlterColumn("dbo.Contributors", "Name", c => c.String());
        }
    }
}
