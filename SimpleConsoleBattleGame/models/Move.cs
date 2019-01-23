using SimpleConsoleBattleGame.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    public class Move
    {
        public int Damage { get; set; } = 1;
        public AGENT_STATUS InflictedStatus { get; set;} = AGENT_STATUS.NORMAL;
        public int Accuracy { get; set; } = 100;
        public string Description { get; set; } = "DEFAULT_DESCRIPTION";
        public string Name { get; set; }  = "DEFAULT_NAME";
        public bool Polarity { get; set; }

        public Move()
        {

        }

        public Move(string name, int damage, AGENT_STATUS status, int accuracy, string desc)
        {
            Damage = damage;
            InflictedStatus = status;
            Accuracy = accuracy;
            Description = desc;
            Name = name;
        }
    }
}
