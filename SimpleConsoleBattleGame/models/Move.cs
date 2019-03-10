using SimpleConsoleBattleGame.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    public class Move
    {
        public int Power { get; set; } = 1;
        public AGENT_STATUS InflictedStatus { get; set;} = AGENT_STATUS.NORMAL;
        public int Accuracy { get; set; } = 100;
        public string Title { get; set; } = "DEFAULT_TITLE";
        public string Description { get; set; } = "DEFAULT_DESCRIPTION";
        public string Name { get; set; }  = "DEFAULT_NAME";
        public bool Polarity { get; set; }

        public Move()
        {

        }

        public Move(string name, int power, AGENT_STATUS status, int accuracy, string title, string desc)
        {
            Power = power;
            InflictedStatus = status;
            Accuracy = accuracy;
            Title = title;
            Description = desc;
            Name = name;
        }
    }
}
