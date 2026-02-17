var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Coffee is Ready!");
app.MapGet("/brew-coffee",()=>"Your Piping hot coffee is ready");
app.Run();
