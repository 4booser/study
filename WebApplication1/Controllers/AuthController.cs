using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;


namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("new_list")]
    public class AuthController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            using var db = new SqliteConnection("Data Source=TodoList.db");
            db.Open();

            var cmd = new SqliteCommand("SELECT Id, list FROM Lists", db);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                int id = reader.GetInt32(0);       // колонка Id
                string text = reader.GetString(1); // колонка Text
                Console.WriteLine($"{id}: {text}");
            }

            return Ok("API работает");
        }

        [HttpPost("post")]
        public IActionResult Post(string _list)
        {
            using var db = new SqliteConnection("Data Source=TodoList.db");
            db.Open();

            Console.WriteLine(_list);


            new SqliteCommand(
                @"CREATE TABLE IF NOT EXISTS Lists (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Text TEXT NOT NULL
    )", db
            ).ExecuteNonQuery();

            new SqliteCommand($"INSERT INTO Lists (list) VALUES ('{_list}')", db).ExecuteNonQuery();

            return Ok("API работает");
        }
    }
}




 

