using OnlineShop.Messaging.Abstraction.Entities;
using OnlineShop.Messaging.Service.Extensions;

namespace OnlineShop.Messaging.Service.Storage;

public class SubscriptionStorage
{
    private readonly Dictionary<Type, object> _handlers = new();

    public void Subscribe<TEventParameters>(Action<TEventParameters> newHandler) where TEventParameters : EventParameters
    {
        if (!_handlers.TryGetValueAs(typeof(TEventParameters), out Action<TEventParameters>? existedHandler))
        {
            _handlers.Add(typeof(TEventParameters), newHandler);
            return;
        }

        var combinedHandler = existedHandler + newHandler;
        _handlers[typeof(TEventParameters)] = combinedHandler;
    }

    public bool TryInvoke<TEventParameters>(TEventParameters parameters)
    {
        if (!_handlers.TryGetValueAs(typeof(TEventParameters), out Action<TEventParameters>? handler))
        {
            return false;
        }

        handler!.Invoke(parameters);

        return true;
    }
}