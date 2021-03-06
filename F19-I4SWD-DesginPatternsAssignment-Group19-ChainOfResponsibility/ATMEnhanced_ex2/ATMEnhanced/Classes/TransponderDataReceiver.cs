﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMEnhanced.Interfaces;
using TransponderReceiver;

namespace ATMEnhanced.Classes
{
    public class TransponderDataReceiver : Handler, ITransponderDataReceiver
    {
        private ITransponderReceiver _transponderReceiver;

        public TransponderDataReceiver(ITransponderReceiver transponderReceiver)
        {
            _transponderReceiver = transponderReceiver;
            // Subscribing to event
            _transponderReceiver.TransponderDataReady += ReceiverTransponderReady;
        }


        public void ReceiverTransponderReady(object sender, RawTransponderDataEventArgs e)
        {
            if (e.TransponderData.Count > 0)
            {
                base.Handle(e.TransponderData);
            }
        }

    }
}
