using CodingTrackerApp.Models;
using CodingTrackerApp.Services;
using Spectre.Console;


namespace CodingTrackerApp;

public class Program
{
    public static void Main(string[] args)
    {
        DataConnection conn = new();
        string menuChoice;
        UI.WelcomeMessage();

        do
        {
            menuChoice = UI.GetMainMenuChoice();

            switch (menuChoice)
            {
                case "1":
                    InsertNewRecord();
                    break;
                case "2":
                    conn.ViewAllRecords();
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


        void InsertNewRecord()
        {
            Event newEvent = UI.GetInsertRecordInfo();
            conn.InsertRecord(newEvent);
        }
    }
}