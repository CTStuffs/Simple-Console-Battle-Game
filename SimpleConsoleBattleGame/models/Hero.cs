using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    public class Hero: Agent
    {
        public Hero()
        {

        }

        public Hero(string name, string description, string image): base(name, description, image)
        {
            
        }

        public override string Display()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n[HERO: " + Name + "]");
            sb.AppendLine("HP: " + HP);
            sb.AppendLine("MP: " + MP);
            sb.AppendLine("Status: " + Status.ToString());

            return sb.ToString();
        }


    }
}
