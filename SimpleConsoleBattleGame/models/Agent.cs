using SimpleConsoleBattleGame.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    // this is the superclass of the Hero and the Boss classes
    // it refers to an object that acts within the game state

    public class Agent
    {
        protected int HitPoints {get; set; } = 100;
        protected int ManaPoints { get; set; } = 50;
        protected AGENT_STATUS Status { get; set; } = AGENT_STATUS.NORMAL;
        protected string Name { get; set; } = "AGENT";
        protected string Description { get; set; } = "AGENT DESCRIPTION";
        protected string Image { get; set; }  = "INSERT IMAGE HERE";
        protected List<Move> Moves { get; set; } = new List<Move>();


        public Agent()
        {

        }

        public Agent(string name, string description, string image)
        {
            Name = name;
            Description = description;
            Image = image;
        }

        public bool AddMove(Move newMove)
        {
            // check that a move of the same name doesn't already exist
            foreach(Move m in Moves)
            {
                if (m.Name == newMove.Name)
                {
                    return false;
                }
            }
            Moves.Add(newMove);
            return true;
        }

        public Move FindMove(string moveName)
        {
            foreach (Move m in Moves)
            {
                if (m.Name == moveName)
                {
                    return m;
                }
            }
            return null;
        }

        public void ModHP(int mod)
        {
            this.HitPoints += mod;
        }

        public bool IsAlive()
        {
            return HitPoints > 0;
        }

        public virtual void Display()
        {
            Console.WriteLine("Name: " + Name + "\nDescription: " + Description + "\nStatus: " + Status.ToString()
                + "\nHP: " + HitPoints + "\nMP: " + ManaPoints);
        }

    }
}
