using CoffeeMachineAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
int totalNumberOfCalls=0; // i think this might cause some issues if it were a real environment but for this case I think is fine
app.MapGet("/", () => "Hello Coffee is Ready!");
app.MapGet("/brew-coffee", () =>
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
            var response = new CoffeeResponseDTO  
            {
            Message = "Your piping hot coffee is ready",
            Prepared = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz").Replace(":", "")
            };
            return Results.Ok(response);
    }

});
app.Run();
