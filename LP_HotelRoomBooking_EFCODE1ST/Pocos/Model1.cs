using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Pocos
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }


        public Model1(DbConnection connection) : base(connection, true)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>().Property(t => t.FirstName).IsRequired();
            modelBuilder.Entity<Guest>().Property(t => t.LastName).IsRequired();
            modelBuilder.Entity<Guest>().Property(t => t.Email).IsRequired();
            modelBuilder.Entity<Guest>().Property(t => t.Phone).IsRequired();
            modelBuilder.Entity<Guest>().Property(t => t.Username).IsRequired();
            modelBuilder.Entity<Guest>().Property(t => t.FirstName).HasMaxLength(50);
            modelBuilder.Entity<Guest>().Property(t => t.LastName).HasMaxLength(100);



            // Configure cascade actions for foreign key constraints
            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId)
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<Payment>()
                .HasRequired(p => p.Guest)
                .WithMany(g => g.Payments)
                .HasForeignKey(p => p.GuestId)
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .WillCascadeOnDelete(false); 


        }

        public virtual DbSet<Guest> guests { get; set; }
        public virtual DbSet<Booking> bookings { get; set; }
        public virtual DbSet<Room> rooms { get; set; }
        public virtual DbSet<Payment> payments { get; set; }
        public virtual DbSet<BookingStatus> booking_status { get; set; }
       



    }
}
