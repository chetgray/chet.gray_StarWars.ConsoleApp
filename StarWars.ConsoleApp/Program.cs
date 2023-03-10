using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using StarWars.ConsoleApp.Business;
using StarWars.WebApi.Proxy;
using StarWars.WebApi.Proxy.Models;

using static System.Console;
using static System.Math;

namespace StarWars.ConsoleApp
{
    internal static class Program
    {
        private static readonly StarWarsProxy _api = new StarWarsProxy();
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
                        LookupAllCharacters();
                        break;

                    case "2":
                        // Get a character's information by their name
                        LookupCharacterByName();
                        break;

                    case "3":
                        // Get a list of character information by allegiance
                        LookupCharactersByAllegiance();
                        break;

                    case "4":
                        // Get a list of character information by trilogy
                        LookupCharactersByTrilogy();
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
        /// Queries the API for all <see cref="CharacterModel">character</see>s and writes the
        /// information to the console.
        /// </summary>
        private static void LookupAllCharacters()
        {
            List<CharacterModel> characters;
            try
            {
                characters = (List<CharacterModel>)_api.ListCharactersAsync().Result;
            }
            catch (AggregateException ex)
            {
                ex.Flatten().Handle(HandleApiExceptions);
                return;
            }
            WriteCharacterTable(characters);
        }

        /// <summary>
        /// Prompts the user for a name, then queries the API for a <see
        /// cref="CharacterModel">character</see> with that <see
        /// cref"CharacterModel.Name">name</see> and writes the information to the console.
        /// </summary>
        private static void LookupCharacterByName()
        {
            string nameInput = null;
            while (string.IsNullOrEmpty(nameInput))
            {
                Write("Enter a character name to look up:\n» ");
                nameInput = ReadLine();
            }
            WriteLine();

            CharacterModel character;
            try
            {
                character = _api.GetCharacterByNameAsync(nameInput).Result;
            }
            catch (AggregateException ex)
            {
                ex.Flatten().Handle(HandleApiExceptions);
                return;
            }
            if (character is null)
            {
                WriteLine($"No character found with name \"{nameInput}\"");
                return;
            }
            WriteCharacterTable(new List<CharacterModel> { character });
        }

        /// <summary>
        /// Prompts the user for an <see cref="Allegiance">allegiance</see>, then queries the
        /// API for <see cref="CharacterModel">character</see>s with that <see
        /// cref"CharacterModel.Allegiance">allegiance</see> and writes the information to the
        /// console.
        /// </summary>
        private static void LookupCharactersByAllegiance()
        {
            string allegianceInput = null;
            Allegiance? allegiance = null;
            while (allegiance is null)
            {
                while (string.IsNullOrEmpty(allegianceInput))
                {
                    Write("Enter an allegiance to look up:\n» ");
                    allegianceInput = ReadLine();
                }
                try
                {
                    allegiance = (Allegiance)
                        Enum.Parse(typeof(Allegiance), allegianceInput, ignoreCase: true);
                }
                catch (ArgumentException)
                {
                    WriteLine($"Invalid allegiance \"{allegianceInput}\"");
                    WriteLine(
                        "Valid allegiances: "
                            + string.Join(", ", Enum.GetNames(typeof(Allegiance)))
                    );
                    allegianceInput = null;
                }
            }
            WriteLine();

            List<CharacterModel> characters;
            try
            {
                characters =
                    (List<CharacterModel>)
                        _api.ListCharactersByAllegianceAsync((Allegiance)allegiance).Result;
            }
            catch (AggregateException ex)
            {
                ex.Flatten().Handle(HandleApiExceptions);
                return;
            }
            if (characters.Count == 0)
            {
                WriteLine($"No characters found with allegiance \"{allegiance}\"");
                return;
            }
            WriteCharacterTable(characters);
        }

        /// <summary>
        /// Prompts the user for a <see cref="Trilogy">trilogy</see>, then queries the API for
        /// <see cref="CharacterModel">character</see>s introduced in that <see
        /// cref="CharacterModel.TrilogyIntroducedIn">trilogy</see> and writes the information
        /// to the console.
        /// </summary>
        private static void LookupCharactersByTrilogy()
        {
            string trilogyInput = null;
            Trilogy? trilogy = null;
            while (trilogy is null)
            {
                while (string.IsNullOrEmpty(trilogyInput))
                {
                    Write("Enter a trilogy to look up:\n» ");
                    trilogyInput = ReadLine();
                }
                try
                {
                    trilogy = (Trilogy)
                        Enum.Parse(typeof(Trilogy), trilogyInput, ignoreCase: true);
                }
                catch (ArgumentException)
                {
                    WriteLine($"Invalid trilogy \"{trilogyInput}\"");
                    WriteLine(
                        "Valid trilogies: " + string.Join(", ", Enum.GetNames(typeof(Trilogy)))
                    );
                    trilogyInput = null;
                }
            }
            WriteLine();

            List<CharacterModel> characters;
            try
            {
                characters =
                    (List<CharacterModel>)
                        _api.ListCharactersByTrilogyAsync((Trilogy)trilogy).Result;
            }
            catch (AggregateException ex)
            {
                ex.Flatten().Handle(HandleApiExceptions);
                return;
            }
            if (characters.Count == 0)
            {
                WriteLine($"No characters found introduced in trilogy \"{trilogy}\"");
                return;
            }
            WriteCharacterTable(characters);
        }

        /// <summary>
        /// Writes a table of <see cref="CharacterModel">character</see> information to the
        /// console.
        /// </summary>
        /// <param name="characters">
        /// The <see cref="IEnumerable{CharacterModel}">collection</see> of <see
        /// cref="CharacterModel">characters</see> to write.
        /// </param>
        private static void WriteCharacterTable(IEnumerable<CharacterModel> characters)
        {
            // Find longest Id and Name from characters
            int idWidth = Max(2, characters.Max(c => c.Id.ToString().Length));
            int nameWidth = Max(4, characters.Max(c => c.Name.Length));
            const int jediWidth = 5;
            const int allegianceWidth = 10;
            const int trilogyWidth = 8;
            // Write table header
            WriteLine(
                " "
                    + string.Join(
                        " | ",
                        "Id".PadRight(idWidth),
                        "Name".PadRight(nameWidth),
                        "Jedi?".PadRight(jediWidth),
                        "Allegiance".PadRight(allegianceWidth),
                        "Trilogy".PadRight(trilogyWidth)
                    )
            );
            WriteLine(
                " "
                    + string.Join(
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
                    " "
                        + string.Join(
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
