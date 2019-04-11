using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class TrackCalculator : ITrackCalculator
    {
        private double distance;
        private DateTime _dt1, _dt2;

        public TrackCalculator(int Last_x, int Last_y, int New_x, int New_y , DateTime dt1, DateTime dt2)
        {
            P1 = new Point(Last_x,Last_y);
            P2 = new Point(New_x,New_y);
            _dt1 = new DateTime();
            _dt2 = new DateTime();
            _dt1 = dt1;
            _dt2 = dt2;
        }
        
        public double CalculateHorizontalVelocity()
        {
            distance = Distance();
            TimeSpan tsSpan = _dt2.Subtract(_dt1);

            return Trim(distance/tsSpan.TotalSeconds);

        }

        public double CalculateCompassCourse()
        {
            double deltaY = P2.Y - P1.Y;
            double deltaX = P2.X - P1.X;

            double angle = Math.Atan2(deltaY, deltaX) * (180 / Math.PI);

            angle = 360 - ((angle) - 90);

            if (angle > 360)
            {
                angle -= 360;
            }

            return Trim(angle);
        }

        public double Trim(double deg)
        {
            return (Math.Round(deg, 5));
        }

        public double Distance()
        {
            double deltaY = P2.Y - P1.Y;
            double deltaX = P2.X - P1.X;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }


        #region properties

        public Point P1 { get; set; }

        public Point P2 { get; set; }

    #endregion
}

}
