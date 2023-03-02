using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using StarWars.ConsoleApp.Business;
using StarWars.ConsoleApp.Models;

using static System.Console;

namespace StarWars.ConsoleApp
{
    internal static class Program
    {
        private static readonly ICharacterBL _characterBL = new CharacterBL(
            ApiHelper.ApiClient
        );

        /// <summary>
        /// The entry point of the application.
        /// </summary>
        private static void Main()
        {
            WriteLine(
                @"
      ________________.  ___     .______
     /                | /   \    |   _  \
    |   (-----|  |----`/  ^  \   |  |_)  |
     \   \    |  |    /  /_\  \  |      /
.-----)   |   |  |   /  _____  \ |  |\  \-------.
|________/    |__|  /__/     \__\| _| `.________|
 ____    __    ____  ___     .______    ________.
 \   \  /  \  /   / /   \    |   _  \  /        |
  \   \/    \/   / /  ^  \   |  |_)  ||   (-----`
   \            / /  /_\  \  |      /  \   \
    \    /\    / /  _____  \ |  |\  \---)   |
     \__/  \__/ /__/     \__\|__| `._______/
"
            );

            bool shouldContinue = true;
            while (shouldContinue)
            {
                WriteLine(
                    "Available Star Wars info query options:\n"
                        + "[1] Get a list of all character information\n"
                        + "[2] Get a character's information by their name\n"
                        + "[3] Get a list of character information by allegiance\n"
                        + "[4] Get a list of character information by trilogy\n"
                        + "[0] Exit\n"
                );
                Write("Which query would you like to look up?\n» ");
                string menuInput = ReadLine();
                WriteLine();

                switch (menuInput)
                {
                    case "1":
                        // Get a list of all character information
                        LookupAllCharacters(_characterBL);
                        break;

                    case "2":
                        // Get a character's information by their name
                        break;

                    case "3":
                        // Get a list of character information by allegiance
                        break;

                    case "4":
                        // Get a list of character information by trilogy
                        break;

                    case "0":
                        shouldContinue = false;
                        break;

                    default:
                        WriteLine("Invalid choice");
                        continue;
                }
                if (!shouldContinue)
                {
                    break;
                }
                WriteLine();

                Write("Would you like to look up more info? ([y]/n)\n» ");
                string continueInput = ReadLine();
                WriteLine();

                if (continueInput.StartsWith("n", ignoreCase: true, culture: null))
                {
                    shouldContinue = false;
                }
            }
            WriteLine(
                @"
   __.-._
   '-._""7'  May the Force
    /'.-c    be with you,
    |  /T      always.
   _)_/LI
"
            );

            WriteLine("Press any key to exit . . . ");
            ReadKey(intercept: true);
        }

        /// <summary>
        /// Handles exceptions thrown by the API BL classes.
        /// </summary>
        /// <param name="ex">The exception to handle.</param>
        /// <returns>
        /// <see langword="true">true</see> if the exception was handled; otherwise, <see
        /// langword="false">false</see>.
        /// </returns>
        /// <remarks>
        /// This method is passed as a predicate to <see
        /// cref="AggregateException.Handle">AggregateException.Handle</see>. For <see
        /// cref="HttpRequestException">HttpRequestException</see>s, it writes the <see
        /// cref="Exception.Message">message</see> to the console. Other <see
        /// cref="Exception">exception</see>s are not handled.
        /// </remarks>
        private static bool HandleApiExceptions(Exception ex)
        {
            if (ex is HttpRequestException)
            {
                WriteLine($"ERROR: {ex.Message}");
            }
            return ex is HttpRequestException;
        }

        /// <summary>
        /// Gets all characters from the API and writes them to the console.
        /// </summary>
        private static void LookupAllCharacters(ICharacterBL characterBL)
        {
            List<CharacterModel> characters;
            try
            {
                characters = characterBL.GetAllAsync().Result.ToList();
            }
            catch (AggregateException ex)
            {
                ex.Flatten().Handle(HandleApiExceptions);
                return;
            }
            WriteCharacterTable(characters);
        }

        private static void WriteCharacterTable(IEnumerable<CharacterModel> characters)
        {
            // Find longest Id and Name from characters
            int idWidth = characters.Max(c => c.Id.ToString().Length);
            int nameWidth = characters.Max(c => c.Name.Length);
            const int jediWidth = 5;
            const int allegianceWidth = 10;
            const int trilogyWidth = 8;
            // Write table header
            WriteLine(
                string.Join(
                    " | ",
                    "Id".PadRight(idWidth),
                    "Name".PadRight(nameWidth),
                    "Jedi?".PadRight(jediWidth),
                    "Allegiance".PadRight(allegianceWidth),
                    "Trilogy".PadRight(trilogyWidth)
                )
            );
            WriteLine(
                string.Join(
                    " | ",
                    new string('-', idWidth),
                    new string('-', nameWidth),
                    new string('-', jediWidth),
                    new string('-', allegianceWidth),
                    new string('-', trilogyWidth)
                )
            );
            // Write table body
            foreach (CharacterModel character in characters)
            {
                WriteLine(
                    string.Join(
                        " | ",
                        character.Id.ToString().PadRight(idWidth),
                        character.Name.PadRight(nameWidth),
                        $"{(character.IsJedi ? "Jedi" : "")}".PadRight(jediWidth),
                        character.Allegiance.ToString().PadRight(allegianceWidth),
                        character.TrilogyIntroducedIn.ToString().PadRight(trilogyWidth)
                    )
                );
            }
        }
    }
}
