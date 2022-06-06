using Microsoft.EntityFrameworkCore;
using cs_app.Data.Models;

namespace cs_app.Data
{
    public class EducationContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Avia> Avias { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        //это метод, вызываемый при инициализации экземпляра класса EducationContext,
        //который отвечает за первичное подключение к БД и ее настройку.
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=education;Username=postgres;Password=root;Port=5434")
                .UseSnakeCaseNamingConvention()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())).EnableSensitiveDataLogging();
        }

        //метод определения параметров при работе с сущностями и таблицами в БД
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Avia>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Passenger>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Booking>().HasMany(au => au.Avias).WithMany(af => af.Bookings);
            modelBuilder.Entity<Passenger>().HasMany(ar => ar.Bookings);
        }
    }
}