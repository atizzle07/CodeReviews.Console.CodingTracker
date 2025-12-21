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
            Console.WriteLine("\n\n");
            switch (userInput)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "x":
                    return userInput;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid entry. Please type a [/][green]menu choice[/]");
                    break;
            }
        }
    }
    static public void ReportsMenu()
    {
        AnsiConsole.MarkupLine("[bold red]This feature is not completed yet. Please come back later.[/]");
        Console.ReadKey();

        // View top 5 longest coding sessions
        // View Average session time
        // Challenge Requirement - total and average coding session per period. Allow user to pick grouping by day / month / year

    }
    public static Event GetNewRecordInfo() //TODO - adjust name to remove improved
    {
        Console.Clear();
        Event codeEvent = new();

        codeEvent.StartTime = GetDateFromUser("Start Time").ToString();
        codeEvent.EndTime = GetDateFromUser("End Time").ToString();

        AnsiConsole.Markup($"[bold orange3]Please enter any comments you would like to save...[/]");
        codeEvent.Details = Console.ReadLine();
        return codeEvent;
    }
    public static DateTime GetDateFromUser(string prompt)
    {
        DateOnly parsedDate;
        TimeOnly parsedTime;
        string input;
        bool isValid = false;
        Rule rule = new Rule($"[orange3]{prompt}[/]");
        AnsiConsole.Write(rule);

        input = AnsiConsole.Ask<string>("To save the current date/time, enter [blue on yellow]NOW[/] or press any key to continue.");
        if (input.ToLower() == "now")
            return DateTime.Now;
        else
        {
            do
            {
                string date = AnsiConsole.Ask<string>("Enter Date [[MM-dd-YYYY]]");
                if (DateOnly.TryParseExact(date, "MM-dd-yyyy", out parsedDate))
                {
                    // ERROR CHECK. Error if:
                    // date is in the future
                    // check for months between 1-12, days between 1-31
                    isValid = true;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Invalid entry. Please try again...[/]");
                }
            } while (!isValid);

            do
            {
                string time = AnsiConsole.Ask<string>("Enter 24HR Time [[HH:MM]]");
                if (TimeOnly.TryParseExact(time, "HH:mm", out parsedTime))
                    isValid = true;
                else
                    AnsiConsole.MarkupLine("[bold red]Invalid entry. Please try again...[/]");
            } while (!isValid);

            return parsedDate.ToDateTime(parsedTime);
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
    public static int GetValidRecordID()
    {
        do
        {
            string id = AnsiConsole.Ask<string>("Enter Record ID:");
            if (int.TryParse(id, out int parsedID))
            {
                if (Validation.isValidID(parsedID))
                    return parsedID;
            }
            else
            {
                continue;
            }
        }
        while (true);
    }
}