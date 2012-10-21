namespace ZeroDay.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        ThumbnailUrl = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        PreviousDayUrl = c.String(),
                        DownloadUrl = c.String(),
                        Photographer = c.String(),
                        PhotographerUrl = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Thread = c.String(),
                        Level = c.String(),
                        Logger = c.String(),
                        Message = c.String(),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "Category_Id" });
            DropForeignKey("dbo.Images", "Category_Id", "dbo.Categories");
            DropTable("dbo.Log");
            DropTable("dbo.Categories");
            DropTable("dbo.Images");
        }
    }
}
