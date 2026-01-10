using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class ListDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    [ApiController]
    [Route("new_list")]
    public class AuthController : ControllerBase
    {
        // ------------------------------
        // 1. Здесь объявляем строку подключения
        // ------------------------------
        private const string ConnectionString = "Data Source=TodoList.db";

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            // ------------------------------
            // 2. Используем переменную ConnectionString
            // ------------------------------
            using var db = new SqliteConnection(ConnectionString);
            db.Open();

            var result = new List<ListDto>();

            using var cmd = new SqliteCommand(
                @"SELECT l.Id, l.Text
                  FROM Lists l
                  JOIN UserTokens u ON u.Id = l.UserTokenId
                  WHERE u.Token = @token", db);

            cmd.Parameters.AddWithValue("@token", "abc1231");

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new ListDto
                {
                    Id = reader.GetInt32(0),
                    Text = reader.GetString(1)
                });
            }

            return Ok(result);
        }

        [HttpPost("post")]
        public IActionResult Post(string text)
        {
            using var db = new SqliteConnection(ConnectionString);
            db.Open();

            // ------------------------------
            // 3. Создание таблиц, если их нет
            // ------------------------------
            new SqliteCommand(
            @"CREATE TABLE IF NOT EXISTS UserTokens (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Token TEXT NOT NULL UNIQUE
            )", db).ExecuteNonQuery();

            new SqliteCommand(
            @"CREATE TABLE IF NOT EXISTS Lists (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserTokenId INTEGER NOT NULL,
                Text TEXT NOT NULL,
                FOREIGN KEY (UserTokenId) REFERENCES UserTokens(Id)
            )", db).ExecuteNonQuery();

            // ------------------------------
            // 4. Вставка токена, если его нет
            // ------------------------------
            new SqliteCommand(
                @"INSERT OR IGNORE INTO UserTokens(Token)
                  VALUES ('abc1231')", db).ExecuteNonQuery();

            // ------------------------------
            // 5. Получение Id токена
            // ------------------------------
            var getTokenId = new SqliteCommand(
                @"SELECT Id FROM UserTokens WHERE Token = 'abc1231'", db);

            var userTokenId = (long)getTokenId.ExecuteScalar();

            // ------------------------------
            // 6. Вставка нового списка
            // ------------------------------
            using var insertCmd = new SqliteCommand(
                @"INSERT INTO Lists (UserTokenId, Text)
                  VALUES (@userTokenId, @text)", db);

            insertCmd.Parameters.AddWithValue("@userTokenId", userTokenId);
            insertCmd.Parameters.AddWithValue("@text", text);
            insertCmd.ExecuteNonQuery();

            return Ok("List added");
        }
    }
}
