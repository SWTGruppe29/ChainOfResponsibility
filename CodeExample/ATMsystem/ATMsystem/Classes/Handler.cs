using System;
using System.Collections.Generic;
using System.Text;

namespace ATMsystem.Classes
{
    abstract class Handler
    {
        protected Handler _sucessor;

        public void SetSuccessor(Handler successor)
        {
            _sucessor = successor;
        }

        protected void HandleRequest()
        {
            _sucessor?.HandleRequest();
        }
    }
}
