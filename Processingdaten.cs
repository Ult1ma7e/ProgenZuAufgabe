using System;

namespace Datenhaltung
{
    public class Processingdaten
    {
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

        public Processingdaten() { }
        public Processingdaten(int id, double aktiveKredite, double endbestandKapitalmarkt, double filialen,
                               double mitarbeiter, double neueEinlagen, double neueKredite, double par30Kredite, double spareinlagen,
                               double transaktionenKapitalmarkt)
        {
            if (id <= 0)
            {
                throw new Exception("Processingdaten: ID must be greater than 0.");
            }

            if (aktiveKredite < 0 || filialen < 0 || mitarbeiter < 0 ||
                neueEinlagen < 0 || neueKredite < 0 || spareinlagen < 0)
            {
                throw new Exception("Processingdaten: Negative numeric values are not allowed.");
            }

            if (endbestandKapitalmarkt < 0 || par30Kredite < 0 || transaktionenKapitalmarkt < 0)
            {
                throw new Exception("Processingdaten: Financial values cannot be negative.");
            }

            ID = id;
            AktiveKredite = aktiveKredite;
            EndbestandKapitalmarkt = endbestandKapitalmarkt;
            Filialen = filialen;
            Mitarbeiter = mitarbeiter;
            NeueEinlagen = neueEinlagen;
            NeueKredite = neueKredite;
            Par30Kredite = par30Kredite;
            Spareinlagen = spareinlagen;
            TransaktionenKapitalmarkt = transaktionenKapitalmarkt;
        }
    }
}


