using System;
using Microsoft.VisualBasic;

// erstellt von fapa1019

namespace EasyBankingBackOffice.Datenhaltung

{
    /// <summary>
    /// Defines a time period with start and end dates , that coresspond to an ID
    /// </summary>
    public class Periode
    {
        // Private fields to store data internaly
        private DateTime _begin;
        private DateTime _ende;
        private int _iD;


        /// <summary>
        /// The get and init methods for each coloumn in the "Periode" table in the database
        /// </summary>
        public DateTime Beginn
        {
            get { return _begin; }
            init { _begin = value; }
        }

        public DateTime Ende
        {
            get { return _ende; }
            init { _ende = value; }
        }

        public int ID
        {
            get { return _iD; }
            init { _iD = value; }
        }

        // Empty Constructor
        public Periode() { }

        // Method to output a custom string representation of the object
        public override string ToString()
        {
            return $"{ID} : {Beginn:dd.MM.yyyy} - {Ende:dd.MM.yyyy}";
        }
    }
}


