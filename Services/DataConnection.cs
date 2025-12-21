using CodingTrackerApp.Models;
using Dapper;
using Spectre.Console;
using System.Data.SQLite;
using System.Reflection;


namespace CodingTrackerApp.Services;

public class DataConnection
{
    string tableName = "event";
    //TODO - This throws an error. The computer cannot find the connection string with this code.
    //static string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString; 
    static string connectionString = "Data Source=codingtracker.db;Version=3;";
    public DataConnection()
    {
        try
        {
            CreateTable();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine(ex.InnerException?.ToString());
            Console.ReadKey();
        }
    }
    public void CreateTable()
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

    public void InsertRecord(Event _event)
    {
        string insertQuery = $"INSERT INTO {tableName} (StartTime, EndTime, Details) VALUES (@StartTime, @EndTime, @Details)";

        using (var conn = new SQLiteConnection(connectionString))
        {
            var rowsAffected = conn.Execute(insertQuery, _event);

            if (rowsAffected != 0)
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

    public void ViewAllRecords()
    {
        string selectAllQuery = $"SELECT * FROM {tableName}";
        using (var conn = new SQLiteConnection(connectionString))
        {
            var output = conn.Query<Event>(selectAllQuery).ToList();

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
    public void UpdateRecord(int ID)
    {
        Console.WriteLine($"ID: {ID}");
    }

    
}
