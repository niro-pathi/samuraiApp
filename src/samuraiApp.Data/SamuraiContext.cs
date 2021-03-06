
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using samuraiApp.Domain;

namespace samuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public DbSet<Samurai> Samurais {get; set;}
        public DbSet<Quote> Quotes{get; set;}
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "server=nppoc-dev-as-sql.database.windows.net;database=SamuraiAppData;trusted_connection=true;integrated security=false;User ID=npadmin;Password=N1rosh@na;")
                .LogTo(Console.Write,new[] { DbLoggerCategory.Database.Command.Name},
                        LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Battles)
                .WithMany(b => b.Samurais)
                .UsingEntity<BattleSamurai>
                (bs => bs.HasOne<Battle>().WithMany(),
                 bs => bs.HasOne<Samurai>().WithMany())
                .Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getDate()");
        }
  
    }
}