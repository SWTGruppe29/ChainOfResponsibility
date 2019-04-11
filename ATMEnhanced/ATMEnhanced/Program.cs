using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATMEnhanced.Classes;

namespace ATMEnhanced
{
    class Program
    {
        static void Main(string[] args)
        {
            ATMSystemFactory factory = new ATMSystemFactory();
            factory.CreateATMSystem();
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
