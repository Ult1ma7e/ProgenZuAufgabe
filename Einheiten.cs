using System;

// erstellt von fapa1019

namespace EasyBankingBackOffice.Datenhaltung
{
    /// <summary>
    /// Represents operations and data from "Einheiten " table
    /// </summary>
    public class Einheiten
    {

        // Private fields to store data internaly
        private int _iD;
        private int _periodeID;
        private double _aktiveKredite;
        private double _par30Kredite;
        private double _spareinlagen;
        private double _neueKredite;
        private double _neueEinlagen;
        private double _transaktionenKapitalmarkt;
        private double _endbestandKapitalmarkt;


        /// <summary>
        /// The get and init methods for each coloumn in the "Einheiten" table in the database
        /// </summary>
        public int ID
        {
            get { return _iD; }
            init { _iD = value; }
        }

        public int PeriodeID
        {
            get { return _periodeID; }
            init { _periodeID = value; }
        }

        public double AktiveKredite
        {
            get { return _aktiveKredite; }
            init { _aktiveKredite = value; }
        }

        public double Par30Kredite
        {
            get { return _par30Kredite; }
            init { _par30Kredite = value; }
        }

        public double Spareinlagen
        {
            get { return _spareinlagen; }
            init { _spareinlagen = value; }
        }

        public double NeueKredite
        {
            get { return _neueKredite; }
            init { _neueKredite = value; }
        }

        public double NeueEinlagen
        {
            get { return _neueEinlagen; }
            init { _neueEinlagen = value; }
        }

        public double TransaktionenKapitalmarkt
        {
            get { return _transaktionenKapitalmarkt; }
            init { _transaktionenKapitalmarkt = value; }
        }

        public double EndbestandKapitalmarkt
        {
            get { return _endbestandKapitalmarkt; }
            init { _endbestandKapitalmarkt = value; }
        }

        // Empty Constructor
        public Einheiten() { }
    }
}
