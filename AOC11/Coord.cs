using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC11
{
    internal struct Coord : IComparable<Coord>
    {
        public int X;
        public int Y;

        public int CompareTo(Coord other)
        {
            var yComp = Y.CompareTo(other.Y);
            if(yComp != 0)
            {
                return yComp;
            }
            return X.CompareTo(other.X);
        }
    }
}
