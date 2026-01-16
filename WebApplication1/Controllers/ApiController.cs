using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("ToDoLists")]
    public class ApiController : ControllerBase
    {

        private const string ConnectionString = "Data Source=TodoList.db";

        [HttpGet("GetLists")]
        public IActionResult Ping([FromHeader(Name = "UserToken")] string user_token)
        {
            using var db = new SqliteConnection(ConnectionString);
            db.Open();


            var selectCmd = db.CreateCommand();

            selectCmd.CommandText = "SELECT Id, ListText FROM UserLists WHERE Token = @token";
            selectCmd.Parameters.AddWithValue("@token", user_token);


            using var reader = selectCmd.ExecuteReader();
            var result = new List<ListToDo>();

            while (reader.Read())
            {
                result.Add(new ListToDo
                {
                    Id = reader.GetInt32(0),
                    Text = reader.GetString(1)
                });
            }
            return Ok(result);
        }


        [HttpPost("PostList")]
        public IActionResult Post([FromHeader(Name = "UserToken")] string user_token, [FromBody] string text)
        {
            using var db = new SqliteConnection(ConnectionString);
            db.Open();


            var tableCmd = db.CreateCommand();
            tableCmd.CommandText =

            @"CREATE TABLE IF NOT EXISTS UserLists 
            (Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Token TEXT NOT NULL,
            ListText TEXT NOT NULL)";
            tableCmd.ExecuteNonQuery();


            tableCmd.CommandText = "INSERT INTO UserLists (Token, ListText) VALUES (@token, @text)";
            tableCmd.Parameters.AddWithValue("@token", user_token);
            tableCmd.Parameters.AddWithValue("@text", text);
            tableCmd.ExecuteNonQuery();

            return Ok("List added");
        }
    }
}
