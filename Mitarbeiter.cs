using System;

namespace Datenhaltung
{
    public class Mitarbeiter
    {
        private int _iD;
        private int _periodeID;
        private int _anlageberater;
        private int _kreditberater;
        private int _backofficemitarbeiter;
        private int _filialen;

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

        public Mitarbeiter() { }
        public Mitarbeiter(int id, int periodeID, int anlageberater, int kreditberater, int backofficemitarbeiter, int filialen)
        {
            if (id <= 0)
            {
                throw new Exception("ID must be greater than 0.");
            }

            if (periodeID <= 0)
            {
                throw new Exception("PeriodeID must be greater than 0.");
            }

            if (anlageberater < 0 || kreditberater < 0 || backofficemitarbeiter < 0 || filialen < 0)
            {
                throw new Exception("Check the values in \"Mitarbeiter\" table, a value can not be neggative.");
            }

            ID = id;
            PeriodeID = periodeID;
            Anlageberater = anlageberater;
            Kreditberater = kreditberater;
            Backofficemitarbeiter = backofficemitarbeiter;
            Filialen = filialen;
        }
    }
}


