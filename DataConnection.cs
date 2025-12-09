using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Configuration;


namespace CodingTrackerApp;

public class DataConnection
{
    private static string databaseName = "codingtracker";
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

        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Execute(createTableText);
        }
    }
}
