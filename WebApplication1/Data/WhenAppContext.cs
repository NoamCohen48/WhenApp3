
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using whenAppModel.Models;

namespace WhenUp
{
    public class WhenAppContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=WhenUpDB;user=root;password=toor";
        //private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=WhenUpDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
            //optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            //modelBuilder.Entity<User>().HasKey(e => e.Username);
            //modelBuilder.Entity<Message>().HasKey(e => e.Id);
            //modelBuilder.Entity<Rating>().HasKey(e => e.Id);
            //modelBuilder.Entity<Chat>().HasKey(e => e.Person1);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Rating> Ratings { get; set; }


    }
}