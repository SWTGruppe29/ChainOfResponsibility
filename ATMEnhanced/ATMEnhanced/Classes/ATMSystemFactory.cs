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
        private TransponderDataReceiver _receiver;
        private Decoder _decoder;
        private RegionFilter _regionFilter;
        private TrackUpdater _trackUpdater;
        private SeparationChecker _separationChecker;
        private ConsolePrinter _consolePrinter;
        private Logger _logger;
        public void CreateATMSystem()
        {
            _receiver = new TransponderDataReceiver(TransponderReceiverFactory.CreateTransponderDataReceiver());
            _decoder = new Decoder();
            _regionFilter = new RegionFilter(new AirSpace(10000, 90000, 90000, 10000, 20000, 500));
            _trackUpdater = new TrackUpdater();
            _separationChecker = new SeparationChecker(new SeparationCondition(300,5000));
            _consolePrinter = new ConsolePrinter();
            _logger = new Logger();
            _receiver.SetSuccessor(_decoder).SetSuccessor(_regionFilter).SetSuccessor(_trackUpdater)
                .SetSuccessor(_separationChecker).SetSuccessor(_consolePrinter).SetSuccessor(_logger);
        }

        public void Start()
        {

        }
    }
}
