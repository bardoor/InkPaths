using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;


public class DBManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        using (var connection = new SqliteConnection("Data Source=playerinfo.db"))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "CREATE TABLE Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Age INTEGER NOT NULL)";
            command.ExecuteNonQuery();

            Debug.Log("Created table");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
