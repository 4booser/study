using Microsoft.Data.Sqlite;
using System;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    class Program
    {
        

        static void Main(string[] args)
        {

            User leha = new User();
            leha.hit(15);
            Console.WriteLine(leha.Health);

            using var connection = new SqliteConnection("Data Source=MyDatabase.db");
            connection.Open();




            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
            @"
    CREATE TABLE IF NOT EXISTS UserLists (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Token TEXT NOT NULL,
    ListText TEXT NOT NULL
);
";
            tableCmd.ExecuteNonQuery();





            string userToken = "abc1233ппН";

            // Добавление текста
            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = "INSERT INTO UserLists (Token, ListText) VALUES (@token, @text)";
            insertCmd.Parameters.AddWithValue("@token", userToken);
            insertCmd.Parameters.AddWithValue("@text", "Привет! Это текст для пользователя.");
            insertCmd.ExecuteNonQuery();

            // Получение текста по токену
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT ListText FROM UserLists WHERE Token = @token";
            selectCmd.Parameters.AddWithValue("@token", userToken);

            using var reader = selectCmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }


            connection.Close();
        }
    }
}