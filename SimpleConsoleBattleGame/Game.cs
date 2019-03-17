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
        GameController gControl;
        TextCollection texts;
        bool debugMode = false;

        public Game()
        {
            Load();
            
            
        }

        public Game(bool debug)
        {
            this.debugMode = debug;
        }

        public void Load()
        {
         
            Hero = new Hero("Hero", "A hero from a far away land", null);
            Boss = new Boss("Boss", "A boss from the realms of demons", null);

            // replace this with a JSON loading function
            Hero.AddMove(new Move(1,"Attack", 25, 0, enums.AGENT_STATUS.NORMAL, 100, "Normal attack", "attacks"));
            Hero.AddMove(new Move(2, "Magic Attack", 50, 50, enums.AGENT_STATUS.NORMAL, 100, "Magic attack", "performs a magic attack!"));
            Hero.AddMove(new Move(3, "Defend", 10, 0, enums.AGENT_STATUS.NORMAL, 100, "Defending attack", "defends and restores health!"));
            Hero.AddMove(new Move(4, "Escape", 10, 0, enums.AGENT_STATUS.NORMAL, 60, "Escape move", "tried to escape!"));

            Hero.Moves[3].Lethal = false;

            Boss.AddMove(new Move(1, "Strike", 15, 0, enums.AGENT_STATUS.POISON, 100, "Poison Attack", "fired poison from the mouth"));
            Boss.AddMove(new Move(2, "Attack", 25, 0, enums.AGENT_STATUS.NORMAL, 100, "Normal attack", "attacks"));

            texts = new TextCollection();
            gControl  = new GameController(texts, Hero, Boss);
        }



        public void Run()
        {
            // load all things needed for the game (agent stats, endings achieved etc.)
            Load();


            // show game UI

            Console.WriteLine(texts.Find("intro_text"));
            while (true)
            {
                // print HERO UI.
                // ask for input

                Console.WriteLine(texts.Find("idle_hero_text"));
                Console.WriteLine(gControl.GetHeroDisplay());


                if (debugMode) { Console.WriteLine(gControl.GetBossDisplay()); }
                //Hero.Display();
                //Boss.Display("The Boss is standing there...menancingly...");
                //Console.WriteLine();

                
                Console.WriteLine(gControl.ShowAvailableMoves());

                Console.WriteLine(texts.Find("input_prompt"));

                string input;
                while (true)
                {
                    input = InputValidator.ReceiveText();
                    if(gControl.ProcessInput(input))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(texts.Find("bad_input_error"));
                    }

                }
                Console.WriteLine(gControl.GetGameResponse());
   
                if (!gControl.CheckGameState())
                {
                    gControl.ProcessEnd();
                    Console.WriteLine(gControl.GetGameResponse());
                    break;
                }
                else
                {
                    Console.WriteLine(texts.Find("input_waiting"));
                }
                //Act(input, Hero);

                // perform middleman tasks
                
                // receive output from boss agent
                // perform effect on hero
                // check gamestate
            }

            Console.WriteLine(texts.Find("game_ended"));
            // await user input for hero
        }

        
       

       


       

      
    }
}
