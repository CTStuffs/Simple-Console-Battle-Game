using SimpleConsoleBattleGame.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame
{
    public class Game
    {
        Hero Hero;
        Boss Boss;

        public Game()
        {
         
        }
        

        public void Run()
        {
            // load all things needed for the game (agent stats, endings achieved etc.)
            Load();

            Console.WriteLine("Welcome to the Game!");
            Console.WriteLine("Oh no, the BOSS is attacking! HERO, stop him!");
            // show game UI

           
            while (true)
            {
                // print HERO UI.
                // ask for input
                Console.WriteLine("Input your command: ");
                string input;
                while (true)
                {
                    input = InputValidator.ReceiveText();
                    if(Act(input, Hero))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(input + "is not a valid input! Please try again!");
                    }

                }
                Hero.Display();
                //Boss.Display();
                Console.WriteLine("The Boss is standing there...menancingly...");

                
                if (!CheckState())
                {
                    break;
                }
                else
                {
                    Console.WriteLine("What will you do?");
                }
                //Act(input, Hero);

                // perform middleman tasks
                
                // receive output from boss agent
                // perform effect on hero
                // check gamestate
            }

            Console.WriteLine("End of game.");
            // await user input for hero
        }

        public void Load()
        {
            Hero = new Hero("Hero", "A hero", null);
            Boss = new Boss("Boss", "A boss", null);

            Hero.AddMove(new Move("Attack", 40, enums.AGENT_STATUS.NORMAL, 100, "Normal attack"));
            Boss.AddMove(new Move("Strike", 20, enums.AGENT_STATUS.POISON, 100, "Poison Attack"));
        }

        // this may need an additional class
        public bool Act(string moveName, Agent initiator)
        {
            Move move = initiator.FindMove(moveName);
            if (move != null)
            {
                Console.WriteLine("Executing the Move: " + move.Name);
                if (initiator.GetType() == Hero.GetType())
                {
                    ProcessMove(move, Boss);
                }
                else
                {
                    ProcessMove(move, Hero);
                }
            }
            else
            {
                return false;
            }

            // send string to middleman, middleman deciphers it as a move, middleman processes it onto boss

            // update both here if necessary
            return true;
        }

        public bool CheckState()
        {
            if (!Boss.IsAlive())
            {
                return false;
            }

            return true;
        }

        public void ProcessMove(Move move, Agent target)
        {
            int maxAccuracy = 100;
            int damage = move.Damage;
            if (move.Accuracy < maxAccuracy)
            {
                Random rand = new Random();
                if (rand.Next(move.Accuracy, maxAccuracy) + 1 < move.Accuracy)
                {
                    damage = 0;
                }
            }
            else
            {
                target.ModHP(-damage);
            }

          
        }

      
    }
}
