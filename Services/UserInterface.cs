using CodingTrackerApp.Models;
using Spectre.Console;

namespace CodingTrackerApp.Services;

public static class UserInterface
{
    public static void WelcomeMessage()
    {
        AnsiConsole.MarkupLine("[bold orange3]Welcome to the Code Tracking Application![/]\n");
        AnsiConsole.MarkupLine("[bold orange3]In this application you will keep track of coding sessions using a start and stop time. To Continue, please press Enter... [/]");
        Console.ReadKey();
    }

    public static string GetMainMenuChoice()
    {
        Console.Clear();
        string userInput;

        while (true)
        {
            var menuTitle = new Rule("[bold orange3]Main Menu[/]");
            menuTitle.Justification = Justify.Left;
            AnsiConsole.Write(menuTitle);
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[green]1[/] - Add an entry");
            AnsiConsole.MarkupLine($"[green]2[/] - View saved entries");
            AnsiConsole.MarkupLine($"[green]3[/] - Update an entry");
            AnsiConsole.MarkupLine($"[green]4[/] - Delete an entry");
            AnsiConsole.MarkupLine($"[red]X[/] - Exit the application");
            AnsiConsole.Markup("[green]Your Selection: [/]");
            userInput = Console.ReadLine().ToLower();

            switch (userInput)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "x":
                    return userInput;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid entry. Please type a [/][green]menu choice[/]");
                    break;
            }
        }
    }

    public static void ViewRecords()
    {
        AnsiConsole.MarkupLine("[bold blue]ViewRecords Method Reached! You entered:[/]");
    }

    public static Event GetInsertRecordInfo()
    {
        Console.Clear();
        string? userInput;
        DateTime parsedDate;
        bool isValid = false;
        string format = "MM-dd-yyyy-HH:mm";
        Event codeEvent = new();

        AnsiConsole.Markup($"[bold orange3]Please enter info about your coding session...[/]");

        codeEvent.StartTime = GetDateFromUser().ToString(); //Currently does not allow null values
        codeEvent.EndTime = GetDateFromUser().ToString(); //Currently does not allow null values

        AnsiConsole.Markup($"[bold orange3]Please enter any comments you would like to save...[/]");
        codeEvent.Details = Console.ReadLine();

        return codeEvent;
    }

    public static DateTime GetDateFromUser()
    {
        string? userInput;
        DateTime parsedDate;
        bool isValid = false;
        string format = "MM-dd-yyyy-HH:mm";

        while (true)
        {
            AnsiConsole.Markup($"[bold orange3]Enter time/date ({format} (24-hr) or enter [white on gray]NOW[/] for current time/date)[/]");
            userInput = Console.ReadLine();

            if (userInput == "NOW")
            {
                return DateTime.Now;
            }
            else if (isValid = DateTime.TryParseExact(userInput, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else if (userInput == null)
            {
                // TODO - Need to to allow for null values in the end time. This allows creating of a record for both before coding event has happened and after.
            }

                AnsiConsole.Markup($"[bold red]Invalid Entry, please try again...[/]");
        }
    }
}