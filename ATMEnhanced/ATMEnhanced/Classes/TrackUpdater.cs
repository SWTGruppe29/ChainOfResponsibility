using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using ATM.Interfaces;
using ATMEnhanced.Interfaces;
using static ATM.Interfaces.ITrackCalculator;

namespace ATMEnhanced.Classes
{
    public class TrackUpdater : Handler, ITrackUpdater
    {
        private ITrackCalculator _calculator;
        private List<Track> _prevTracks;
        private DateTime dateTimeNow;

        public TrackUpdater()
        {
            _prevTracks = new List<Track>();
        }

        protected override void Handle(object data)
        {
            List<Track> trackData = (List<Track>)data;
            foreach (var tracks in trackData)
            {
                int index = CheckIfTrackIsInList(tracks.Tag);
                if (index>=0)
                {
                    _calculator=new TrackCalculator(_prevTracks[index].XCoordinate, _prevTracks[index].YCoordinate, tracks.XCoordinate,tracks.YCoordinate,_prevTracks[index].LastDateUpdate,tracks.LastDateUpdate);
                    tracks.Velocity = _calculator.CalculateHorizontalVelocity();
                    tracks.CurrentCompCourse = _calculator.CalculateCompassCourse();
                }
            }
            //overskriver de gamle tracks med de nye.
            _prevTracks = trackData;

            base.Handle(trackData);
        }

        public int CheckIfTrackIsInList(string tag)
        {
            for (int i = 0; i < _prevTracks.Count; i++)
            {
                if (_prevTracks[i].Tag == tag)
                    return i;
            }

            return -1;
        }



    }
}
