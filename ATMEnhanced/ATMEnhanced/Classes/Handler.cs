using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMEnhanced.Interfaces;

namespace ATMEnhanced.Classes
{
    public abstract class Handler : IHandler
    {
        protected IHandler _successor;

        public IHandler SetSuccessor(IHandler successor)
        {
            _successor = successor;
            return _successor;
        }

        public virtual void Handle(object data)
        {
            _successor?.Handle(data);
        }

    }
}
