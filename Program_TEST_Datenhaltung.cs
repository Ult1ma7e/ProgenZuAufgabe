// Program_TEST_Datenhaltung.cs (zu Zulassungsaufgabe 25W)
// Test des Namensraums 'Datenhaltung'
// Konsolenanwendung

/*
 * WICHTIG:
 * - Das konstante Feld '_dbname' enthält den Dateinamen der Datenbank.
 * - Das konstante Feld '_location' enthält den Pfad zum Verzeichnis, in dem die Datenbank liegt.
 * Bitte an Ihre Arbeitsumgebung anpassen!
 * 
 */

#nullable disable

#pragma warning disable IDE0079 // Unnötige Unterdrückung entfernen
#pragma warning disable SYSLIB1054 // Verwenden Sie \"LibraryImportAttribute\" anstelle von \"DllImportAttribute\", um P/Invoke-Marshallingcode zur Kompilierzeit zu generieren.
#pragma warning disable IDE0290 // Primären Konstruktor verwenden
#pragma warning disable IDE0090 // "new(...)" verwenden

using System;
using System.Linq;
using System.Reflection;
using static TestSupport.TestSupport;
using System.Runtime.InteropServices;
using Datenhaltung;

namespace TestDatenhaltung.Ablauf
{

    class Program
    {
        //private const string _location = @".\";
        private const string _location = @"C:\Users\dorin\Desktop\ProgenZulassungsAufagabe\Project\";

        private const string _dbname = "Bank.sqlite";
        //private const string _dbname = "Bank.sqlite";

        #region Hilfsklassen und Hilfsmethoden

        // Klasse für eine Zeile der Vorgabetabellen
        // T: Typ der Vorgabe
        private class VorgabeZeile<T>
        {
            public int PeriodeID { get; }
            public T Zeile { get; }

            public VorgabeZeile(int periodeID, T zeile)
            {
                PeriodeID = periodeID;
                Zeile = zeile;
            }

            public void Vergleichen(T vergleichsZeile)
            {
                foreach (PropertyInfo pi in typeof(T).GetProperties())
                {
                    MethodInfo mi = pi.GetGetMethod();
                    Console.Write(pi.Name + ": ");
                    CompareAndPrint(mi.Invoke(Zeile, null), mi.Invoke(vergleichsZeile, null));
                }
            }
        }

        // Delegatentyp für Methoden, die aus der Datenbank Zeilen abrufen
        // T: Typ des Austauschobjekts der Zeile
        // periodenID: Nummer der Periode der Zeile
        // Rückgabe: abgerufene Tabellenzeile
        private delegate T AbgerufeneZeile<T>(int periodenID);

        // Methode zum Abgleich der Vorgaben mit den Werten aus der Datenbank
        // T: Typ des Austauschobjektes
        // vorgaben: Array mit Vorgabezeilen
        // zeileAbrufen: zum Typ korrespondierende Methode zum Abruf einer Zeile aus der Datenbank
        private static void TestDurchführen<T>(VorgabeZeile<T>[] vorgaben, AbgerufeneZeile<T> zeileAbrufen)
        {
            int maxID = -1;
            object dummy;

            Console.WriteLine($"\n- {typeof(T).Name}:\n");
            foreach (VorgabeZeile<T> vorgabeZeile in vorgaben)
            {
                int periodeID = vorgabeZeile.PeriodeID;
                if (periodeID > maxID)
                    maxID = periodeID;
                Console.WriteLine(periodeID.ToString());
                T datenZeile = zeileAbrufen(periodeID);
                vorgabeZeile.Vergleichen(datenZeile);
            }
            ProvokeException(() => dummy = zeileAbrufen(0));
            ProvokeException(() => dummy = zeileAbrufen(maxID + 1));
        }

        // Fabrikmethode für Klasse 'Periode'
        private static Periode ErzeugtePeriode(int id, DateTime beginn, DateTime ende) => new()
        {
            ID = id,
            Beginn = beginn,
            Ende = ende
        };

        // Fabrikmethode für Klasse 'Einheiten'
        private static Einheiten ErzeugteEinheiten(int id,
                                                   int periodeId,
                                                   double aktiveKredite,
                                                   double par30Kredite,
                                                   double spareinlagen,
                                                   double neueKredite,
                                                   double neueEinlagen,
                                                   double transaktionenKapitalmarkt,
                                                   double endbestandKapitalmarkt) => new()
        {
            ID = id,
            PeriodeID = periodeId,
            AktiveKredite = aktiveKredite,
            Par30Kredite = par30Kredite,
            Spareinlagen = spareinlagen,
            NeueKredite = neueKredite,
            NeueEinlagen = neueEinlagen,
            TransaktionenKapitalmarkt = transaktionenKapitalmarkt,
            EndbestandKapitalmarkt = endbestandKapitalmarkt
        };

        // Fabrikmethode für Klasse 'Indizes'
        private static Indizes ErzeugteIndizes(int id, int periodeId, double itIndex, double qualifikationsindex) => new()
        {
            ID = id,
            PeriodeID = periodeId,
            ITIndex = itIndex,
            Qualifikationsindex = qualifikationsindex
        };

        // Fabrikmethode für Klasse 'Mitarbeiter'
        private static Mitarbeiter ErzeugteMitarbeiter(int id, int periodeId, int kreditberater, int anlageberater, int backofficemitarbeiter, int filialen) => new()
        {
            ID = id,
            PeriodeID = periodeId,
            Kreditberater = kreditberater,
            Anlageberater = anlageberater,
            Backofficemitarbeiter = backofficemitarbeiter,
            Filialen = filialen
        };

        // Methoden zum Maximieren des Konsolenfensters
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern nint GetConsoleWindow();
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(nint hWnd, int cmdShow);

        #endregion Hilfsklassen und Hilfsmethoden

        // Einstiegsmethode
        private static void Main()
        {
            // Maximieren des Konsolenfensters
            ShowWindow(GetConsoleWindow(), 3);

            Console.WriteLine("--- Testen der Entitäts-/Transferklassen");

            #region Klasse Periode

            Console.WriteLine($"\n- {nameof(Periode)} -\n");

            // vorgegebene Werte
            int id = ZufallsInt(1, 100);
            DateTime periodeBeginn = new DateTime(ZufallsInt(2000, 2030), ZufallsInt(1, 13), ZufallsInt(1, 29));
            DateTime periodeEnde = new DateTime(ZufallsInt(2000, 2030), ZufallsInt(1, 13), ZufallsInt(1, 29));

            // Objekt anlegen
            Periode periode = ErzeugtePeriode(id, periodeBeginn, periodeEnde);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(periode.ID, id, name: nameof(periode.ID));
            CompareAndPrint(periode.Beginn, periodeBeginn, name: nameof(periode.Beginn));
            CompareAndPrint(periode.Ende, periodeEnde, name: nameof(periode.Ende));

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            TestForNonwriteableProperties<Periode>(3);

            // Test auf überschriebenes 'ToString'
            TestForNewMethod<Periode>("ToString");

            #endregion Klasse Periode

            #region Klasse Processingdaten

            Console.WriteLine($"\n- {nameof(Processingdaten)} -\n");

            // vorgegebene Werte
            id = ZufallsInt(1, 10);
            double processingdatenAktiveKredite = ZufallsDouble(1.0, 10.0);
            double processingdatenPar30Kredite = ZufallsDouble(1.0, 10.0);
            double processingdatenSpareinlagen = ZufallsDouble(1.0, 10.0);
            double processingdatenNeueKredite = ZufallsDouble(1.0, 10.0);
            double processingdatenMitarbeiter = ZufallsDouble(1.0, 10.0);
            double processingdatenFilialen = ZufallsDouble(1.0, 10.0);
            double processingdatenNeueEinlagen = ZufallsDouble(1.0, 10.0);
            double processingdatenTransaktionenKapitalmarkt = ZufallsDouble(1.0, 10.0);
            double processingdatenEndbestandKapitalmarkt = ZufallsDouble(1.0, 10.0);

            // Objekt anlegen
            Processingdaten processingdaten = new Processingdaten
            {
                ID = id,
                AktiveKredite = processingdatenAktiveKredite,
                Par30Kredite = processingdatenPar30Kredite,
                Spareinlagen = processingdatenSpareinlagen,
                NeueKredite = processingdatenNeueKredite,
                Mitarbeiter = processingdatenMitarbeiter,
                Filialen = processingdatenFilialen,
                NeueEinlagen = processingdatenNeueEinlagen,
                TransaktionenKapitalmarkt = processingdatenTransaktionenKapitalmarkt,
                EndbestandKapitalmarkt = processingdatenEndbestandKapitalmarkt
            };

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(processingdaten.ID, id, name: nameof(processingdaten.ID));
            CompareAndPrintDouble(processingdaten.AktiveKredite, processingdatenAktiveKredite, name: nameof(processingdaten.AktiveKredite));
            CompareAndPrintDouble(processingdaten.Par30Kredite, processingdatenPar30Kredite, name: nameof(processingdaten.Par30Kredite));
            CompareAndPrintDouble(processingdaten.Spareinlagen, processingdatenSpareinlagen, name: nameof(processingdaten.Spareinlagen));
            CompareAndPrintDouble(processingdaten.NeueKredite, processingdatenNeueKredite, name: nameof(processingdaten.NeueKredite));
            CompareAndPrintDouble(processingdaten.Mitarbeiter, processingdatenMitarbeiter, name: nameof(processingdaten.Mitarbeiter));
            CompareAndPrintDouble(processingdaten.Filialen, processingdatenFilialen, name: nameof(processingdaten.Filialen));
            CompareAndPrintDouble(processingdaten.NeueEinlagen, processingdatenNeueEinlagen, name: nameof(processingdaten.NeueEinlagen));
            CompareAndPrintDouble(processingdaten.TransaktionenKapitalmarkt, processingdatenTransaktionenKapitalmarkt, name: nameof(processingdaten.TransaktionenKapitalmarkt));
            CompareAndPrintDouble(processingdaten.EndbestandKapitalmarkt, processingdatenEndbestandKapitalmarkt, name: nameof(processingdaten.EndbestandKapitalmarkt));

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            TestForNonwriteableProperties<Processingdaten>(10);

            #endregion Klasse Processingdaten

            #region Klasse Mitarbeiter

            Console.WriteLine($"\n- {nameof(Mitarbeiter)} -\n");

            // vorgegebene Werte
            id = ZufallsInt(1, 10);
            int periodeId = ZufallsInt(1, 10);
            int mitarbeiterKreditberater = ZufallsInt(1, 10);
            int mitarbeiterAnlageberater = ZufallsInt(1, 10);
            int mitarbeiterBackofficemitarbeiter = ZufallsInt(1, 10);
            int mitarbeiterFilialen = ZufallsInt(1, 10);

            // Objekt anlegen
            Mitarbeiter mitarbeiter = ErzeugteMitarbeiter(id,
                                                          periodeId,
                                                          mitarbeiterKreditberater,
                                                          mitarbeiterAnlageberater,
                                                          mitarbeiterBackofficemitarbeiter,
                                                          mitarbeiterFilialen);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(mitarbeiter.ID, id, name: nameof(mitarbeiter.ID));
            CompareAndPrint(mitarbeiter.PeriodeID, periodeId, name: nameof(mitarbeiter.PeriodeID));
            CompareAndPrint(mitarbeiter.Kreditberater, mitarbeiterKreditberater, name: nameof(mitarbeiter.Kreditberater));
            CompareAndPrint(mitarbeiter.Anlageberater, mitarbeiterAnlageberater, name: nameof(mitarbeiter.Anlageberater));
            CompareAndPrint(mitarbeiter.Backofficemitarbeiter, mitarbeiterBackofficemitarbeiter, name: nameof(mitarbeiter.Backofficemitarbeiter));
            CompareAndPrint(mitarbeiter.Filialen, mitarbeiterFilialen, name: nameof(mitarbeiter.Filialen));

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            TestForNonwriteableProperties<Mitarbeiter>(6);

            #endregion Klasse Mitarbeiter

            #region Klasse Indizes

            Console.WriteLine($"\n- {nameof(Indizes)} -\n");

            // vorgegebene Werte
            id = ZufallsInt(1, 10);
            periodeId = ZufallsInt(1, 10);
            double indizesItIndex = ZufallsDouble(0.0, 1.0);
            double indizesQualifikationsindex = ZufallsDouble(0.0, 1.0);

            // Objekt anlegen
            Indizes indizes = ErzeugteIndizes(id, periodeId, indizesItIndex, indizesQualifikationsindex);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(indizes.ID, id, name: nameof(indizes.ID));
            CompareAndPrint(indizes.PeriodeID, periodeId, name: nameof(indizes.PeriodeID));
            CompareAndPrintDouble(indizes.ITIndex, indizesItIndex, name: nameof(indizes.ITIndex));
            CompareAndPrintDouble(indizes.Qualifikationsindex, indizesQualifikationsindex, name: nameof(indizes.Qualifikationsindex));

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            TestForNonwriteableProperties<Indizes>(4);

            #endregion Klasse Indizes

            #region Klasse Einheiten

            Console.WriteLine($"\n- {nameof(Einheiten)} -\n");

            // vorgegebene Werte
            id = ZufallsInt(1, 10);
            periodeId = ZufallsInt(1, 10);
            double einheitenAktiveKredite = ZufallsDouble(1.0, 10.0);
            double einheitenPar30Kredite = ZufallsDouble(1.0, 10.0);
            double einheitenSpareinlagen = ZufallsDouble(1.0, 10.0);
            double einheitenNeueKredite = ZufallsDouble(1.0, 10.0);
            double einheitenNeueEinlagen = ZufallsDouble(1.0, 10.0);
            double einheitenTransaktionenKapitalmarkt = ZufallsDouble(1.0, 10.0);
            double einheitenEndbestandKapitalmarkt = ZufallsDouble(1.0, 10.0);

            // Objekt anlegen
            Einheiten einheiten = ErzeugteEinheiten(id,
                                                    periodeId,
                                                    einheitenAktiveKredite,
                                                    einheitenPar30Kredite,
                                                    einheitenSpareinlagen,
                                                    einheitenNeueKredite,
                                                    einheitenNeueEinlagen,
                                                    einheitenTransaktionenKapitalmarkt,
                                                    einheitenEndbestandKapitalmarkt);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(einheiten.ID, id, name: nameof(einheiten.ID));
            CompareAndPrint(einheiten.PeriodeID, periodeId, name: nameof(einheiten.PeriodeID));
            CompareAndPrintDouble(einheiten.AktiveKredite, einheitenAktiveKredite, name: nameof(einheiten.AktiveKredite));
            CompareAndPrintDouble(einheiten.Par30Kredite, einheitenPar30Kredite, name: nameof(einheiten.Par30Kredite));
            CompareAndPrintDouble(einheiten.Spareinlagen, einheitenSpareinlagen, name: nameof(einheiten.Spareinlagen));
            CompareAndPrintDouble(einheiten.NeueKredite, einheitenNeueKredite, name: nameof(einheiten.NeueKredite));
            CompareAndPrintDouble(einheiten.NeueEinlagen, einheitenNeueEinlagen, name: nameof(einheiten.NeueEinlagen));
            CompareAndPrintDouble(einheiten.TransaktionenKapitalmarkt, einheitenTransaktionenKapitalmarkt, name: nameof(einheiten.TransaktionenKapitalmarkt));
            CompareAndPrintDouble(einheiten.EndbestandKapitalmarkt, einheitenEndbestandKapitalmarkt, name: nameof(einheiten.EndbestandKapitalmarkt));

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            TestForNonwriteableProperties<Einheiten>(9);

            #endregion Klasse Einheiten

            Console.WriteLine("\n--- Testen der Datenbank\n");

            #region Klasse Datenbank

            Console.WriteLine("\n- Laden der Datenbank -\n");

            // Test der Ausnahmen vor Laden der Datenbank
            object dummy;
            CompareAndPrint(Datenbank.IstGeladen, false);
            ProvokeException(() => dummy = Datenbank.PeriodenIDs);
            ProvokeException(() => dummy = Datenbank.Processingdaten);
            ProvokeException(() => dummy = Datenbank.Periode(1));
            ProvokeException(() => dummy = Datenbank.Einheiten(1));
            ProvokeException(() => dummy = Datenbank.Indizes(1));
            ProvokeException(() => dummy = Datenbank.Mitarbeiter(1));

            // Test der Plausibilitätsprüfungen
            ProvokeException(() => Datenbank.DatenbankAuslesen(null));
            ProvokeException(() => Datenbank.DatenbankAuslesen(" "));

            // Test, ob der Pfad auch wirklich genutzt wird
            ProvokeException(() => Datenbank.DatenbankAuslesen("AA:cf4rfnu"));

            // Datenbank laden
            Datenbank.DatenbankAuslesen(_location + _dbname);


            Console.WriteLine("\n- Testen der Eigenschaften -\n");

            Console.WriteLine("IstGeladen");
            CompareAndPrint(Datenbank.IstGeladen, true);
            IsNull(typeof(Datenbank).GetProperty("IstGeladen").GetSetMethod());

            Console.WriteLine("PeriodenIDs");
            CompareAndPrint(Datenbank.PeriodenIDs.SequenceEqual([1, 2, 3, 4, 5]), true);
            IsNull(typeof(Datenbank).GetProperty("PeriodenIDs").GetSetMethod());
            Datenbank.PeriodenIDs[2] = 222;
            CompareAndPrint(Datenbank.PeriodenIDs[2], 3);

            Console.WriteLine("Processingdaten");
            processingdaten = Datenbank.Processingdaten;
            CompareAndPrintDouble(processingdaten.AktiveKredite, .5);
            CompareAndPrintDouble(processingdaten.Par30Kredite, 10.02);
            CompareAndPrintDouble(processingdaten.Spareinlagen, .05);
            CompareAndPrintDouble(processingdaten.NeueKredite, 1.0);
            CompareAndPrintDouble(processingdaten.Mitarbeiter, .03);
            CompareAndPrintDouble(processingdaten.Filialen, .5);
            CompareAndPrintDouble(processingdaten.NeueEinlagen, .05);
            CompareAndPrintDouble(processingdaten.TransaktionenKapitalmarkt, .04);
            CompareAndPrintDouble(processingdaten.EndbestandKapitalmarkt, .02);
            IsNull(typeof(Datenbank).GetProperty("Processingdaten").GetSetMethod());


            Console.WriteLine("\n- Testen der Methoden zum Abrufen der Tabellenzeilen -\n");

            VorgabeZeile<Periode>[] vorgabePeriode =
            [
                new(1, ErzeugtePeriode(1, new DateTime(2020, 1, 1), new DateTime(2020, 12, 31))),
                new(2, ErzeugtePeriode(2, new DateTime(2021, 1, 1), new DateTime(2021, 12, 31))),
                new(3, ErzeugtePeriode(3, new DateTime(2022, 1, 1), new DateTime(2022, 12, 31))),
                new(4, ErzeugtePeriode(4, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31))),
                new(5, ErzeugtePeriode(5, new DateTime(2024, 1, 1), new DateTime(2024, 12, 31)))
            ];
            TestDurchführen(vorgabePeriode, Datenbank.Periode);

            VorgabeZeile<Einheiten>[] vorgabeEinheiten =
            [
                new(1, ErzeugteEinheiten(1, 1, 154.62, 5.42, 585.00, 80.00, 498.00, 4.80, 14.65)),
                new(2, ErzeugteEinheiten(2, 2, 160.34, 5.13, 594.00, 87.00, 525.00, 5.00, 15.00)),
                new(3, ErzeugteEinheiten(3, 3, 161.50, 5.30, 601.00, 90.00, 507.00, 5.10, 15.12)),
                new(4, ErzeugteEinheiten(4, 4, 162.30, 5.26, 603.00, 92.00, 480.00, 4.70, 15.30)),
                new(5, ErzeugteEinheiten(5, 5, 164.40, 5.46, 606.00, 85.00, 460.00, 4.30, 14.90))
            ];
            TestDurchführen(vorgabeEinheiten, Datenbank.Einheiten);

            VorgabeZeile<Indizes>[] vorgabeIndizes =
            [
                new(1, ErzeugteIndizes(1, 1, 0.9936, 1.0)),
                new(2, ErzeugteIndizes(2, 2, 0.9936, 1.0)),
                new(3, ErzeugteIndizes(3, 3, 1.01, 0.99)),
                new(4, ErzeugteIndizes(4, 4, 1.02, 1.01)),
                new(5, ErzeugteIndizes(5, 5, 1.01, 0.98))
            ];
            TestDurchführen(vorgabeIndizes, Datenbank.Indizes);

            VorgabeZeile<Mitarbeiter>[] vorgabeMitarbeiter =
            [
                new(1, ErzeugteMitarbeiter(1, 1, 570, 500, 400, 150)),
                new(2, ErzeugteMitarbeiter(2, 2, 570, 500, 400, 150)),
                new(3, ErzeugteMitarbeiter(3, 3, 560, 510, 405, 150)),
                new(4, ErzeugteMitarbeiter(4, 4, 550, 500, 410, 150)),
                new(5, ErzeugteMitarbeiter(5, 5, 560, 495, 405, 150))
            ];
            TestDurchführen(vorgabeMitarbeiter, Datenbank.Mitarbeiter);

            #endregion Klasse Datenbank

            Console.WriteLine("\n--- ERGEBNIS\n");

            PrintResult();
        }
    }
}
