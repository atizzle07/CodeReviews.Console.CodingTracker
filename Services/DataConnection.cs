using CodingTrackerApp.Models;
using Dapper;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Text;
using System.Xml.Linq;


namespace CodingTrackerApp.Services;

public class DataConnection
{
    string tableName = "event";
    static string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString; //TODO - This throws an error. The computer cannot find the connection string with this code.
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

        Console.WriteLine("Before Create");
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            // TODO - Need to refactor this to Dapper execution. Currently in ADO.net format.
            conn.Open();
            conn.Execute(createTableText);
            conn.Close();
        }
    }

    public void InsertRecord(Event _event)
    {
        string insertQuery = $"INSERT INTO {tableName} (StartTime, Endtime, Details) VALUES (@StartTime, @Endtime, @Details)";

        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            conn.Query(insertQuery, _event);// TODO - This does not work, debuigger says insufficient data provided. Likely need to include the ID field
            conn.Close();
        }
    }

    public void UpdateRecord(int ID)
    {
        Console.WriteLine($"ID: {ID}");
    }
}
