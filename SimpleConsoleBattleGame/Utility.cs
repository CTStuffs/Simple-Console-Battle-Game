using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleConsoleBattleGame
{
    class Utility
    {
        public Utility()
        {
        }

        public List<T> ReadListFromJson<T>(string fileName)
        {
            JArray array = JArray.Parse(File.ReadAllText(fileName));
            List<T> list = JsonConvert.DeserializeObject<List<T>>(array.ToString());
            return list;
        }
    }
}
