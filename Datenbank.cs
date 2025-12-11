using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;

// erstellt von fapa1019

namespace EasyBankingBackOffice.Datenhaltung
{

    // Ich habe die Klasse „AppDbContext” in Datenbank.cs eingefügt, da ich nicht wusste,
    // ob ich noch eine weitere Klasse verwenden konnte
    // Ich habe Entity Framework Core mit Bank.sqlite verwendet.
    public class AppDbContext : DbContext
    {
        public DbSet<Periode> Periode { get; set; }
        public DbSet<Einheiten> Einheiten { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Indizes> Indizes { get; set; }
        public DbSet<Processingdaten> Processingdaten { get; set; }

        private string _dbPath;

        // Constructor recieves the path to the database
        public AppDbContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        // Database connection using SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }
    }


    public static class Datenbank
    {
        // Private static field to store data
        private static bool _istGeladen = false;
        private static List<int> _periodenIDs = new List<int>();
        private static Processingdaten _processingdaten;
        private static string _dbPath;


        /// <summary>
        /// Returns the database loading status
        /// </summary>
        public static bool IstGeladen
        {
            get
            {
                return _istGeladen;
            }
        }

        /// <summary>
        /// Returns a copy(list) of the loaded period IDs
        /// </summary>
        public static List<int> PeriodenIDs
        {
            get
            {
                if (!_istGeladen)
                {
                    throw new Exception("ERROR: Database did not load");
                }
                return _periodenIDs.ToList();
            }
        }

        /// <summary>
        /// Returns the single loaded Processingdaten object
        /// </summary>
        public static Processingdaten Processingdaten
        {
            get
            {
                if (!_istGeladen)
                {
                    throw new Exception("ERROR: Database did not load");
                }
                return _processingdaten;
            }
        }

        /// <summary>
        /// Loads and initializes the database.
        /// </summary>
        /// <param name="dbPath">The path to the database</param>
        /// <returns>True if loading the Database  was successful.</returns>
        /// <exception cref="Exception">Thrown if the path is invalid, the file doesn't exist, 
        /// the Processingdaten table has more or less than one row, or the Periode table is empty.</exception>
        public static bool DatenbankAuslesen(string dbPath)
        {
            if (string.IsNullOrWhiteSpace(dbPath))
            {
                throw new Exception("ERROR: Path to database can not be empty");
            }

            if (!File.Exists(dbPath))
            {
                throw new Exception("ERROR: There is no such Path to a database");
            }

            // Stores the path globally
            _dbPath = dbPath;

            using (var context = new AppDbContext(dbPath))
            {
                context.Database.OpenConnection();


                // Reads the Processingdaten and checks if one exists
                var allProcesData = context.Processingdaten.ToList();

                if (allProcesData.Count == 1)
                {
                    _processingdaten = allProcesData.Single();
                }
                else
                {
                    throw new Exception($"ERROR: \"Processingdaten\" table contains {allProcesData.Count} rows, but only one is allowed in project if specified.");
                }


                // Reads all IDs from the Periode table
                List<int> periodIDsList = new List<int>();

                foreach (var periodeObj in context.Periode)
                {
                    int currentId = periodeObj.ID;
                    periodIDsList.Add(currentId);
                }

                periodIDsList.Sort();
                _periodenIDs = periodIDsList;

                if (!_periodenIDs.Any())
                {
                    throw new Exception("ERROR: \"Periode\" table is empty.");
                }

                _istGeladen = true;
                return true;
            }
        }

        /// <summary>
        /// Method used to retrieve a "Periode" object by its ID.
        /// </summary>
        /// <param name="periodeID">The ID of the period to retrieve.</param>
        /// <returns>The requested "Periode" object.</returns>
        /// <exception cref="Exception">Thrown if the database is not loaded or the ID is not found.</exception>
        public static Periode Periode(int periodeID)
        {
            if (!IstGeladen) throw new Exception("ERROR: Database did not load");

            using (var context = new AppDbContext(_dbPath))
            {
                Periode? periode = null;

                foreach (var item in context.Periode)
                {
                    if (item.ID == periodeID)
                    {
                        periode = item;
                        break;
                    }
                }

                if (periode == null)
                {
                    throw new Exception($"ERROR: \"Periode\" with PeriodeID {periodeID} not found.");
                }

                return periode;
            }
        }

        /// <summary>
        /// Method to retrieve an "Einheiten" object by the Period ID.
        /// </summary>
        /// <param name="periodeID">The ID of the period for which to retrieve the "Einheiten" data.</param>
        /// <returns>The requested "Einheiten" object.</returns>
        /// <exception cref="Exception">Thrown if the database is not loaded or the PeriodeID is not found.</exception>
        public static Einheiten Einheiten(int periodeID)
        {
            if (!IstGeladen)
            {
                throw new Exception("ERROR: Database did not load.");
            }

            using (var context = new AppDbContext(_dbPath))
            {
                Einheiten? einheit = null;

                foreach (var item in context.Einheiten)
                {
                    if (item.PeriodeID == periodeID)
                    {
                        einheit = item;
                        break;
                    }
                }

                if (einheit == null)
                {
                    throw new Exception($"ERROR: \"Einheiten\" with PeriodeID {periodeID} not found.");
                }

                return einheit;
            }
        }


        /// <summary>
        /// Method to retrieve an "Mitarbeiter" object by the Period ID.
        /// </summary>
        /// <param name="periodeID">The ID of the period for which to retrieve the "Mitarbeiter" data.</param>
        /// <returns>The requested "Mitarbeiter" object.</returns>
        /// <exception cref="Exception">Thrown if the database is not loaded or the PeriodeID is not found.</exception>
        public static Mitarbeiter Mitarbeiter(int periodeID)
        {
            if (!IstGeladen) throw new Exception("ERROR: Database did not load");

            using (var context = new AppDbContext(_dbPath))
            {
                Mitarbeiter? mitarbeiter = null;

                foreach (var item in context.Mitarbeiter)
                {
                    if (item.PeriodeID == periodeID)
                    {
                        mitarbeiter = item;
                        break;
                    }
                }

                if (mitarbeiter == null)
                {
                    throw new Exception($"ERROR: \"Mitarbeiter\" with PeriodeID {periodeID} not found.");
                }

                return mitarbeiter;
            }
        }


        /// <summary>
        /// Method to retrieve an "Indizes" object by the Period ID.
        /// </summary>
        /// <param name="periodeID">The ID of the period for which to retrieve the "Indizes" data.</param>
        /// <returns>The requested "Indizes" object.</returns>
        /// <exception cref="Exception">Thrown if the database is not loaded or the PeriodeID is not found.</exception>
        public static Indizes Indizes(int periodeID)
        {
            if (!IstGeladen) throw new Exception("ERROR: Database did not load");

            using (var context = new AppDbContext(_dbPath))
            {
                Indizes? indizes = null;

                foreach (var item in context.Indizes)
                {
                    if (item.PeriodeID == periodeID)
                    {
                        indizes = item;
                        break;
                    }
                }

                if (indizes == null)
                {
                    throw new Exception($"ERROR: \"Indizes\" with PeriodeID {periodeID} not found.");
                }

                return indizes;
            }
        }
    }
}



