using OnlineShop.Messaging.Service.Extensions;

namespace OnlineShop.Messaging.Service.Storage;

public class SubscriptionStorage
{
    private readonly Dictionary<Type, object> _handlers = new();

    public void Subscribe<TEventArgs>(EventHandler<TEventArgs> newHandler)
    {
        if (!_handlers.TryGetValueAs(typeof(TEventArgs), out EventHandler<TEventArgs>? existedHandler))
        {
            _handlers.Add(typeof(TEventArgs), newHandler);
            return;
        }

        var combinedHandler = existedHandler + newHandler;
        _handlers[typeof(TEventArgs)] = combinedHandler;
    }

    public bool TryInvoke<TEventArgs>(TEventArgs parameters)
    {
        if (!_handlers.TryGetValueAs(typeof(TEventArgs), out EventHandler<TEventArgs>? handler))
        {
            return false;
        }

        handler!.Invoke(this, parameters);

        return true;
    }
}