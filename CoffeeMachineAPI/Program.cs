using CoffeeMachineAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string apiKey= "aefa89ddb40bc09ad524ecaeef1afec1";
const string city = "Manila"; // Or use lat/lon
const string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
int totalNumberOfCalls=0;
app.MapGet("/", () => "Hello Coffee is Ready!");
app.MapGet("/brew-coffee", () =>
{
    var DateToday= DateTime.Now;
    totalNumberOfCalls++;
    if (totalNumberOfCalls % 5 == 0)
    {
        return Results.StatusCode(503);
    }
    else if(DateToday.Month==4 && DateToday.Day == 1)
    {
        return Results.StatusCode(418);
    }
    else
    {
        var response = new CoffeeResponseDTO
        {
        Message = "Your piping hot coffee is ready",
        Prepared = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz").Replace(":", "")
        };
        return Results.Ok(response);
        
    }

});
app.MapGet("/weather", async () =>
{
    using var client = new HttpClient();
    var response = await client.GetFromJsonAsync<dynamic>(url);
    double temp = response?.GetProperty("main").GetProperty("temp").GetDouble();
    return Results.Ok($"Temperature today in {city} is {temp}");
});
app.Run();
