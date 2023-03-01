using static System.Console;

namespace StarWars.ConsoleApp
{
    internal static class Program
    {
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
    }
}
