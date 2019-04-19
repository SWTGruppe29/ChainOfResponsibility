using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;

namespace ATMEnhanced.Classes
{
    public class TrackData
    {
        public List<Track> Tracks { get; set; }
        public List<Conflict> Conflicts { get; set; }
    }
}
