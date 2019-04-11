using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;

namespace ATM.Interfaces
{
    public class SeparationLogEventArgs : EventArgs
    {
        public List<Conflict> ConflictList { get; set; }
    }

    public interface ILogger
    {
        void LogMessage(List<Conflict> involvedTags);
        void SeparationLogDataHandler(object sender, SeparationLogEventArgs e);
        
    }
}
