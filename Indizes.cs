using System;

namespace Datenhaltung
{
    public class Indizes
    {
        private int _iD;
        private int _periodeID;
        private double _iTIndex;
        private double _qualifikationsindex;

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


        public Indizes() { }
        public Indizes(int id, int periodeID, double itIndex, double qualifikationsindex)
        {
            if (id <= 0)
            {
                throw new Exception("ID must be greater than 0.");
            }

            if (periodeID <= 0)
            {
                throw new Exception("PeriodeID must be greater than 0.");
            }

            if (itIndex < 0 || qualifikationsindex < 0)
            {
                throw new Exception("Check the values in \"Indizes\" table, a value can not be negative.");
            }

            ID = id;
            PeriodeID = periodeID;
            ITIndex = itIndex;
            Qualifikationsindex = qualifikationsindex;
        }
    }
}


