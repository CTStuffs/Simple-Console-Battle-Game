﻿using SimpleConsoleBattleGame.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    // this is the superclass of the Hero and the Boss classes
    // it refers to an object that acts within the game state

    public class Agent
    {
        public int HitPoints {get; set; } = 100;
        public int ManaPoints { get; set; } = 50;
        public AGENT_STATUS Status { get; set; } = AGENT_STATUS.NORMAL;
        public string Name {  get; set; } = "AGENT";
        public string Description { get; set; } = "AGENT DESCRIPTION";
        public string Image { get; set; }  = "INSERT IMAGE HERE";
        public List<Move> Moves { get; set; } = new List<Move>();


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
