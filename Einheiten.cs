using System;

namespace Datenhaltung
{
    public class Einheiten
    {
        private int _iD;
        private int _periodeID;
        private double _aktiveKredite;
        private double _par30Kredite;
        private double _spareinlagen;
        private double _neueKredite;
        private double _neueEinlagen;
        private double _transaktionaneKapitalmarkt;
        private double _endbestandKapitalmarkt;

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
            get { return _transaktionaneKapitalmarkt; }
            init { _transaktionaneKapitalmarkt = value; }
        }

        public double EndbestandKapitalmarkt
        {
            get { return _endbestandKapitalmarkt; }
            init { _endbestandKapitalmarkt = value; }
        }

        public Einheiten() { }
        public Einheiten(int iD, int periodeID, double aktiveKredite, double par30Kredite, double spareinlagen, double neueKredite,
                         double neueEinlagen, double transaktionaneKapitalmarkt, double endbestandKapitalmarkt)
        {

            if (iD <= 0)
            {
                throw new Exception("ID must be greater than 0.");
            }

            if (periodeID <= 0)
            {
                throw new Exception("PeriodeID must be greater than 0.");
            }

            if (aktiveKredite < 0 || par30Kredite < 0 || spareinlagen < 0 || neueKredite < 0 || neueEinlagen < 0 || transaktionaneKapitalmarkt < 0 || endbestandKapitalmarkt < 0)
            {
                throw new Exception("Check the values in \"Einheiten\" table, a value can not be negative.");
            }

            ID = iD;
            PeriodeID = periodeID;
            AktiveKredite = aktiveKredite;
            Par30Kredite = par30Kredite;
            Spareinlagen = spareinlagen;
            NeueKredite = neueKredite;
            NeueEinlagen = neueEinlagen;
            TransaktionenKapitalmarkt = transaktionaneKapitalmarkt;
            EndbestandKapitalmarkt = endbestandKapitalmarkt;
        }
    }
}

