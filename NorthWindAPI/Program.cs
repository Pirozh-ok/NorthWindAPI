using NorthWindAPI.Services.Implementations;
using NorthWindAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddRouting();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseHttpsRedirection();
app.MapDefaultControllerRoute();





/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Customers}/{action=Get}/{id?}");
});
*/
/*var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});*/

/*app.MapGet("/vanya", ()=> 
{
    var data = new List<string>();
    var connectionString = "Server = (localdb)\\mssqllocaldb; Database = northwind; Persist Security Info = false; User ID = 'sa'; Password = 'sa'; MultipleActiveResultSets = True; Trusted_Connection = False;";
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        SqlCommand command = new SqlCommand("select * from dbo.Customers", connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            data.Add(reader["ContactName"].ToString());
        }
    }
    return data.ToArray();
});*/
/*app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
});*/

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
/*Scaffold-DbContext "Server=.\SQLExpress; Database=northwind;Trusted_Connection=True;"*/

/*Scaffold-DbContext "Server = (localdb)\mssqllocaldb; Database = northwind; Persist Security Info = false; User ID = 'sa'; Password = 'sa'; MultipleActiveResultSets = True; Trusted_Connection = False;" Microsoft.EntityFrameworkCore.SQLserver -OutputDir Models -Tables Customers, Order Details, Orders, Products*/