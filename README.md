StarWars.ConsoleApp
===================

User Story #211926: API Proxies: Create and use API Proxy for Star Wars
API
------------------------------------------------------------------------

> Create an API Proxy for your Star Wars API (US #211916, #211918,
> #211920).
>
> - The API Proxy should be in .Net Standard 2.0.
> - Be sure to add at least one Unit Test to the API Proxy.
> - You should create a Nuget Package from the Proxy and publish it
>   to Local Nuget.
> - Create a simple console app that pulls in the Nuget package and uses
>   the Proxy methods to:
>   - [x] Get a list of all the character information
>   - [x] Get a character's information by their name
>   - [x] Get a list of character information by allegiance
>   - [x] Get a list of character information by trilogy
> - [x] The console app should have a main menu where you can choose which
>   of the above you want to do.
> - [x] Once done, it should ask if you want to do it again. If so,
>   represent the main menu.
> - Do NOT call the API directly - use the proxy methods from the Nuget
>   package!

User Story #211922: Calling APIs: Call Star Wars API
---------------------------------------------------

> Let's call our Star Wars API!
>
> - Write a console application that calls the Star Wars WebAPI you
>   created earlier and set up in IIS
> - [x] It should present the user a menu of options.
> - They can:
>   - [x] Get a list of all the character information
>   - [x] Get a character's information by their name
>   - [x] Get a list of character information by allegiance
>   - [x] Get a list of character information by trilogy
> - [x] Once the user select an option from the menu, prompt them for
>   any additional information needed.
> - [x] Then call your Star Wars WebAPI and display the appropriate
>   returned data on the Console.
> - [x] After it's done, it should ask the user if they want to do it
>   again.
> - [x] If they answer Yes, it should present the Main Menu again.
