
using Microsoft.EntityFrameworkCore;
using samuraiApp.Domain;

namespace samuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public DbSet<Samurai> Samurais {get; set;}
        public DbSet<Quote> Quotes{get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=nppoc-dev-as-sql.database.windows.net;database=SamuraiAppData;trusted_connection=true;integrated security=false;User ID={user};Password={password};");
            


        }
  
    }
}