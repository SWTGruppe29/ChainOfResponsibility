using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using ATMEnhanced.Interfaces;
using TransponderReceiver;

namespace ATMEnhanced.Classes
{
    public class Decoder : Handler, IDecoder
    {
        private List<string> datastring;
        private string flightNum;
        private DateTime dateTimeNew;
        private int x, y, alt;
        private List<Track> _tracks;

        public Decoder()
        {
            _tracks = new List<Track>();
            datastring = new List<string>();
        }

        public Decoder(List<string> dataSt, List<Track> tracks)
        {
            datastring = dataSt;
            _tracks = tracks;
        }

        public override void Handle(object data)
        {
            _tracks = new List<Track>();
            foreach (var track in (List<string>)data)
            {
                List(track);
                TypeConverter();

                Track newTrack = new Track(flightNum, x, y, alt, dateTimeNew);
                _tracks.Add(newTrack);
            }
            base.Handle(new List<Track>(_tracks));
        }


        private void List(string s)
        {
            datastring = s.Split(';').Reverse().ToList<string>();
            datastring.Reverse();
        }

        private void dateConverter()
        {
            dateTimeNew = DateTime.ParseExact(datastring[4], "yyyyMMddHHmmssfff", null);

        }

        private void TypeConverter()
        {
            flightNum = datastring[0];
            int.TryParse(datastring[1], out x);
            int.TryParse(datastring[2], out y);
            int.TryParse(datastring[3], out alt);
            dateConverter();
        }
    }
}
