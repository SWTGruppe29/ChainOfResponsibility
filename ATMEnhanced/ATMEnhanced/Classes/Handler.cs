﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMEnhanced.Classes
{
    public abstract class Handler
    {
        protected Handler _successor;

        public Handler SetSuccessor(Handler successor)
        {
            _successor = successor;
            return _successor;
        }

        protected virtual void Handle(object data)
        {
            _successor?.Handle(data);
        }
    }
}
