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
        private IHandler _receiver;
        private IHandler _decoder;
        private IHandler _regionFilter;
        private IHandler _trackUpdater;
        private IHandler _separationChecker;
        private IHandler _consolePrinter;
        private IHandler _logger;
        public void CreateATMSystem()
        {
            _receiver = new TransponderDataReceiver(TransponderReceiverFactory.CreateTransponderDataReceiver());
            _decoder = new Decoder();
            _regionFilter = new RegionFilter(new AirSpace(10000, 90000, 90000, 10000, 20000, 500));
            _trackUpdater = new TrackUpdater();
            _separationChecker = new SeparationChecker(new SeparationCondition(3000,10000));
            _consolePrinter = new ConsolePrinter();
            _logger = new Logger();

            _receiver.SetSuccessor(_decoder).SetSuccessor(_regionFilter).SetSuccessor(_trackUpdater)
                .SetSuccessor(_separationChecker).SetSuccessor(_consolePrinter).SetSuccessor(_logger);
        }

    }
}
