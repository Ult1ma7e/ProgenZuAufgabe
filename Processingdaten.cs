using System;

// erstellt von fapa1019

namespace EasyBankingBackOffice.Datenhaltung
{
    /// <summary>
    /// Stores parameters and reference values corresponding to the "Processingdaten" table
    /// </summary>
    public class Processingdaten
    {
        // Private fields to store data internaly
        private int _iD;
        private double _aktiveKredite;
        private double _endbestandKapitalmarkt;
        private double _filialen;
        private double _mitarbeiter;
        private double _neueEinlagen;
        private double _neueKredite;
        private double _par30Kredite;
        private double _spareinlagen;
        private double _transaktionenKapitalmarkt;



        /// <summary>
        /// The get and init methods for each coloumn in the "Processingdaten" table in the database
        /// </summary>
        public int ID
        {
            get { return _iD; }
            init { _iD = value; }
        }

        public double AktiveKredite
        {
            get { return _aktiveKredite; }
            init { _aktiveKredite = value; }
        }

        public double EndbestandKapitalmarkt
        {
            get { return _endbestandKapitalmarkt; }
            init { _endbestandKapitalmarkt = value; }
        }

        public double Filialen
        {
            get { return _filialen; }
            init { _filialen = value; }
        }

        public double Mitarbeiter
        {
            get { return _mitarbeiter; }
            init { _mitarbeiter = value; }
        }

        public double NeueEinlagen
        {
            get { return _neueEinlagen; }
            init { _neueEinlagen = value; }
        }

        public double NeueKredite
        {
            get { return _neueKredite; }
            init { _neueKredite = value; }
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

        public double TransaktionenKapitalmarkt
        {
            get { return _transaktionenKapitalmarkt; }
            init { _transaktionenKapitalmarkt = value; }
        }

        // Empty Constructor
        public Processingdaten() { }
    }
}


