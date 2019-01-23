using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsoleBattleGame
{
    // simple class for receiving console input and validating it immediately after
    static class InputValidator
    {

        public static string Receive()
        {
            string text = "";

            while (true)
            {
                text = Console.ReadLine();

                if (!String.IsNullOrEmpty(text))
                {
                    return text;
                }
                else
                {
                    Console.WriteLine("Cannot enter an empty string! Please try again!");
                }
            }
        }

        public static string ReceiveText()
        {
            return Receive();
        }

        public static int ReceiveInt()
        {
            string text = "";
            int value = 0;
            while (true)
            {
                text = Receive();
                if (Int32.TryParse(text, out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Cannot enter a non-numerical value!");
                }

            }

            /*
            do
            {
                
            } while (String.IsNullOrEmpty(text) && (Int32.TryParse(text, out value) == false));
            return value;*/
        }

        public static int ReceiveIntWithStart(int start)
        {
            int value = 0;

            while (true)
            {
                value = ReceiveInt();

                if (value >= start)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Incorrect numerical value. Please enter a value greater than or equal to " + start);
                }
            }
        }

        public static int ReceiveIntWithRange(int start, int end)
        {
            int value = 0;

            while (true)
            {
                value = ReceiveInt();

                if (value >= start && value <= end)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Incorrect numerical value. Please enter a value between " + start + " and " + end);
                }
            }

        }

        public static int ReceiveIntWithSelection(List<int> selection)
        {
            int value = 0;
            while (true)
            {
                value = ReceiveInt();
                if (selection.Contains(value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("You need to enter a value within the selection.");
                }
            }
        }
    }

}
