using System;
using System.Collections.Generic;

namespace NumberCor
{

    public interface IHandler
    {
        object Handle(object request);
        IHandler SetNext(IHandler nextHandler);
    }



    abstract class BaseHandler :IHandler
    {
        private IHandler _nextHandler;

        public virtual object Handle(object request)
        {
            return _nextHandler?.Handle(request);
        }

        //monkey.SetNext(PositivHandler).SetNext(ZeroHandler).SetNext(NegativHandler).SetNext(EndHandler);
        public IHandler SetNext(IHandler nextHandler)
        {
            this._nextHandler = nextHandler;
            return nextHandler;
        }
    }

    class PositiveHandler : BaseHandler
    {
        public override object Handle(object request)
        {
            if (Int32.Parse(request.ToString()) > 0)
            {
                return $"PositiveHandler: The request number: {Int32.Parse(request.ToString())} is positive";
            }

            return base.Handle(request);
        }

    }

    class ZeroHandler : BaseHandler
    {
        public override object Handle(object request)
        {
            if (Int32.Parse(request.ToString()) == 0)
            {
                return $"ZeroHandler: The request number: {Int32.Parse(request.ToString())} is zero";
            }

            return base.Handle(request);
        }
    }

    class NegativeHandler : BaseHandler
    {
        public override object Handle(object request)
        {
            if (Int32.Parse(request.ToString()) < 0)
            {
                return $"NegativeHandler: The request number: {Int32.Parse(request.ToString())} is negative";
            }

            return base.Handle(request);
        }
    }

    class Client
    {
        public static void ClientCode(BaseHandler handler)
        {
            foreach (var number in new List<string>{"1","20","-32","0","0","12","5","-100","-2"})
            {
                Console.WriteLine($"Client: the number this time is {number}");

                var result = handler.Handle(number);

                if (result != null)
                {
                    Console.WriteLine($"{result}");
                }
                else
                {
                    //da vi arbejder med tal, burder dette aldrig kunne blive muligt, men det vil blive illustreret ved at glemme at sætte en handler på Cor.
                    Console.WriteLine($"{number} did not get handled in the handlers.");
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var Positive = new PositiveHandler();
            var Zero = new ZeroHandler();
            var Negative = new NegativeHandler();

            //Cor rækkefølge
            // PositiveHandler > ZeroHandler > NegativeHandler
            Positive.SetNext(Zero).SetNext(Negative);

            Console.WriteLine("PositiveHandler > ZeroHandler > NegativeHandler");

            Client.ClientCode(Positive);
            Console.WriteLine("\n\n");

            Console.WriteLine("ZeroHandler > NegativeHandler");
            Console.WriteLine("Alle positive tal, skal komme ud uden at være handlet.");
            Client.ClientCode(Zero);

            Console.ReadKey();

        }
    }
}
