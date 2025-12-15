using CodingTrackerApp;
using CodingTrackerApp.Services;
using Spectre.Console;
using System.Security.Principal;


namespace CodingTrackerApp;

public class Program
{

    DataConnection conn = new();
    static void Main(string[] args)
    {
        string menuChoice;
        UserInterface.WelcomeMessage();

        do
        {
            menuChoice = UserInterface.GetMainMenuChoice();

            switch (menuChoice)
            {
                case "1":
                    AnsiConsole.MarkupLine($"[orange3]You selected {menuChoice}[/]");
                    Console.ReadKey();
                    break;
                case "2":
                    AnsiConsole.MarkupLine($"[orange3]You selected {menuChoice}[/]");
                    Console.ReadKey();
                    break;
                case "3":
                    AnsiConsole.MarkupLine($"[orange3]You selected {menuChoice}[/]");
                    Console.ReadKey();
                    break;
                case "4":
                    AnsiConsole.MarkupLine($"[orange3]You selected {menuChoice}[/]");
                    Console.ReadKey();
                    break;
                case "x":
                    AnsiConsole.MarkupLine("[orange3]Exiting Application... Press Enter to Continue[/]");
                    Console.ReadKey();
                    break;
                default:
                    AnsiConsole.MarkupLine("[bold red]Invalid Entry. Please make a selection from the menu[/]");
                    break;
            }
        }
        while (menuChoice != "x");
    }
}