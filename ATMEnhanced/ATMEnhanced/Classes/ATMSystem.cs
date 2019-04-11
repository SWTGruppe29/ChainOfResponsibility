using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using ATMEnhanced.Interfaces;
using TransponderReceiver;

namespace ATM.Classes
{
    public class ATMSystem : IATMSystem
    {
        private List<Track> Tracks = new List<Track>();
        private int x, y, alt;
        private TrackCalculator calc;
        public List<string> datastring;
        private DateTime dateTimeNew;
        private string flightNum;
        private Track newTrack;

        public ITransponderReceiver receiver;
        private IAirSpace _airSpace;
        private ICondition _condition;
        private IConsolePrinter _consolePrinter;
        private ILogger _logger;
        private ISeparationChecker _separationChecker;
        private ITrackCalculator _calc;

        List<Conflict> _conflictList = new List<Conflict>();
        //public event EventHandler<SeparationLogEventArgs> SeparationLogDataReady;
        //public event EventHandler<ConsoleSeparationEventArgs> ConsoleSeparationDataReady;

        public ATMSystem(IATMFactory factory, ITransponderReceiver transponderReceiver)
        {   
            //Calling factory methods
            receiver = transponderReceiver;
            _airSpace = factory.CreateAirSpace();
            _condition = factory.CreateCondition();
            _consolePrinter = factory.CreateConsolePrinter();
            _logger = factory.CreateLogger();
            _separationChecker = factory.CreateSeparationChecker();

            //Subscribing to events
            this.receiver.TransponderDataReady += ReceiverOnTransponderReady;
            //this.SeparationLogDataReady += _logger.SeparationLogDataHandler;
            //this.ConsoleSeparationDataReady += _consolePrinter.ConsoleSeparationDataHandler;
        }

        


        public void ReceiverOnTransponderReady(object sender, RawTransponderDataEventArgs e)
        {
            if (e.TransponderData.Count > 0)
            {
                //Receive data, calls list foreach.
                foreach (var data in e.TransponderData)
                {
                    List(data);

                    //Converts datastring to separate variables and appropiate types.
                    TypeConverter();


                        if (_airSpace.IsInAirSpace(x, y,alt)==true)
                        {
                            int index = CheckIfTrackIsInList(flightNum);
                            if (index >= 0)
                            {

                            calc = new TrackCalculator(Tracks[index].XCoordinate, Tracks[index].YCoordinate, x, y,
                                Tracks[index].LastDateUpdate, dateTimeNew);
                            newTrack = new Track(flightNum, x, y, alt, dateTimeNew, calc.CalculateCompassCourse(),
                                calc.CalculateHorizontalVelocity());
                            Tracks[index] = newTrack;

                            }
                            else
                            {
                            newTrack = new Track(flightNum, x, y, alt, dateTimeNew);
                            Tracks.Add(newTrack);
                            }

                            _conflictList = _separationChecker.CheckForSeparation(Tracks, newTrack);
                            //_separationChecker = new SeparationChecker(_airSpace, _condition);

                        }
                        else
                        {
                            int index = CheckIfTrackIsInList(flightNum);
                            if (index >= 0)
                            {
                            Tracks.RemoveAt(index);
                            }
                        }
                }

                SeparationLogEventArgs logArgs = new SeparationLogEventArgs();
                logArgs.ConflictList = _conflictList;
                SeparationLogDataReady?.Invoke(this, logArgs);

                ConsoleSeparationEventArgs conArgs = new ConsoleSeparationEventArgs()
                {
                    conflictList = _conflictList,
                    tracks = Tracks
                };
                ConsoleSeparationDataReady?.Invoke(this, conArgs);
                
            }
        }
       

        public int CheckIfTrackIsInList(string tag)
        {
            for (int i = 0; i < Tracks.Count; i++)
            {
                if (Tracks[i].Tag == tag)
                    return i;
            }

            return -1;
        }

        private void List(string s)
        {
            
            datastring = s.Split(';').Reverse().ToList<string>();
            datastring.Reverse();
            
        }

        private void dateConverter()
        {
            dateTimeNew = DateTime.ParseExact(datastring[4],"yyyyMMddHHmmssfff",null);
            
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
