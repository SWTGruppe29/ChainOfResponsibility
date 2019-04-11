using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using TransponderReceiver;

namespace ATM.Interfaces
{
    public interface IATMFactory
    {
        IAirSpace CreateAirSpace();
        ICondition CreateCondition();
        IConsolePrinter CreateConsolePrinter();
        ILogger CreateLogger();
        ISeparationChecker CreateSeparationChecker();
    }
}
