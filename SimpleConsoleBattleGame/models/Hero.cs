using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    class Hero: Agent
    {
        public Hero()
        {

        }

        public Hero(string name, string description, string image): base(name, description, image)
        {
            
        }

        public override void Display()
        {
            Console.WriteLine("[HERO: " + Name + "]");
            Console.WriteLine("\tHP: " + HitPoints);
            Console.WriteLine("\tMP: " + ManaPoints);
            Console.WriteLine("\tStatus: " + Status.ToString());
        }


    }
}
