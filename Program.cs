using CodingTrackerApp.Services;
using Spectre.Console;


namespace CodingTrackerApp;

public class Program
{
    public static void Main(string[] args)
    {
        DataConnection.CreateTable();
        string menuChoice;
        UI.WelcomeMessage();

        do
        {
            menuChoice = UI.GetMainMenuChoice();

            switch (menuChoice)
            {
                case "1":
                    DataConnection.InsertRecord();
                    break;
                case "2":
                    DataConnection.ViewAllRecords();
                    break;
                case "3":
                    DataConnection.UpdateRecord();
                    Console.ReadKey();
                    break;
                case "4":
                    DataConnection.DeleteRecord();
                    break;
                case "5":
                    UI.ReportsMenu();
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