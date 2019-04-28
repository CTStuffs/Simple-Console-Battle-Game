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
        public int HP { get; set; } = 100;
        public int maxHP { get; set; } = 100;
        public int maxMP { get; set; } = 100;
        public int MP { get; set; } = 50;
        public AGENT_STATUS Status { get; set; } = AGENT_STATUS.NORMAL;
        public string Name { get; set; } = "AGENT";
        public string Description { get; set; } = "AGENT DESCRIPTION";
        public string Image { get; set; } = "INSERT IMAGE HERE";
        public List<Move> Moves { get; set; } = new List<Move>();
        public int Shield { get; set; } = 0;


        public Agent()
        {

        }

        public Agent(string name, string description, string image)
        {
            Name = name;
            Description = description;
            Image = image;
        }

        /*
        public Agent AddHP(int diff)
        {
            this.HP += diff;
            if (this.HP > this.maxHP)
            {
                this.HP = this.maxHP;
            }
            return this;
        }*/

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
            this.HP += mod;

            if (this.HP > this.maxHP)
            {
                this.HP = this.maxHP;
            }

            if (this.HP < 0)
            {
                this.HP = 0;
            }

        }

        public void ModMP(int mod)
        {
            this.MP += mod;

            if (this.MP > this.maxMP)
            {
                this.MP = this.maxMP;
            }

            if (this.MP < 0)
            {
                this.MP = 0;
            }

        }

        public bool IsAlive()
        {
            return HP > 0;
        }

        public virtual string Display()
        {
            return ("Name: " + Name + "\nDescription: " + Description + "\nStatus: " + Status.ToString()
                + "\nHP: " + HP + "\nMP: " + MP);
        }

        public virtual void Display(string text)
        {
            Console.WriteLine(text);
        }

        public List<Move> GetMoves()
        {
            return Moves;
        }

    }
}
