using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    public class TextCollection
    {
        List<GameText> texts;
        Utility util = new Utility();
        public TextCollection()
        {
            texts = new List<GameText>();
            Load();
        }



        // in the future, load from file here
        public bool Load()
        {
            try
            {
                this.texts = util.ReadListFromJson<GameText>("files/ingame_text.json");
                /*
                using (StreamReader sr = new StreamReader("./files/ingame_text.json"))
                {
                    string line;

                    
                    // perform JSON loading tasks here instead
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(": ");
                        if (parts.Length == 2)
                        {
                            parts[1] = parts[1].Replace("\"", "");
                            texts.Add(new GameText(parts[0], parts[1]));
                        }


                    }
                }
                */

                /*
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                using (StreamWriter sw = new StreamWriter("./files/ingame_text.json"))
                {


                    string texts = JsonConvert.SerializeObject(this.texts,
                        new JsonSerializerSettings
                        {
                            ContractResolver = contractResolver,
                            Formatting = Formatting.Indented
                        });
                    sw.Write(texts);
                }*/

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            

            
        }

        public string Find(string id)
        {
            var result = texts.Find(x => x.id == id).text;

            if (String.IsNullOrEmpty(result))
            {
                return "ERROR: TEXT WITH ID " + id + " NOT FOUND"; 
            }

            return result;
        }
    }

    class GameText
    {
        public string id = "Default Id";
        public string text = "Default text";

        public GameText(string id, string text)
        {
            this.id = id;
            this.text = text;
        }

    }
}

