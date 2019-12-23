namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "NumberOfBeds");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "NumberOfBeds", c => c.Int(nullable: false));
        }
    }
}
