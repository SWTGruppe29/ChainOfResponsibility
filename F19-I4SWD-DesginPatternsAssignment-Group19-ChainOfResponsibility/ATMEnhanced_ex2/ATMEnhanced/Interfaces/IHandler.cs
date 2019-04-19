using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMEnhanced.Interfaces
{
    public interface IHandler
    {
        IHandler SetSuccessor(IHandler handler);
        void Handle(object data);
    }
}
