using System;
using Microsoft.VisualBasic;

namespace Datenhaltung
{
    public class Periode
    {
        private DateTime _begin;
        private DateTime _ende;
        private int _iD;

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


        public Periode() { }
        public Periode(DateTime begin, DateTime ende, int id)
        {
            if (id <= 0)
            {
                throw new Exception("ID must be greater than 0.");
            }

            if (ende < begin)
            {
                throw new ArgumentException("The End Date can not be before the Begin Date.", nameof(ende));
            }

            ID = id;
            Beginn = begin;
            Ende = ende;
        }

        public override string ToString()
        {
            return $"{ID} : {Beginn:dd.MM.yyyy} - {Ende:dd.MM.yyyy}";
        }
    }
}


