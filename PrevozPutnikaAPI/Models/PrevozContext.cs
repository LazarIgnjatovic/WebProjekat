using Microsoft.EntityFrameworkCore;

namespace PrevozPutnikaAPI.Models
{
    public class PrevozContext : DbContext
    {
        public DbSet<Korisnik> Korisnici {get; set;}
        public DbSet<Relacija> Relacije {get;set;}
        public DbSet<Rezervacija> Rezervacije{get;set;}
        public DbSet<Sediste> Sedista{get;set;}

        public PrevozContext(DbContextOptions options) : base(options)
        {
            
        }  
    }
}