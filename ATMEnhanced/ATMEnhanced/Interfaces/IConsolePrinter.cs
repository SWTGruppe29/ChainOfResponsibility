using System.Collections.Generic;
using ATM.Classes;

namespace ATMEnhanced.Interfaces
{
    public interface IConsolePrinter
    {
        void Print(List<Track> tracks, List<Conflict> conflictTags);
    }
}
