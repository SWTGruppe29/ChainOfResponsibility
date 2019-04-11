using System.Reflection.Metadata.Ecma335;

namespace HeartStoneCor.Classes
{

    //abstract klasse til minions -> bruges her også som basehandler.
    public abstract class Minion
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        protected Minion(string name, int attack, int defense)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }

        protected abstract void Handle(object request);

    }

    public class Crypt_Lord : Minion
    {
        public Crypt_Lord(string name, int attack, int defense) : base(name, attack, defense)
        {

        }

        protected override void Handle(object sender)
        {
            throw new System.NotImplementedException();
        }
    }
}

