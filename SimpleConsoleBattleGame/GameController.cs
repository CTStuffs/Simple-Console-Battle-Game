using System;
using System.Collections.Generic;
using System.Text;
using SimpleConsoleBattleGame.models;

namespace SimpleConsoleBattleGame
{
    public class GameController
    {
        Hero hero;
        Boss boss;
        TextCollection texts;
        StringBuilder gameResponseSb;
        bool overrideEnding = false;
        bool debugMode = false;

        public GameController(TextCollection t)
        {
            texts = t;
            gameResponseSb = new StringBuilder();
        }

        public GameController(TextCollection t, Hero h, Boss b)
        {
            hero = h;
            boss = b;
            texts = t;
            gameResponseSb = new StringBuilder();
        }

        public string GetHeroDisplay()
        {
            return hero.Display();
        }

        public string GetBossDisplay()
        {
            return boss.Display();
        }

        public string GetGameResponse()
        {
            string response = gameResponseSb.ToString();
            gameResponseSb.Clear();
            return response;
        }

        public string ShowAvailableMoves()
        {
            StringBuilder sb = new StringBuilder();
          
            sb.AppendLine(texts.Find("available_moves_header"));
            foreach (Move m in hero.Moves)
            {
                sb.AppendLine(m.Id + ". " + m.Name);
            }
            return sb.ToString();
        }

        public bool ProcessInput(string moveName)
        {
            Move currentMove = null;
            int numFormat = -1;

            // attempt to find the move
            currentMove = hero.Moves.Find(x => x.Name == moveName);

            // check if the input was for the move number instead
            if (currentMove == null && Int32.TryParse(moveName, out numFormat)) {
                currentMove = hero.Moves.Find(x => x.Id == Int32.Parse(moveName));
            }

            if (currentMove == null)
            {
                return false;
            }

            if (!ProcessMove(currentMove, hero, boss))
            {
                return false;
            }
            else
            {
                //ProcessMove(currentMove, hero, boss);
                ProcessAI(boss, hero);
                return true;
            }
        }

        // process the AI 
        public void ProcessAI(Agent initiator, Agent target)
        {
            List<Move> initiatorMoves = initiator.GetMoves();
            Random rand = new Random();
            Move aiMove = initiatorMoves[rand.Next(0, initiatorMoves.Count)];


            ProcessMove(aiMove, initiator, target);

        }


        public bool ProcessMove(Move move, Agent initiator, Agent target)
        {

            // Check that there is enough MP.
            if (move.MPReq > 0 && initiator.MP < move.MPReq)
            {
                //gameResponseSb.Append(texts.Find("no_mp_error"));
                return false;
            }

            // check if the move is lethal
            if (move.Lethal)
            {
                // this needs to be changed into a generic method
                if (move.Name.ToUpper().Contains("ATTACK"))
                {
                    gameResponseSb.Append("The " + initiator.Name + " " + move.Description + "!");
                    ProcessDamage(move, initiator, target);
                    return true;
                }
                else if (move.Name.ToUpper().Contains("DEFEND"))
                {
                    initiator.HP += move.Power;
                    gameResponseSb.Append("The " + initiator.Name + " " + move.Description + "!");
                    return true;
                }
                else
                {
                    gameResponseSb.Append("The " + initiator.Name + " " + move.Description + "!");
                    ProcessDamage(move, initiator, target);
                    return true;
                }
            }
            else
            {
                if (move.Name.ToUpper().Contains("ESCAPE"))
                {
                    gameResponseSb.Append("The " + initiator.Name + " " + move.Description + "!");
                    ProcessNonLethal(move);
                    return true;
                }
            }
            // this needs to be replaced.
            
            return false;

        }

        public void ProcessNonLethal(Move move)
        {
            int maxAccuracy = 100;
            Random rand = new Random();
            if (move.Accuracy == maxAccuracy || rand.Next(move.Accuracy, maxAccuracy) + 1 >= move.Accuracy)
            {
                overrideEnding = true;
            }
            else
            {
                overrideEnding = false;
            }
        }

        public void ProcessDamage(Move move, Agent initiator, Agent target)
        {
            //userResponseSb.AppendLine("The " + initiator.Name + " " + move.Description + "!");

            Random rand = new Random();
            int maxAccuracy = 100;
            int damageMod = rand.Next(5, 15);
            int damage = move.Power + damageMod;

            // subtract the MP required
            if (move.MPReq > 0)
            {
                initiator.MP -= move.MPReq;
            }

            // perform the accuracy roll if the move has less than 100 accuracy
            if (move.Accuracy < maxAccuracy)
            {

                if (rand.Next(move.Accuracy, maxAccuracy) + 1 < move.Accuracy)
                {
                    damage = 0;
                }
            }
            else
            {
                gameResponseSb.AppendLine(" " + damage + " damage to the " + target.Name + "!");
                target.ModHP(-damage);
                
            }
        }


        // check the game state and whether it is allowed to continue
        public bool CheckGameState()
        {
            if (overrideEnding)
            {
                return false;
            }

            // 
            if (!boss.IsAlive() || !hero.IsAlive())
            {
                return false;
            }

            return true;
        }

        // process the end of the game
        public void ProcessEnd()
        {
            if (overrideEnding)
            {
                gameResponseSb.AppendLine(texts.Find("escape_ending_1"));
                return;
            }

            if (hero.HP > boss.HP)
            {

                gameResponseSb.AppendLine(texts.Find("hero_ending_1"));
                return;
            }
            else if (hero.HP < boss.HP)
            {
                gameResponseSb.AppendLine(texts.Find("boss_ending_1"));
                return;
            }
            else
            {
                gameResponseSb.AppendLine(texts.Find("draw_ending_1"));
                return;
            }
        }

    }
}
