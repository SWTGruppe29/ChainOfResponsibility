using System.Collections.Generic;
using ATM.Classes;

namespace ATMEnhanced.Interfaces
{
    public interface ILogger
    {
        void LogMessage(List<Conflict> involvedTags);
    }
}
