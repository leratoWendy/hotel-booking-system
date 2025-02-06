namespace Pocos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pocos.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Pocos.Model1 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.guests.AddOrUpdate(x => x.GuestId,
             new Guest() { GuestId = 1, FirstName = "John", LastName = "Smith", Username = "JohnSmith", Email = "John@gmail.com", Phone = "0795794678" },
             new Guest() { GuestId = 2, FirstName = "Lerato", LastName = "Phetla", Username = "LeratoPhetla", Email = "Lerato@gmail.com", Phone = "0765535952" },
             new Guest() { GuestId = 3, FirstName = "Wendy", LastName = "Williams", Username = "WendyWilliams", Email = "Wendy@gmail.com", Phone = "0762456789" },
             new Guest() { GuestId = 4, FirstName = "Johny", LastName = "Martins", Username = "JohnyMartins", Email = "Johny@gmail.com", Phone = "0845676778" },
             new Guest() { GuestId = 5, FirstName = "James", LastName = "Lopez", Username = "JamesLopez", Email = "James@gmail.com", Phone = "0725678956" },
             new Guest() { GuestId = 6, FirstName = "Emily", LastName = "Brown", Username = "EmilyBrown", Email = "Emily@gmail.com", Phone = "0795565951" },
             new Guest() { GuestId = 7, FirstName = "Mia", LastName = "Steven", Username = "MiaSteven", Email = "Mia@gmail.com", Phone = "0756809951" },
             new Guest() { GuestId = 8, FirstName = "Johannes", LastName = "Peterson", Username = "JohannesPeterson", Email = "Johannes@gmail.com", Phone = "0896434567" },
             new Guest() { GuestId = 9, FirstName = "Amanda", LastName = "Walker", Username = "AmandaWalker", Email = "Amanda@gmail.com", Phone = "0756979443" },
             new Guest() { GuestId = 10, FirstName = "Rose", LastName = "Taylor", Username = "RoseTaylor", Email = "Rose@gmail.com", Phone = "0825678651" }
            );

            context.rooms.AddOrUpdate(x => x.RoomId,
                new Room() {RoomId = 100, RoomNumber = 5, RoomType = "DELUXE", NightPrice = 1000 },
                new Room() { RoomId = 101, RoomNumber = 3, RoomType = "STANDARD", NightPrice = 700 },
                new Room() { RoomId = 102, RoomNumber = 2, RoomType = "SUITE", NightPrice = 1500 },
                new Room() { RoomId = 103, RoomNumber = 6, RoomType = "EXECUTIVE", NightPrice = 2000 },
                new Room() { RoomId = 104, RoomNumber = 7, RoomType = "ECONOMY", NightPrice = 500 },
                new Room() { RoomId = 105, RoomNumber = 8, RoomType = "PENTHOUSE", NightPrice = 3000 },
                new Room() { RoomId = 106, RoomNumber = 9, RoomType = "DELUXE", NightPrice = 1000 },
                new Room() { RoomId = 107, RoomNumber = 4, RoomType = "SUITE", NightPrice = 1500},
                new Room() { RoomId = 108, RoomNumber = 1, RoomType = "DELUXE", NightPrice = 1000 }


            );

            context.bookings.AddOrUpdate(x => x.BookingId,
            new Booking() { BookingId = 001, GuestId = 7, RoomId = 100 , CheckInDate =  DateTime.Today.AddHours(09).AddMinutes(30),  CheckOutDate = DateTime.Today.AddDays(2).AddHours(10).AddMinutes(00), TotalPrice = 2000.00 },
            new Booking() { BookingId = 002, GuestId = 8, RoomId = 102, CheckInDate = DateTime.Today.AddHours(10).AddMinutes(30), CheckOutDate = DateTime.Today.AddDays(2).AddHours(10).AddMinutes(00), TotalPrice = 3000.00 },
            new Booking() { BookingId = 003, GuestId = 4, RoomId = 103, CheckInDate = DateTime.Today.AddHours(11).AddMinutes(0), CheckOutDate = DateTime.Today.AddDays(3).AddHours(10).AddMinutes(00), TotalPrice = 4000.00 },
            new Booking() { BookingId = 004, GuestId = 9, RoomId = 104, CheckInDate = DateTime.Today.AddHours(12).AddMinutes(30), CheckOutDate = DateTime.Today.AddDays(2).AddHours(10).AddMinutes(20), TotalPrice = 6000.00 },
            new Booking() { BookingId = 005, GuestId = 5, RoomId = 105, CheckInDate = DateTime.Today.AddHours(06).AddMinutes(0), CheckOutDate = DateTime.Today.AddDays(3).AddHours(10).AddMinutes(00), TotalPrice = 3000.00 },
            new Booking() { BookingId = 006, GuestId = 3, RoomId = 106, CheckInDate = DateTime.Today.AddHours(04).AddMinutes(30), CheckOutDate = DateTime.Today.AddDays(3).AddHours(10).AddMinutes(30), TotalPrice = 7000.00 },
            new Booking() { BookingId = 007, GuestId = 2, RoomId = 107, CheckInDate = DateTime.Today.AddHours(09).AddMinutes(0), CheckOutDate = DateTime.Today.AddDays(2).AddHours(10).AddMinutes(00), TotalPrice = 4500.00 },
            new Booking() { BookingId = 008, GuestId = 1, RoomId = 101, CheckInDate = DateTime.Today.AddHours(05).AddMinutes(30), CheckOutDate = DateTime.Today.AddDays(2).AddHours(10).AddMinutes(30), TotalPrice = 6000.00 }


            );

            context.booking_status.AddOrUpdate(x => x.StatusId,
                new BookingStatus() {StatusId = 111, StatusName = "Pending" },
                new BookingStatus() { StatusId = 222, StatusName = "Confirmed" },
                new BookingStatus() { StatusId = 333, StatusName = "Cancelled" }
              

                );

            context.payments.AddOrUpdate(x => x.PaymentId,
                new Payment() {PaymentId = 01, GuestId = 7, BookingId = 001, PaymentDate = DateTime.Today.AddHours(09).AddMinutes(30), Amount = 2000.00 },
                new Payment() { PaymentId = 02, GuestId = 8, BookingId = 002, PaymentDate = DateTime.Today.AddHours(09).AddMinutes(30), Amount = 3000.00 },
                new Payment() { PaymentId = 03, GuestId = 4, BookingId = 003, PaymentDate = DateTime.Today.AddHours(09).AddMinutes(30), Amount = 4000.00 },
                new Payment() { PaymentId = 04, GuestId = 9, BookingId = 004, PaymentDate = DateTime.Today.AddHours(09).AddMinutes(30), Amount = 6000.00 },
                new Payment() { PaymentId = 05, GuestId = 5, BookingId = 005, PaymentDate = DateTime.Today.AddHours(09).AddMinutes(30), Amount = 3000.00 },
                new Payment() { PaymentId = 06, GuestId = 3, BookingId = 006, PaymentDate = DateTime.Today.AddHours(09).AddMinutes(30), Amount = 7000.00 }
                );


           





        }
    }
}
