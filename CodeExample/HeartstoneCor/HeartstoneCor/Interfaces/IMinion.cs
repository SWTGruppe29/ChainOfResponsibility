using System.ComponentModel;

namespace HeartstoneCor.Interfaces
{
    public interface IMinion
    {
        void Handle(object request);
        IMinion SetNext(IMinion nextMinion);

    }
}