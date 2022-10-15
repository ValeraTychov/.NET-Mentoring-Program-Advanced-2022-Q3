namespace OnlineShop.CatalogService.Domain.Models;

public class OperationResult : IOperationResult
{
    public bool Success { get; init; }

    public string Message { get; init; }
}
