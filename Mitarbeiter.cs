using System;

// erstellt von dado1011

namespace EasyBankingBackOffice.Datenhaltung
{
    public class Mitarbeiter
    {
        // Private fields to store data internaly
        private int _iD;
        private int _periodeID;
        private int _anlageberater;
        private int _kreditberater;
        private int _backofficemitarbeiter;
        private int _filialen;

        /// <summary>
        /// The get and init methods for each coloumn in the "Mitarbeiter" table in the database
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

        public int Anlageberater
        {
            get { return _anlageberater; }
            init { _anlageberater = value; }
        }

        public int Kreditberater
        {
            get { return _kreditberater; }
            init { _kreditberater = value; }
        }

        public int Backofficemitarbeiter
        {
            get { return _backofficemitarbeiter; }
            init { _backofficemitarbeiter = value; }
        }

        public int Filialen
        {
            get { return _filialen; }
            init { _filialen = value; }
        }

        // Empty Constructor
        public Mitarbeiter() { }
    }
}

