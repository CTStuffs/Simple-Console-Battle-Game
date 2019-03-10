using System;

namespace SimpleConsoleBattleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // load menu
            // 1. Start Game
            // 2. Exit
            // add other options later

            while (true)
            {
                PrintMenu();

                int choice = InputValidator.ReceiveIntWithRange(1, 4);

                switch (choice)
                {
                    case 1:
                        Game game = new Game();
                        game.Run();
                        break;
                    case 2:
                        Console.WriteLine("This feature has not yet been implemented.");
                        break;
                    case 3:
                        Console.WriteLine("This feature has not yet been implemented.");
                        break;
                    case 4:
                        Console.WriteLine("Thank you for playing!");
                        return;
                    default:
                        Console.WriteLine("Thank you for playing!");
                        return;
                }

            }

        }

        public static void PrintMenu()
        {
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Stats");
            Console.WriteLine("3. Options");
            Console.WriteLine("4. Exit");
        }

    }
}
