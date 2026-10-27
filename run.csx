using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Data.SqlClient;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");
    var cities = new List<string>();
    var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();
        string query = "SELECT CityName FROM Cities";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    cities.Add(reader.GetString(0));
                }
            }
        }
    }
    return new OkObjectResult(cities);
}
