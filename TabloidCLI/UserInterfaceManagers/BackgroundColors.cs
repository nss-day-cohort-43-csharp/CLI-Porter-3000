using System;

namespace TabloidCLI.UserInterfaceManagers
{

    class BackgroundColors
    {

        public void ColorSelection()
        {

            Console.WriteLine();
            Console.WriteLine("Please choose one of the following font colors:  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[1] = Red");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[2] = Yellow");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[3] = Green");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[4] = Blue");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[5] = Cyan");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[6] = White");
            Console.WriteLine();


            Console.Write("Your Selection:  ");
            string backgroundColorChoice = Console.ReadLine();


            switch (backgroundColorChoice)
            {

                case "1":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    break;


                case "2":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Clear();
                    break;


                case "3":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    break;


                case "4":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    break;


                case "5":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Clear();
                    break;


                case "6":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    break;


                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }
        }
    }
}