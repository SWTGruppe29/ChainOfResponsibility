using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using ATMEnhanced.Classes;

namespace ATM.Classes
{
    public class SeparationChecker : Handler, ISeparationChecker
    {
        public override void Handle(object data)
        {
            List<Track> tracks = (List<Track>) data;
            foreach (var track in tracks)
            {
                CheckForSeparation(tracks, track);
            }
            TrackData trackData = new TrackData();
            trackData.Conflicts = _currentConflicts;
            trackData.Tracks = tracks;
            base.Handle(trackData);
        }

        private ICondition _separationCondition;
        private List<Conflict> _currentConflicts;

        
        public SeparationChecker(ICondition separationCondition)
        {
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
