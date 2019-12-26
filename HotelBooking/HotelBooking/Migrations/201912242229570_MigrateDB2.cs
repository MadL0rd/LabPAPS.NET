namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Hotels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rooms", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Categories", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Hotels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Rooms", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Categories", "ApplicationUser_Id");
            DropColumn("dbo.Hotels", "ApplicationUser_Id");
            DropColumn("dbo.Rooms", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Hotels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Categories", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rooms", "ApplicationUser_Id");
            CreateIndex("dbo.Hotels", "ApplicationUser_Id");
            CreateIndex("dbo.Categories", "ApplicationUser_Id");
            AddForeignKey("dbo.Rooms", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Hotels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Categories", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
