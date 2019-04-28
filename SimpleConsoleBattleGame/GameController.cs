using System;
using System.Collections.Generic;
using System.Text;
using SimpleConsoleBattleGame.models;
using SimpleConsoleBattleGame.enums;

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
        int maxAccuracy = 100;
        int damageModMin = 5;
        int damageModMax = 15;

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

        public string GetGameResponse() // maybe this needs to be a seperate object instead of string
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
            if (currentMove == null && Int32.TryParse(moveName, out numFormat))
            {
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
        // improve on this later
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
            gameResponseSb.Append("The " + initiator.Name + " " + move.Description + "!");
            switch (move.MoveType)
            {

                case MoveType.ATTACK:
                    ProcessMoveDamage(move, initiator, target);
                    break;
                case MoveType.DEFEND:
                    ProcessDefend(move, target);
                    break;
                case MoveType.AILMENT:
                    ProcessAilment(move, target);
                    break;
                case MoveType.HEAL:
                    ProcessHeal(move, target);
                    break;
                case MoveType.OTHER:
                    // do other stuff here
                    break;
                default:
                    break;
            }

            /*
            // check if the move is lethal
            if (move.Lethal)
            {
                // this needs to be changed into a generic method
                if (move.Name.ToUpper().Contains("ATTACK"))
                {
                    
                   
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
            }*/
            // this needs to be replaced.

            return false;

        }

        public bool ProcessAgentState(Agent target)
        {
            if (target.Status == AGENT_STATUS.POISON)
            {
                gameResponseSb.Append(texts.Find("dmg_poison"));\
                //ProcessAilmentDamage(target)
                
            }

            return true;
        }

        // this function is redundant?
        // replaced by special functions for each move type
        /*
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
        }*/

        public void ProcessMoveDamage(Move move, Agent initiator, Agent target)
        {
            //userResponseSb.AppendLine("The " + initiator.Name + " " + move.Description + "!");

            Random rand = new Random();
            int damageMod = rand.Next(damageModMin, damageModMax); // make these variables and not magic numbers
            int damage = move.Power + damageMod;

            // subtract the MP required
            if (move.MPReq > 0)
            {
                initiator.ModMP(move.MPReq);
            }

            // perform the accuracy roll if the move has less than 100 accuracy
            if (move.Accuracy < maxAccuracy)
            {

                if (rand.Next(move.Accuracy, maxAccuracy) + 1 < move.Accuracy)
                {
                    damage = 0;
                    gameResponseSb.AppendLine(texts.Find("move_missed"));
                    return;
                }
                // else peform the damage stuff

            }
            else
            {
                gameResponseSb.AppendLine(" " + damage + " damage to the " + target.Name + "!");
                target.ModHP(-(damage - target.Shield));
                target.Shield = 0;
                return;

            }
        }



        public void ProcessDefend(Move move, Agent target)
        {
            target.Shield += move.Power;
        }

        public void ProcessAilment(Move move, Agent target)
        {
            if (move.InflictedStatus != AGENT_STATUS.NORMAL && move.InflictedStatus != AGENT_STATUS.ERROR)
            {
                target.Status = move.InflictedStatus;
                gameResponseSb.AppendLine("The " + target.Name + " is now inflicted with " + target.Status.ToString() + "!");

                
               
            }

        }
        public void ProcessHeal(Move move, Agent target)
        {
            if (move.Power > 0)
            {
                target.ModHP(move.Power);
                gameResponseSb.AppendLine("The " + target.Name + " was healed for " + move.Power.ToString() + "!");
            }
        }


        // check the game state and whether it is allowed to continue
        // perhaps the ending text should be added to the text game response here?
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
        // maybe merge this and gameEnd() together?
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
