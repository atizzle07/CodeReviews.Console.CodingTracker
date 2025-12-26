using CodingTrackerApp.Models;
using Dapper;
using Spectre.Console;
using System.Data.SQLite;


namespace CodingTrackerApp.Services;

public static class DataConnection
{
    static readonly string tableName = "event";
    //TODO - Need to add config connection logic. This code throws an error. The computer cannot find the connection string.
    //static string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString; 
    static readonly string connectionString = "Data Source=codingtracker.db;Version=3;";

    public static void CreateTable()
    {
        string createTableText = @$"CREATE TABLE IF NOT EXISTS {tableName}(
                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                StartTime TEXT,
                                Endtime TEXT,
                                Details TEXT
                                )";

        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Execute(createTableText);
        }
    }

    public static void InsertRecord()
    {
        Console.Clear();
        Event newEvent = UI.GetNewRecordInfo();
        string insertQuery = $"INSERT INTO {tableName} (StartTime, EndTime, Details) VALUES (@StartTime, @EndTime, @Details)";

        using (var conn = new SQLiteConnection(connectionString))
        {
            var rowsAffected = conn.Execute(insertQuery, newEvent);
            if (rowsAffected != 0) // TODO - Change these to UI.OperationFailed() and UI.OperationSucceeded() methods to move UI logic out of this class
            {
                AnsiConsole.MarkupLine($"[bold green]Insert successful. Rows inserted:[/] {rowsAffected}");
                Console.ReadKey();
            }
            else
            {
                AnsiConsole.MarkupLine($"[bold red]Insert failed. Rows inserted:[/] {rowsAffected}");
                Console.ReadKey();
            }
        }
    }

    public static void ViewAllRecords() //TODO - change to "GetAllRecords" and list<event> instead of void to move UI logic out of this class
    {
        string selectQuery = $"SELECT * FROM {tableName}";
        using (var conn = new SQLiteConnection(connectionString))
        {
            var output = conn.Query<Event>(selectQuery).ToList();

            if (output.Count() == 0)
            {
                AnsiConsole.MarkupLine($"[bold red]No Records Found[/]");
                Console.ReadKey();
            }
            else
            {
                UI.BuildObjTable(output);
            }
        }
    }

    public static List<int> GetAllRecordIDs()
    {
        string selectRecordIDQuery = $"SELECT ID FROM {tableName}";
        using (var conn = new SQLiteConnection(connectionString))
        {
            var output = conn.Query<int>(selectRecordIDQuery).ToList();
            return output;
        }
    }

    public static void UpdateRecord()
    {
        string updateQuery = $"UPDATE {tableName} SET StartTime = @StartTime, EndTime = @EndTime, Details = @Details WHERE ID = @ID";
        ViewAllRecords();
        int updateRecord = UI.GetValidRecordID();
        Event codeEvent = UI.GetNewRecordInfo();
        codeEvent.ID = updateRecord;

        using (var conn = new SQLiteConnection(connectionString))
        {
            var rowsAffected = conn.Execute(updateQuery, codeEvent);

            if (rowsAffected != 0)
            {
                AnsiConsole.MarkupLine($"[bold green]Update successful.[/]");
                Console.ReadKey();
            }
            else
            {
                AnsiConsole.MarkupLine($"[bold red]Update failed.[/]");
                Console.ReadKey();
            }
        }

    }

    public static void DeleteRecord()
    {
        ViewAllRecords();
        Rule rule = new Rule("[bold red]Delete Record[/]").LeftJustified();
        AnsiConsole.Write(rule);
        //AnsiConsole.MarkupLine("[red]Delete Record[/]");
        int id = UI.GetValidRecordID();
        string deleteQuery = $"DELETE FROM {tableName} WHERE ID = @ID";

        using (var conn = new SQLiteConnection(connectionString))
        {
            int rowsAffected = conn.Execute(deleteQuery, new { ID = id });

            if (rowsAffected != 0)
            {
                AnsiConsole.Markup($"[bold green]Delete successful. Rows deleted:[/] {rowsAffected}");
                Console.ReadKey();
            }
            else
            {
                AnsiConsole.Markup($"[bold red]Delete failed. Rows deleted:[/] {rowsAffected}");
                Console.ReadKey();
            }
        }
    }

    public static void GetTopRecords(int records)
    {

    }

    public static List<Event> GetTopResults(int numRecords)
    {
        string query = $"SELECT * FROM {tableName}";
        List<Event> output = new();
        using (var conn = new SQLiteConnection(connectionString))
        {
            output = conn.Query<Event>(query).ToList();
        }

        if (output.Count() == 0)
        {
            AnsiConsole.MarkupLine($"[bold red]No Records Found[/]");
            Console.ReadKey();
        }
        else
        {
            output = output.OrderByDescending(x => x.DurationMinutes)
                .Take(numRecords)
                .ToList();
        }
        return output;

    }
}