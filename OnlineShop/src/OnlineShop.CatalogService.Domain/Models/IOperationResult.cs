namespace OnlineShop.CatalogService.Domain.Models;

public interface IOperationResult
{
    bool Success { init; }

    string Message { init; }
}

public interface IOperationResult<T> : IOperationResult
{
    T Value { init; }
}
