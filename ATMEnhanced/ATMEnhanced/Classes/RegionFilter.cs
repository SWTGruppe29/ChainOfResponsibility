using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using ATM.Interfaces;
using ATMEnhanced.Interfaces;

namespace ATMEnhanced.Classes
{
    class RegionFilter : Handler, IRegionFilter
    {
        private IAirSpace _airSpace;
        List<Track> _trackData = new List<Track>();

        public RegionFilter(IAirSpace airSpace)
        {
            _airSpace = airSpace;
        }

        protected override void Handle(object data)
        {
            TrackData trackData = (TrackData)data;

            _trackData = trackData.Tracks;

            foreach (var track in _trackData)
            {
                if(!_airSpace.IsInAirSpace(track.XCoordinate,track.YCoordinate,track.Altitude))
                {
                    int index = CheckIfTrackIsInList(track.Tag);
                    _trackData.RemoveAt(index);
                    
                }
            }

            base.Handle(_trackData);
        }

        public int CheckIfTrackIsInList(string tag)
        {
            for (int i = 0; i < _trackData.Count; i++)
            {
                if (_trackData[i].Tag == tag)
                    return i;
            }

            return -1;
        }
    }
}
