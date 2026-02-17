var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string apiKey= "aefa89ddb40bc09ad524ecaeef1afec1";
const string city = "Manila"; // Or use lat/lon
const string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
app.MapGet("/", () => "Hello Coffee is Ready!");
app.MapGet("/brew-coffee",()=>"Your Piping hot coffee is ready");
app.MapGet("/weather", async () =>
{
    using var client = new HttpClient();
    var response = await client.GetFromJsonAsync<dynamic>(url);
    double temp = response?.GetProperty("main").GetProperty("temp").GetDouble();
    return Results.Ok($"Temperature today in {city} is {temp}");
});
app.Run();
