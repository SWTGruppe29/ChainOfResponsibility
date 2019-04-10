using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleExampleCoR
{
    class Program
    {



        public abstract class Creature : IDisposable
        {
            protected Game game;
            protected readonly int InitialAttack;
            protected readonly int InitialDefense;

            public Creature(Game game, int initialAttack, int initialDefense)
            {
                this.game = game;
                InitialAttack = initialAttack;
                InitialDefense = initialDefense;
            }

            public abstract int Attack { get; }
            public abstract int Defense { get; }


            public override string ToString()
            {
                return $"Create of Attack {Attack} and Defense{Defense}";
            }


            protected abstract void Handle(object sender, Query q);
            public abstract void Dispose();

        }

        public class Goblin : Creature
        {
            public Goblin(Game game, int initialAttack, int initialDefense) : base(game, initialAttack, initialDefense)
            {
                game.Queries += Handle;
            }

            public override int Attack
            {
                get
                {
                    var q = new Query(Query.Argument.Attack, InitialAttack);
                    game.PerformQuery(this,q);
                    return q.Value;
                }
            }

            public override int Defense {
                get
                {
                    var q = new Query(Query.Argument.Defense, InitialDefense);
                    game.PerformQuery(this, q);
                    return q.Value;
                }
            }
            protected override void Handle(object sender, Query q)
            {
                var thisGoblin = sender as Goblin;

                if (q.WhatToQuery == Query.Argument.Attack)
                {
                    q.Value = thisGoblin.InitialAttack;
                    foreach (var creature in game.Creatures)
                    {
                        if (!ReferenceEquals(thisGoblin, creature) && creature is GoblinKing)
                        {
                            q.Value += 1;
                        }
                        
                    }
                }
                else if (q.WhatToQuery == Query.Argument.Defense)
                {
                    q.Value = thisGoblin.InitialDefense;
                    foreach (var creature in game.Creatures)
                    {
                        if (!ReferenceEquals(thisGoblin, creature))
                        {
                            q.Value += 1;
                        }
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(q.WhatToQuery));
                }
            }

            public override void Dispose()
            {
                game.Queries -= Handle;
            }

            public override string ToString()
            {
                return $"Goblin of Attack {Attack} and Defense {Defense}";
            }
        }

        public class GoblinKing : Goblin
        {
            public GoblinKing(Game game, int initialAttack, int initialDefense) : base(game, initialAttack, initialDefense)
            {

            }

            public override string ToString()
            {
                return $"GoblinKing of Attack {Attack} and Defense {Defense}";
            }
        }

        public class Game
        {
            public IList<Creature> Creatures;
            public event EventHandler<Query> Queries;

            public Game()
            {
                Creatures = new List<Creature>();
            }

            public void PerformQuery(object sender, Query q)
            {
                Queries?.Invoke(sender, q);
            }
        }

        public class Query
        {
            public enum Argument
            {
                Attack, Defense
            }

            public Argument WhatToQuery;
            public int Value;

            public Query(Argument whatToQuery, int value)
            {
                WhatToQuery = whatToQuery;
                Value = value;
            }
        }

        static void Main(string[] args)
        {




            var game = new Game();

            game.Creatures.Add(new Goblin(game,1,1));

            Console.WriteLine(game.Creatures[0]);
            Console.WriteLine("Lets play 5 goblins more");
            for (int i = 0; i < 5; i++)
            {
                game.Creatures.Add(new Goblin(game,1,1));
                
            }
            Console.WriteLine("\nBefore GoblinKing");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(game.Creatures[i]);

            }

            game.Creatures.Add(new GoblinKing(game,3,3));
            Console.WriteLine("\nAfter GoblinKing");
            foreach (var creature in game.Creatures)
            {
                Console.WriteLine(creature);
            }


            Console.WriteLine("He killed 4 of my Goblins");
            for (int i = 0; i < game.Creatures.Count; i++)
            {
                
                
                game.Creatures.RemoveAt(i);
                
            }

            //game.Creatures.Add(new GoblinKing(game, 3, 3));
            foreach (var creature in game.Creatures)
            {
                Console.WriteLine(creature);
            }
            Console.ReadKey();
        }
    }

    
}
