using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SimpleConsoleBattleGame.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    public class Move
    {
        // perhaps the moves should be stored within JSON instead
        public int Id { get; set; } = -1;
        public string Name { get; set; } = "DEFAULT_NAME";
        public int Power { get; set; } = 1;
        public AGENT_STATUS InflictedStatus { get; set; } = AGENT_STATUS.NORMAL;
        public int Accuracy { get; set; } = 100;
        public string Title { get; set; } = "DEFAULT_TITLE";
        public string Description { get; set; } = "DEFAULT_DESCRIPTION";
        public int MPReq { get; set; } = 0;
        public bool Lethal { get; set; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public MoveType MoveType { get; set; } = MoveType.ATTACK;

        public bool Polarity { get; set; }

        public Move()
        {

        }

        public Move(int id, string name, int power, int mpReq, AGENT_STATUS status, int accuracy, string title, string desc)
        {
            Id = id;
            Power = power;
            InflictedStatus = status;
            Accuracy = accuracy;
            Title = title;
            Description = desc;
            Name = name;
            MPReq = mpReq;
        }
    }
}
