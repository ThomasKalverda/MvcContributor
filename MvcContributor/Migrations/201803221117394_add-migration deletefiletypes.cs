namespace MvcContributor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationdeletefiletypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilePaths", "ContributorID", "dbo.Contributors");
            DropIndex("dbo.FilePaths", new[] { "ContributorID" });
            DropTable("dbo.FilePaths");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FilePaths",
                c => new
                    {
                        FilePathId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        ContributorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilePathId);
            
            CreateIndex("dbo.FilePaths", "ContributorID");
            AddForeignKey("dbo.FilePaths", "ContributorID", "dbo.Contributors", "ID", cascadeDelete: true);
        }
    }
}
