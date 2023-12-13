using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using UnityEditor.MemoryProfiler;


public class DBManager : MonoBehaviour
{
    private SqliteConnection _connection;

    void Awake()
    {
        _connection = new SqliteConnection("Data Source=playerinfo.db");

        _connection.Open();

        SqliteCommand command = new SqliteCommand();
        command.Connection = _connection;
        command.CommandText = "" +
            "CREATE TABLE IF NOT EXISTS PlayerInfo" +
            "(" +
            "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
            "LeverNumber INTEGER NOT NULL," +
            "Time INTEGER NOT NULL," +
            "StarsCount INTEGEG NOT NULL" +
            ")";
        command.ExecuteNonQuery();
    }

    public void AddNewRecord(int levelNumber, int time, int startCount)
    {
        SqliteCommand command = new SqliteCommand();
        command.Connection = _connection;
        command.CommandText = "" +
            $"INSERT INTO PlayerInfo (LevelNumber, Time, StarsCount) VALUES ({levelNumber}, {time}, {startCount})";
        int number = command.ExecuteNonQuery();
        Debug.Log($"DBManager: added {number} record(s) to database");
    }
    
    public void Disconnect()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
}
