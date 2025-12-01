// using Datenhaltung;
// using System;
// using System.Linq;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         Console.WriteLine("--- Back Office Report: Data Layer Initialization ---");
        
//         // 1. Attempt to initialize the Data Layer by calling the static DatenbankAuslesen method.
//         bool success = Datenbank.DatenbankAuslesen();

//         if (success)
//         {
//             Console.WriteLine("\n✅ Success: Data Layer initialized and static data loaded.");
            
//             // 2. Display the cached, period-independent data
//             Console.WriteLine("-----------------------------------------------------");
//             Console.WriteLine("### 📊 Cached Data (Processing Factors) ###");
//             Console.WriteLine($"Processingdata ID: {Datenbank.Processingdaten.ID}");
//             // NOTE: You'd typically print the conversion factor values here for full testing:
//             // Console.WriteLine($"AktiveKredite Factor: {Datenbank.Processingdaten.AktiveKredite}");

//             // 3. Display the cached Period IDs
//             Console.WriteLine("\n### 🕰️ Available Periods ###");
//             Console.WriteLine($"Total Periods Found: {Datenbank.PeriodenIDs.Count}");
//             Console.WriteLine($"IDs (Sorted): {string.Join(", ", Datenbank.PeriodenIDs)}");
//             Console.WriteLine("-----------------------------------------------------");

//             // 4. Test retrieval of period-specific data (e.g., for the first available period)
//             if (Datenbank.PeriodenIDs.Any())
//             {
//                 int firstPeriodId = Datenbank.PeriodenIDs.First();
                
//                 // Get the raw data objects for the first period
//                 var units = Datenbank.Einheiten(firstPeriodId);
//                 var staff = Datenbank.Mitarbeiter(firstPeriodId);
//                 var indices = Datenbank.Indizes(firstPeriodId);
                
//                 Console.WriteLine($"### 🔍 Detail Test: Period ID {firstPeriodId} ###");

//                 if (units != null)
//                 {
//                     Console.WriteLine($"Einheiten (Activity): Active Credits = {units.AktiveKredite}");
//                 }
//                 if (staff != null)
//                 {
//                     Console.WriteLine($"Mitarbeiter (Staffing): Actual BO Staff = {staff.Backofficemitarbeiter}");
//                 }
//                 if (indices != null)
//                 {
//                     Console.WriteLine($"Indizes (Correction): IT Index = {indices.ITIndex}");
//                 }
//             }
//         }
//         else
//         {
//             // The DatenbankAuslesen method already printed the error message to the console.
//             Console.WriteLine("\n❌ Failure: Data Layer initialization failed. Check console output for details.");
//         }

//         Console.WriteLine("\n--- Verification Complete ---");
//         Console.ReadKey(); // Wait for user input to close the console (helpful for testing)
//     }
// }