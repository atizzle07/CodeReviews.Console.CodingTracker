using CodingTrackerApp.Data;
using CodingTrackerApp.Models;
using Spectre.Console;
using System.Globalization;
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
        var userInput = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOption>()
            .Title("Please select a menu Option:")
            .AddChoices(new[] {
                MenuOption.AddEntry,
                MenuOption.ViewSavedEntries,
                MenuOption.UpdateEntry,
                MenuOption.DeleteEntry,
                MenuOption.ExitApplication
            }));
        return userInput;
    }
    public static Event GetNewRecordInfo()
    {
        bool isValid;
        Event codeEvent = new();

        DateTime _startTime = GetDateFromUser("Start Time");
        codeEvent.StartTime = _startTime.ToString();
        do
        {
            isValid = false;
            DateTime _endTime = GetDateFromUser("End Time");
            if (_endTime >= _startTime)
            {
                isValid = true;
                codeEvent.EndTime = _endTime.ToString();
            }
            else
                AnsiConsole.MarkupLine("[bold red]End date/time is before start. Please enter a valid date/time...[/]");
        }
        while (isValid == false);


        AnsiConsole.Markup($"[bold orange3]Please enter any comments you would like to save...[/]\n");
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
                isValid = false;
                string date = AnsiConsole.Ask<string>("Enter Date [[MM-dd-YYYY]]");
                if (DateOnly.TryParseExact(date, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                    isValid = true;
                else
                    AnsiConsole.MarkupLine("[bold red]Invalid entry. Please try again...[/]");
            } while (!isValid);

            do
            {
                isValid = false;
                string time = AnsiConsole.Ask<string>("Enter 24HR Time [[HH:MM]]");
                if (TimeOnly.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedTime))
                    isValid = true;
                else
                    AnsiConsole.MarkupLine("[bold red]Invalid entry. Please try again...[/]");
            } while (!isValid);

            return parsedDate.ToDateTime(parsedTime);
        }
    }
    public static void BuildViewTable(List<Event> events)
    {
        Table table = new();
        Event e = new(); // Create new instance of Event to build dynamically sized table
        foreach (PropertyInfo prop in e.GetType().GetProperties())
        {
            table.AddColumns(prop.Name);
        }

        foreach (var item in events)
        {
            table.AddRow(
                Convert.ToString(item.ID) ?? "",
                Convert.ToString(item.StartTime) ?? "",
                Convert.ToString(item.EndTime) ?? "",
                Convert.ToString(item.Details) ?? "",
                Convert.ToString(item.DurationMinutes) ?? "");
        }

        AnsiConsole.Write(table);
    }
    public static int GetValidRecordID()
    {
        do
        {
            string id = AnsiConsole.Ask<string>("Enter Record ID:");
            if (int.TryParse(id, out int parsedID))
            {
                if (Validation.IsValidId(parsedID))
                    return parsedID;
            }
            else continue;
        }
        while (true);
    }
}