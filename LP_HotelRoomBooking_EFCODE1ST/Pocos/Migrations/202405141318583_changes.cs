namespace Pocos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.booking_status",
                c => new
                    {
                        status_id = c.Int(nullable: false, identity: true),
                        status_name = c.String(),
                    })
                .PrimaryKey(t => t.status_id);
            
            CreateTable(
                "dbo.bookings",
                c => new
                    {
                        booking_id = c.Int(nullable: false, identity: true),
                        guest_id = c.Int(nullable: false),
                        room_id = c.Int(nullable: false),
                        checkin_date = c.DateTime(nullable: false),
                        checkout_date = c.DateTime(nullable: false),
                        total_price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.booking_id)
                .ForeignKey("dbo.guests", t => t.guest_id)
                .ForeignKey("dbo.rooms", t => t.room_id)
                .Index(t => t.guest_id)
                .Index(t => t.room_id);
            
            CreateTable(
                "dbo.guests",
                c => new
                    {
                        guest_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(nullable: false, maxLength: 50),
                        last_name = c.String(nullable: false, maxLength: 100),
                        user_name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.guest_id);
            
            CreateTable(
                "dbo.payments",
                c => new
                    {
                        payment_id = c.Int(nullable: false, identity: true),
                        guest_id = c.Int(nullable: false),
                        booking_id = c.Int(nullable: false),
                        payment_date = c.DateTime(nullable: false),
                        amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.payment_id)
                .ForeignKey("dbo.bookings", t => t.booking_id, cascadeDelete: true)
                .ForeignKey("dbo.guests", t => t.guest_id)
                .Index(t => t.guest_id)
                .Index(t => t.booking_id);
            
            CreateTable(
                "dbo.rooms",
                c => new
                    {
                        room_id = c.Int(nullable: false, identity: true),
                        room_number = c.Int(nullable: false),
                        room_type = c.String(),
                        night_price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.room_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.bookings", "room_id", "dbo.rooms");
            DropForeignKey("dbo.bookings", "guest_id", "dbo.guests");
            DropForeignKey("dbo.payments", "guest_id", "dbo.guests");
            DropForeignKey("dbo.payments", "booking_id", "dbo.bookings");
            DropIndex("dbo.payments", new[] { "booking_id" });
            DropIndex("dbo.payments", new[] { "guest_id" });
            DropIndex("dbo.bookings", new[] { "room_id" });
            DropIndex("dbo.bookings", new[] { "guest_id" });
            DropTable("dbo.rooms");
            DropTable("dbo.payments");
            DropTable("dbo.guests");
            DropTable("dbo.bookings");
            DropTable("dbo.booking_status");
        }
    }
}
