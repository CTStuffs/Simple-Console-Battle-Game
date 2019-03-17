using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleConsoleBattleGame.models
{
    public class TextCollection
    {
        List<GameText> texts;
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
                using (StreamReader sr = new StreamReader("./files/ingame_text.txt"))
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

