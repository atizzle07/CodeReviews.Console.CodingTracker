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
    static string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
    public DataConnection()
    {
        CreateTable();
    }
    public void CreateTable()
    {
        string createTableText = @$"CREATE TABLE IF NOT EXISTS {tableName}(
                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                StartTime TEXT,
                                Endtime TEXT,
                                Details TEXT
                                )";

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
        string insertQuery = $"INSERT INTO {tableName} (StartTime, Endtime, Details) VALUES (@StartTime, @Endtime, @Details";

        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Query(insertQuery, _event);
        }
    }

    public void UpdateRecord(int ID)
    {
        Console.WriteLine($"ID: {ID}");
    }
}
