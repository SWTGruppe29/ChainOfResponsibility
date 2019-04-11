using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace ATMEnhanced.Interfaces
{
    public interface ITransponderDataReceiver
    {
        void ReceiverTransponderReady(object sender, RawTransponderDataEventArgs e);
    }
}
