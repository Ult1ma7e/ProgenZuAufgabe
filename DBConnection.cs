using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Datenhaltung
{
    public class AppDbContext : DbContext
    {
        public DbSet<Periode> Periode { get; set; }
        public DbSet<Einheiten> Einheiten { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Indizes> Indizes { get; set; }
        public DbSet<Processingdaten> Processingdaten { get; set; }

        private string _dbPath;

        public AppDbContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // string dbPath = @"C:\Users\dorin\Desktop\ProgenZulassungsAufagabe\Project\Bank.sqlite";
            
            // if (!File.Exists(dbPath))
            // {
            //     System.Console.WriteLine($"Error: The DB file: '{dbPath}' was not found.");
            // }
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }
    }
}


