using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using System;
    

public class ScoresDB : MonoBehaviour
{
    private string connectionString;

    void Start()
    {
        // Path to your SQLite database file
        string pathToDatabase = Application.dataPath + "/Scores.db";
        connectionString = $"URI=file:{pathToDatabase}";

        // Ensure the Scores table exists
        CreateScoresTable();
    }

    // Function to execute queries
    private void ExecuteQuery(string query)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqliteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    // Create the Scores table if it doesn't exist
    private void CreateScoresTable()
    {
        string createTableQuery = "CREATE TABLE IF NOT EXISTS Scores (ID INTEGER PRIMARY KEY AUTOINCREMENT, Score INTEGER)";
        ExecuteQuery(createTableQuery);
    }

    // Insert a score into the Scores table
    public void InsertScore(int score)
    {
        string insertQuery = $"INSERT INTO Scores (Score) VALUES ({score})";
        ExecuteQuery(insertQuery);
    }

    // Get the latest score from the Scores table
    public int GetLatestScore()
    {
        int latestScore = 0;

        string query = "SELECT Score FROM Scores ORDER BY ID DESC LIMIT 1";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = new SqliteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        latestScore = reader.GetInt32(0);
                    }
                }
            }

            connection.Close();
        }

        return latestScore;
    }
      public List<int> GetHighestScores()
    {
        List<int> highestScores = new List<int>();

        string query = "SELECT Score FROM Scores ORDER BY Score DESC LIMIT 5";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = new SqliteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        highestScores.Add(reader.GetInt32(0));
                    }
                }
            }

            connection.Close();
        }

        return highestScores;
    }
}
