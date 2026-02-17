using CoffeeMachineAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string apiKey= "aefa89ddb40bc09ad524ecaeef1afec1";
const string city = "Manila";
const string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
int totalNumberOfCalls=0; // i think this might cause some issues if it were a real environment but for this case I think is fine
app.MapGet("/", () => "Hello Coffee is Ready!");
app.MapGet("/brew-coffee", async () =>
{
    var DateToday= DateTime.Now;
    
    if(DateToday.Month==4 && DateToday.Day == 1) //Safety Check
    {
        return Results.StatusCode(418);
    }
    totalNumberOfCalls++;
    if (totalNumberOfCalls % 5 == 0) //Safety Check
    {
        return Results.StatusCode(503);
    }
    else // this is where we make coffee
    {
        
        using var client = new HttpClient();
        var weather = await client.GetFromJsonAsync<dynamic>(url);
        double temp = weather?.GetProperty("main").GetProperty("temp").GetDouble();

        if (temp > 30)
        {
            var response = new CoffeeResponseDTO // Think I can make this better 
            {
            Message = "Your refreshing iced coffee is ready",
            Prepared = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz").Replace(":", "")
            };
            return Results.Ok(response);
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
