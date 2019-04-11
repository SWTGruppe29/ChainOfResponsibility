using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class Track
    {
        public string Tag { get; set; }
        public int Altitude { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public double? CurrentCompCourse { get; set; }
        public double? Velocity { get; set; }
        public DateTime LastDateUpdate { get; set; }


        public Track(string tag, int x, int y, int altitude, DateTime trackTime)
        {
            Tag = tag;
            Altitude = altitude;
            XCoordinate = x;
            YCoordinate = y;
            LastDateUpdate = trackTime;
        }

        public Track(string tag, int x, int y, int altitude, DateTime trackTime, double compass, double velocity)
        {
            Tag = tag;
            Altitude = altitude;
            XCoordinate = x;
            YCoordinate = y;
            LastDateUpdate = trackTime;
            Velocity = velocity;
            if (compass >= 0 && compass < 360)
                CurrentCompCourse = compass;
        }
    }
}
