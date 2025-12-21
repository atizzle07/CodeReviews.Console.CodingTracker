using CodingTrackerApp.Models;
using Spectre.Console;
using System.Reflection;

namespace CodingTrackerApp.Services;

public static class UI
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
            AnsiConsole.MarkupLine($"[green]5[/] - View Reports");
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
        Event codeEvent = new();

        AnsiConsole.Markup($"[bold orange3]Please enter info about your coding session...[/]");

        codeEvent.StartTime = GetDateFromUser().ToString(); //Currently does not allow null values
        codeEvent.EndTime = GetDateFromUser().ToString(); //Currently does not allow null values

        AnsiConsole.Markup($"[bold orange3]Please enter any comments you would like to save...[/]");
        codeEvent.Details = Console.ReadLine();

        return codeEvent;
    }

    public static Event GetInsertRecordInfoImproved()
    {
        Console.Clear();
        Event codeEvent = new();

        codeEvent.StartTime = GetDateFromUserImproved("Start Time").ToString();
        codeEvent.EndTime = GetDateFromUserImproved("End Time").ToString();

    }

    public static DateTime GetDateFromUserImproved(string prompt)
    {
        DateOnly parsedDate;
        TimeOnly parsedTime;
        string input;
        bool isValid = false;
        Rule rule = new Rule($"[orange3]{prompt}[/]");

        input = AnsiConsole.Prompt(new TextPrompt<string>(""));
        if (input.ToLower() == "now")
            return DateTime.Now;

        do
        {
            string date = AnsiConsole.Prompt(new TextPrompt<string>("Enter Date [MM-dd-YYYY]"));
            if (DateOnly.TryParseExact(date, "MM-dd-YYYY", out parsedDate))
                isValid = true;
            else
                AnsiConsole.MarkupLine("[bold red]Invalid entry. Please try again...[/]");
        }
        while (!isValid);

        do
        {
            string time = AnsiConsole.Prompt(new TextPrompt<string>("Enter 24HR Time [HH:MM]"));
            if (TimeOnly.TryParseExact(time, "HH:MM", out parsedTime))
                isValid = true;
            else
                AnsiConsole.MarkupLine("[bold red]Invalid entry. Please try again...[/]");
        }
        while (!isValid);

        return parsedDate.ToDateTime(parsedTime);
    }

    public static DateTime GetDateFromUser() // REMOVE - This method works but has been superceded by the improved method.
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

    public static void BuildObjTable(List<Event> events)
    {
        Table table = new();
        Event e = new(); // Create new instance of Event to build dynamic table
        foreach (PropertyInfo prop in e.GetType().GetProperties())
        {
            table.AddColumns(prop.Name);
        }

        foreach (var item in events)
        {
            table.AddRow([
                Convert.ToString(item.ID) ?? "",
                Convert.ToString(item.StartTime) ?? "",
                Convert.ToString(item.EndTime) ?? "",
                Convert.ToString(item.Details) ?? ""
                ]);
        }

        AnsiConsole.Write(table);
        Console.ReadKey();
    }
}