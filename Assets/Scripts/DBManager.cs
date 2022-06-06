using Mono.Data.Sqlite;
using UnityEngine;

public class DBManager : MonoBehaviour
{

    private string _dbGame = "URI=file:Game.db";

    public void CreateDB()
    {
        var connection = new SqliteConnection(_dbGame);
        var command = connection.CreateCommand();

        // Confirm database can be opened.
        try
        {
            connection.Open();
            //Debug.Log("Database successfully opened.");
        }
        catch
        {
            // Debug.Log("Database did not open successfully.");
        }

        // Create User Table
        command.CommandText = @"CREATE TABLE IF NOT EXISTS users (
        firstName VARCHAR(25),
        lastName VARCHAR(25),
        userName VARCHAR(25), 
        password VARCHAR(32))";

        try
        {
            command.ExecuteNonQuery();
            // Debug.Log("Table: users was created or already existed.");
        }
        catch
        {
            //Debug.Log("Table: users was not created.");
        }

        // Confirm database can be closed.
        try
        {
            connection.Close();
            //Debug.Log("Database successfully closed.");
        }
        catch
        {
            //Debug.Log("Database did not close successfully.");
        }
    }

    public bool GetUserAuthorization(string username, string passsword)
    {
        var connection = new SqliteConnection(_dbGame);
        var command = connection.CreateCommand();

        connection.Open();
        command.CommandText = "SELECT * FROM users WHERE userName='" + username + "' AND password='" + passsword + "'";
        SqliteDataReader rdr = command.ExecuteReader();
        
        if(rdr[0].ToString().Length > 0)
        {
            connection.Close();
            return true;
        }

        connection.Close();
        return false;
    }

}

