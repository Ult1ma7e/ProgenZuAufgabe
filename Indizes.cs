using System;

// erstellt von dado1011

namespace EasyBankingBackOffice.Datenhaltung
{
    public class Indizes
    {
        // Private fields to store data internaly
        private int _iD;
        private int _periodeID;
        private double _iTIndex;
        private double _qualifikationsindex;


        /// <summary>
        /// The get and init methods for each coloumn in the "Indizes" table in the database
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

        public double ITIndex
        {
            get { return _iTIndex; }
            init { _iTIndex = value; }
        }

        public double Qualifikationsindex
        {
            get { return _qualifikationsindex; }
            init { _qualifikationsindex = value; }
        }


        // Empty Constructor
        public Indizes() {}
    }
}

