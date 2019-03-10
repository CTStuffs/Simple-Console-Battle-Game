using SimpleConsoleBattleGame.models;
using System;
using System.Collections.Generic;
using System.Linq;
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

                Hero.Display();
                Boss.Display("The Boss is standing there...menancingly...");
                //Console.WriteLine();

                Console.WriteLine("Input your command: ");
                string input;
                while (true)
                {
                    input = InputValidator.ReceiveText();
                    if(ProcessPlayer(input, Hero, Boss))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(input + "is not a valid input! Please try again!");
                    }

                }

                Console.WriteLine("The hero finished his attack. The boss steps up to counter!");
                ProcessAI(Boss, Hero);

              

                
                if (!CheckState())
                {
                    ProcessEnd();
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
            Hero = new Hero("Hero", "A hero from a far away land", null);
            Boss = new Boss("Boss", "A boss from the realms of demons", null);

            // replace this with 
            Hero.AddMove(new Move("Attack", 25, enums.AGENT_STATUS.NORMAL, 100, "Normal attack", "attacks"));
            Hero.AddMove(new Move("Defend", 10, enums.AGENT_STATUS.NORMAL, 100, "Normal attack", "defends and restores health!"));
            Boss.AddMove(new Move("Strike", 15, enums.AGENT_STATUS.POISON, 100, "Poison Attack", "fired poison from the mouth"));
            Boss.AddMove(new Move("Attack", 25, enums.AGENT_STATUS.NORMAL, 100, "Normal attack", "attacks"));
        }

        public void ProcessEnd()
        {
            if (Hero.HitPoints > Boss.HitPoints) { 
            
                Console.WriteLine("The HERO has become victorious over the BOSS! Congratulations!");
                return;
            }
            else if (Hero.HitPoints < Boss.HitPoints)
            {
                Console.WriteLine("The BOSS has become victorious over the HERO! This is terrible!");
                return;
            }
            else
            {
                Console.WriteLine("The HERO and BOSS killed each other at the same time! How did this happen?");
                return;
            }
        }

        // this may need an additional class
        public bool ProcessPlayer(string moveName, Agent initiator, Agent target)
        {
            Move move = initiator.FindMove(moveName);
            if (move != null)
            {

               ProcessMove(move, initiator, target); // process the move on the
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
            if (!Boss.IsAlive() || !Hero.IsAlive())
            {
                return false;
            }

            return true;
        }



        public void ProcessMove(Move move, Agent initiator, Agent target)
        {
            if (move.Name.ToUpper().Contains("ATTACK"))
            {
                ProcessDamage(move, initiator, target);
            }
            else if (move.Name.ToUpper().Contains("DEFEND"))
            {
                initiator.HitPoints += move.Power;
                Console.Write("The " + initiator.Name + " " + move.Description + "!");
            }
            else
            {
                ProcessDamage(move, initiator, target);
            }
          
        }

        public void ProcessDamage(Move move, Agent initiator, Agent target)
        {
            Console.Write("The " + initiator.Name + " " + move.Description + "!");
            Random rand = new Random();
            int maxAccuracy = 100;
            int damageMod = rand.Next(5, 15);
            int damage = move.Power + damageMod;
            if (move.Accuracy < maxAccuracy)
            {

                if (rand.Next(move.Accuracy, maxAccuracy) + 1 < move.Accuracy)
                {
                    damage = 0;
                }
            }
            else
            {
                Console.WriteLine(" " + damage + " damage to the " + target.Name + "!");
                target.ModHP(-damage);
            }
        }


        public void ProcessAI(Agent initiator, Agent target)
        {
            List<Move> initiatorMoves = initiator.GetMoves();
            Move strongestMove = initiatorMoves.OrderByDescending(x => x.Power).First();
            ProcessMove(strongestMove, initiator, target);

        }

      
    }
}
