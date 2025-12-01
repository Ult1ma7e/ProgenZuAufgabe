using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Datenhaltung
{
    // A static class for encapsulated, centralized database access.
    public static class Datenbank
    {
        private static bool _istGeladen = false;
        private static List<int> _periodenIDs = new List<int>();
        private static Processingdaten _processingdaten;
        private static string _dbPath;

        public static bool IstGeladen
        {
            get
            {
                return _istGeladen;
            }
            
            private set
            {
                _istGeladen = value;
            }
        }


        public static List<int> PeriodenIDs
        {
            get
            {
                if (!_istGeladen)
                {
                    throw new Exception("Database did not load");
                }
                return _periodenIDs.ToList();
            }
            
            private set
            {
                _periodenIDs = value;
            }
        }

        public static Processingdaten Processingdaten
        {
            get
            {
                if (!_istGeladen)
                {
                    throw new Exception("Database did not load");
                }
                return _processingdaten;
            }
            
            private set
            {
                _processingdaten = value;
            }
        }
        
        public static bool DatenbankAuslesen(string dbPath)
        {
            if (string.IsNullOrWhiteSpace(dbPath))
            {
                throw new ArgumentException("Path can not be empty");
            }
            _dbPath = dbPath;
            
            using (var context = new AppDbContext(dbPath))
            {
                context.Database.OpenConnection();

                var allProcesData = context.Processingdaten.ToList();

                if (allProcesData.Count == 1)
                {
                    Processingdaten = allProcesData.Single();
                }
                else
                {
                    throw new Exception($"ERROR: Processingdaten table contains {allProcesData.Count} rows, but only one is allowed as per project specification.");
                }

                List<int> periodIDsList = new List<int>();

                foreach (var periodeObj in context.Periode)
                {
                    int currentId = periodeObj.ID;
                    periodIDsList.Add(currentId);
                }

                periodIDsList.Sort();
                PeriodenIDs = periodIDsList;
                    
                if (!_periodenIDs.Any()) // To check if any matching periods were found
                {
                    throw new Exception("Periode table is empty.");
                }

                IstGeladen = true;
                return true;
            } 
        }

        public static Periode Periode(int periodeID)
        {
            if (!IstGeladen) throw new Exception("Database did not load");
            
            using (var context = new AppDbContext(_dbPath))
            {
                var result = context.Periode.FirstOrDefault(p => p.ID == periodeID);
                if (result == null) throw new Exception($"Periode with ID {periodeID} not found.");
                return result;
            }
        }

        public static Einheiten Einheiten(int periodeID)
        {
            if (!IstGeladen) throw new Exception("Database did not load");

            using (var context = new AppDbContext(_dbPath))
            {
                var result = context.Einheiten.FirstOrDefault(e => e.PeriodeID == periodeID);
                if (result == null) throw new Exception($"Einheiten with PeriodeID {periodeID} not found.");
                return result;
            }
        }

        public static Mitarbeiter Mitarbeiter(int periodeID)
        {
            if (!IstGeladen) throw new Exception("Database did not load");

            using (var context = new AppDbContext(_dbPath))
            {
                var result = context.Mitarbeiter.FirstOrDefault(m => m.PeriodeID == periodeID);
                if (result == null) throw new Exception($"Mitarbeiter with PeriodeID {periodeID} not found.");
                return result;
            }
        }

        public static Indizes Indizes(int periodeID)
        {
            if (!IstGeladen) throw new Exception("Database did not load");

            using (var context = new AppDbContext(_dbPath))
            {
                var result = context.Indizes.FirstOrDefault(i => i.PeriodeID == periodeID);
                if (result == null) throw new Exception($"Indizes with PeriodeID {periodeID} not found.");
                return result;
            }
        }
    }
}



