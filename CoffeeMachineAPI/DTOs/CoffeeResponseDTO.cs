namespace CoffeeMachineAPI.DTOs;

public record CoffeeResponseDTO
{
    public string Message{get;set;} =string.Empty;
    public string Prepared{get;set;} =string.Empty;
}
