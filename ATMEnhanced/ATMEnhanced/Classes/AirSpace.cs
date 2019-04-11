using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class AirSpace : IAirSpace
    {
        private int westBoundary;
        private int eastBoundary;
        private int northBoundary;
        private int southBoundary;
        private int upperBoundary;
        private int lowerBoundary;


        public AirSpace(int w, int e, int n, int s, int u, int l)
        {
            westBoundary = w;
            eastBoundary = e;
            northBoundary = n;
            southBoundary = s;
            upperBoundary = u;
            lowerBoundary = l;
        }

        public bool IsInAirSpace(int x, int y, int altitude)
        {
            if (x >= westBoundary & x <= eastBoundary & y >= southBoundary & y <= northBoundary & altitude >= lowerBoundary & altitude <= upperBoundary)
                return true;
            else
                return false;
        }
    }
}
