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
    public class ATMSystemFactory
    {
        public void CreateATMSystem()
        {
            TransponderDataReceiver receiver = new TransponderDataReceiver(TransponderReceiverFactory.CreateTransponderDataReceiver());
            Decoder decoder = new Decoder();
            RegionFilter regionFilter = new RegionFilter(new AirSpace(10000, 90000, 90000, 10000, 20000, 500));
            TrackUpdater trackUpdater = new TrackUpdater();
            SeparationChecker separationChecker = new SeparationChecker(new SeparationCondition(300,5000));
            ConsolePrinter consolePrinter = new ConsolePrinter();
            Logger logger = new Logger();
            receiver.SetSuccessor(decoder).SetSuccessor(regionFilter).SetSuccessor(trackUpdater)
                .SetSuccessor(separationChecker).SetSuccessor(consolePrinter).SetSuccessor(logger);
        }
    }
}
