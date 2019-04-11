using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class SeparationChecker : ISeparationChecker
    {
        
        //public enum Direction {north,south,east,west};
        private IAirSpace _airSpace;
        private ICondition _separationCondition;
        private List<Conflict> _currentConflicts;
        
        public SeparationChecker(IAirSpace airSpace, ICondition separationCondition)
        {
            _airSpace = airSpace;
            _separationCondition = separationCondition;
            _currentConflicts = new List<Conflict>();
        }

        public int AltitudeSeparation(int t1, int t2) 
        {
            int sep = Math.Abs(t1 - t2);
            return sep;
        }

        public double distanceBetweenTracks(Track track1, Track track2)
        {
            double dY = Math.Abs(track1.YCoordinate - track2.YCoordinate);
            double dX = Math.Abs(track1.XCoordinate -track2.XCoordinate);
            return Math.Sqrt((dY * dY) + (dX * dX));
        }
        /*
        public Direction calculateDirection(Track track1)
        {
            if (track1.CurrentCompCourse >= 45 && track1.CurrentCompCourse < 135) //Eastbound course
            {
                return Direction.east;
            }
            else if (track1.CurrentCompCourse >= 135 && track1.CurrentCompCourse < 225)
            {
                return Direction.south;
            }
            else if (track1.CurrentCompCourse >= 225 && track1.CurrentCompCourse < 315)
            {
                return Direction.west;
            }
            else
            {
                return Direction.north;
            }
        }

        public double convertToAngle(double compasCourse)
        {
            double angle = compasCourse + 90;
            if (angle > 360)
            {
                angle = angle - 360;
                return angle;
            }

            return angle;
        }

        public Point calculateEndPoint(Track track, Direction d)
        {
            switch (d)
            {
                case Direction.east:
                    int toEast = _airSpace.getEastBoundary() - track.XCoordinate;
                    int e = Convert.ToInt32(Math.Tan(convertToAngle((double) track.CurrentCompCourse)) * toEast); //SKAL UNDERSØGES OM DEN VIRKER!!!!!
                    if (track.CurrentCompCourse < 90)
                    {
                        return new Point(80000, track.YCoordinate + e);
                    }
                    else if (track.CurrentCompCourse > 90)
                    {
                        return new Point(80000, track.YCoordinate - e);
                    }
                    else
                    {
                        return new Point(80000,track.YCoordinate);
                    }
                    break;
                case Direction.west:
                    int toWest = track.XCoordinate;
                    int w = Convert.ToInt32(Math.Tan(convertToAngle((double)track.CurrentCompCourse)) * toWest); //SKAL UNDERSØGES OM DEN VIRKER!!!!!
                    if (track.CurrentCompCourse < 270)
                    {
                        return new Point(80000, track.YCoordinate + w);
                    }
                    else if (track.CurrentCompCourse > 270)
                    {
                        return new Point(80000, track.YCoordinate - w);
                    }
                    else
                    {
                        return new Point(0, track.YCoordinate);
                    }
                    break;
                case Direction.north:
                    int toNorth = _airSpace.getNorthBoundary() - track.YCoordinate;
                    int n = Convert.ToInt32(Math.Tan(convertToAngle((double)track.CurrentCompCourse) * toNorth)); //SKAL UNDERSØGES OM DEN VIRKER!!!!!
                    if (track.CurrentCompCourse < 360)
                    {
                        return new Point(track.XCoordinate - n, 80000);
                    }
                    else if (track.CurrentCompCourse > 0)
                    {
                        return new Point(track.XCoordinate + n, 80000);
                    }
                    else
                    {
                        return new Point(track.XCoordinate, 80000);
                    }
                    break;
                case Direction.south:
                    int toSouth = track.YCoordinate;
                    int s = Convert.ToInt32(Math.Tan(convertToAngle((double)track.CurrentCompCourse)) * toSouth); //SKAL UNDERSØGES OM DEN VIRKER!!!!!
                    if (track.CurrentCompCourse < 180)
                    {
                        return new Point(track.XCoordinate + s, 80000);
                    }
                    else if (track.CurrentCompCourse > 180)
                    {
                        return new Point(track.XCoordinate - s, 80000);
                    }
                    else
                    {
                        return new Point(track.XCoordinate, 80000);
                    }
                    break;
                default:
                    return null;
            }
        }

        public bool willIntersect(Point p1Start, Point p1End, Point p2Start, Point p2End)
        {
            float dx12 = Math.Abs(p1End.X - p1Start.X);
            float dy12 = Math.Abs(p1End.Y - p1Start.Y);
            float dx34 = Math.Abs(p2End.X - p2Start.X);
            float dy34 = Math.Abs(p2End.Y - p2Start.Y);

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1Start.X - p2Start.X) * dy34 + (p2Start.Y - p1Start.Y) * dx34)
                / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                return false;
            }

            return true;
        }

        public double FindDistanceToSegment(Point point, Point lineStart, Point lineEnd)
        {
            float dx = lineEnd.X - lineStart.X;
            float dy = lineEnd.Y - lineStart.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                dx = point.X - lineStart.X;
                dy = point.Y - lineStart.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            float t = ((point.X - lineStart.X) * dx + (point.Y - lineStart.Y) * dy) /
                      (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                dx = point.X - lineStart.X;
                dy = point.Y - lineStart.Y;
            }
            else if (t > 1)
            {
                dx = point.X - lineEnd.X;
                dy = point.Y - lineEnd.Y;
            }
            else
            {
                Point closest = new Point(Convert.ToInt32(lineStart.X + t * dx), Convert.ToInt32(lineStart.Y + t * dy));
                dx = point.X - closest.X;
                dy = point.Y - closest.Y;
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }


        public bool tracksWillConflict(Point p1Start, Point p1End, Point p2Start, Point p2End)
        {
            Point closest;
            double best_dist = double.MaxValue, test_dist;

            // Try p1.
            test_dist = FindDistanceToSegment(p1Start, p2Start, p2End);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
            }

            // Try p2.
            test_dist = FindDistanceToSegment(p1End, p2Start, p2End);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
            }

            // Try p3.
            test_dist = FindDistanceToSegment(p2Start, p1Start, p1End);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
            }

            // Try p4.
            test_dist = FindDistanceToSegment(p2End, p1Start, p1End);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
            }

            return (best_dist < _separationCondition.getHorizontalSeparationCondition());
        }
        public bool willHaveConflict(Track track1, Track track2)
        {
            Direction t1D = calculateDirection(track1);
            Direction t2D = calculateDirection(track2);
            Point t1EndPoint = calculateEndPoint(track1, t1D);
            Point t2EndPoint = calculateEndPoint(track2, t2D);
            Point t1StartPoint = new Point(track1.XCoordinate,track1.YCoordinate);
            Point t2StartPoint = new Point(track2.XCoordinate,track2.YCoordinate);
            if (willIntersect(t1StartPoint, t1EndPoint, t2StartPoint, t2EndPoint))
            {
                return true;
            }
            else if (tracksWillConflict(t1StartPoint, t1EndPoint, t2StartPoint, t2EndPoint))
            {
                return true;
            }

            return false;

        }*/

        public bool horizontalSeparationConflict(Track track1, Track track2)
        {
            if (distanceBetweenTracks(track1, track2) <= //Checks for current distance between tracks
                _separationCondition.getHorizontalSeparationCondition())
            {
                return true;
            }
            return false;
        }

        public bool verticalSeparationConflict(Track track1, Track track2)
        {
            return (AltitudeSeparation(track1.Altitude, track2.Altitude) <=
                    _separationCondition.getVerticalSeparationCondition());
        }


        public bool hasConflict(Track track1, Track track2)
        {
            if (verticalSeparationConflict(track1,track2) & horizontalSeparationConflict(track1, track2))
            {
                return true;
            }
            return false;

        }

        /*public List<int> conflictExists(Conflict conflict)
        {
            List<int> conflictIndexes = new List<int>();
            for (int i = 0; i < _currentConflicts.Count; i++)
            {
                if (_currentConflicts[i].Tag1 == conflict.Tag1 & _currentConflicts[i].Tag2 == conflict.Tag2)
                    conflictIndexes.Add(i);
            }

            if (conflictIndexes.Count == 0)
                return null;
            return conflictIndexes;
        }*/

        public void RemoveAllConflictsWithTag(string tag)
        {
            List<Conflict> conflictsToRemove = new List<Conflict>();
            foreach (var conflict in _currentConflicts)
            {
                if (conflict.Tag1 == tag | conflict.Tag2 == tag)
                {
                    conflictsToRemove.Add(conflict);
                }
            }
            foreach (var conflict in conflictsToRemove)
            {
                _currentConflicts.Remove(conflict);
            }
        }

        public List<Conflict> CheckForSeparation(List<Track> tracks, Track track)
        {
            RemoveAllConflictsWithTag(track.Tag);
            foreach (var t in tracks)
            {
                if (t.Tag != track.Tag)
                {
                    if (hasConflict(t, track))
                    {
                        Conflict newConflict = new Conflict()
                        {
                            Tag1 = t.Tag,
                            Tag2 = track.Tag
                        };
                        
                     _currentConflicts.Add(newConflict);
                    }
                }
            }
            return _currentConflicts;
        }
    }
}
