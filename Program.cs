using CodingTrackerApp.Data;
using CodingTrackerApp.Services;
using Spectre.Console;


namespace CodingTrackerApp;

public class Program
{
    public static void Main(string[] args)
    {
        DataConnection.CreateTable();
        MenuOption option;
        UI.WelcomeMessage();

        do
        {
            option = UI.GetMainMenuChoice();

            switch (option)
            {
                case MenuOption.AddEntry:
                    DataConnection.InsertRecord();
                    break;
                case MenuOption.ViewSavedEntries:
                    DataConnection.ViewAllRecords();
                    break;
                case MenuOption.UpdateEntry:
                    DataConnection.UpdateRecord();
                    break;
                case MenuOption.DeleteEntry:
                    DataConnection.DeleteRecord();
                    break;
                case MenuOption.Reports:
                    UI.ReportsMenu();
                    break;
                case MenuOption.ExitApplication:
                    AnsiConsole.MarkupLine("[orange3]Exiting Application... Press Enter to Continue[/]");
                    break;
            }
        }
        while (option != MenuOption.ExitApplication);
    }
}