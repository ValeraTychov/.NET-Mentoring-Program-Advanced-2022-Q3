namespace OnlineShop.CatalogService.Domain.Models;

public class OperationResult : IOperationResult
{
    public bool IsSuccess { get; init; }

    public string? Message { get; init; }

    public static IOperationResult Success()
    {
        return new OperationResult
        {
            IsSuccess = true,
        };
    }

    public static IOperationResult Fail(string message)
    {
        return new OperationResult
        {
            IsSuccess = false,
            Message = message
        };
    }
}
