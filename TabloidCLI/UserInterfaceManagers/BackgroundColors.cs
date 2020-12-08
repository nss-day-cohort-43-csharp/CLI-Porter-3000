using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundColors
    {
        public void ColorSelection()
        {
            Console.WriteLine("");
            Console.Write("Welcome to Tabloid");
            Console.WriteLine("--------------------- \n");
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
            Console.WriteLine("");
            string backgroundColorChoice = Console.ReadLine();

            switch (backgroundColorChoice)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "2":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                    ;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                    ;
                case "4":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                    ;
                case "5":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                    ;
                case "6":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                    ;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }
        }
    }
}