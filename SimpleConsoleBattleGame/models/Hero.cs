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
            Console.WriteLine("\n[HERO: " + Name + "]");
            Console.WriteLine("HP: " + HitPoints);
            Console.WriteLine("MP: " + ManaPoints);
            Console.WriteLine("Status: " + Status.ToString());
        }


    }
}
