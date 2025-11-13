using Microsoft.EntityFrameworkCore;
using vinmomo.Models;
using System.IO;

namespace vinmomo.Data
{
    public class AnnuaireContext : DbContext
    {
        public DbSet<Salarie> Salaries { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Site> Sites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Force l'utilisation du fichier SQLite à la racine du projet
                //var basePath = Directory.GetCurrentDirectory();
                //var dbPath = Path.Combine(basePath, "vinmomo.db");
                //optionsBuilder.UseSqlite($"Data Source={dbPath}");

                string connectionString = "Server=localhost;Database=vinmomo;User=root;Password=mysql;";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
