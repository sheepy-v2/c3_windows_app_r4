﻿namespace C3_Windows_App
{
    internal class Helpers
    {
        internal static string Ask(string question)
        {
            string? userInput = "";

            while ((userInput is null || userInput == ""))
            {
                Console.WriteLine(question);
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        internal static int AskForInt(string question)
        {
            int retVal;
            string userInput = "";

            while (!int.TryParse(userInput, out retVal))
            {
                userInput = Ask(question);
            }

            return retVal;
        }

        internal static void Pause()
        {
            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
        }
    }
}