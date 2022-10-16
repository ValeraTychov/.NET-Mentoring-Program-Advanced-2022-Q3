namespace OnlineShop.CatalogService.Domain.Models;

public interface IOperationResult
{
    bool IsSuccess { get; }

    string? Message { get; }
}

public interface IOperationResult<T> : IOperationResult
{
    T Value { get; }
}
