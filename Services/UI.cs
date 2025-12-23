using CodingTrackerApp.Data;
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
    public static MenuOption GetMainMenuChoice()
    {
        Console.Clear();

        //TODO - Put this menu selection in a frame
        var userInput = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOption>()
            .Title("Please select a menu Option:")
            .AddChoices(new[] {
                MenuOption.AddEntry,
                MenuOption.ViewSavedEntries,
                MenuOption.UpdateEntry,
                MenuOption.DeleteEntry,
                MenuOption.Reports
            }));
        return userInput;
    }
    public static Event GetNewRecordInfo()
    {
        //Console.Clear();
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

        input = AnsiConsole.Prompt(
            new TextPrompt<string>("To save the current date/time, enter [blue on yellow]NOW[/] or press [black on white]ENTER[/] to continue.")
            .AllowEmpty());
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
                Convert.ToString(item.Details) ?? "",
                Convert.ToString(item.Duration) ?? ""
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
    static public void ReportsMenu()
    {
        Console.Clear();

        //TODO - Put this menu selection in a frame
        var userInput = AnsiConsole.Prompt(
            new SelectionPrompt<Reports>()
            .Title("Please select a report to view:")
            .AddChoices(new[] {
                Reports.TopResults,
                Reports.AverageTime,
                Reports.TotalPerMonth,
                Reports.TotalPerYear,
                Reports.WeeklyBarCount,
                Reports.WeeklyBarTotal,
                Reports.MonthlyBarCount,
                Reports.MonthlyBarTotal
            }));

        switch (userInput)
        {
            case Reports.TopResults:
                DisplayTopResults();
                break;
            case Reports.AverageTime:
                break;
            case Reports.TotalPerMonth:
                break;
            case Reports.TotalPerYear:
                break;
            case Reports.WeeklyBarCount:
                break;
            case Reports.WeeklyBarTotal:
                break;
            case Reports.MonthlyBarCount:
                break;
            case Reports.MonthlyBarTotal:
                break;
            default:
                break;
        }
    }

    static public void DisplayTopResults()
    {
        // View top x longest coding sessions
        int records;
        do
        {
            records = AnsiConsole.Prompt(
            new TextPrompt<int>("Please select the amount of records to display (max 5): "));
        }
        while (records < 0 && records > 5);

        var top3 = DataConnection.GetTopResults(records);

        BuildObjTable(top3);

    }
}