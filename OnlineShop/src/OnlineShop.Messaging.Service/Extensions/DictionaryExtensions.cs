namespace OnlineShop.Messaging.Service.Extensions;

public static class DictionaryExtensions
{
    public static bool TryGetValueAs<TKey, TValue, TValueAs>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValueAs? valueAs)
        where TValueAs : class, TValue

    {
        if (!dictionary.TryGetValue(key, out var value))
        {
            valueAs = default;
            return false;
        }

        valueAs = value as TValueAs;
        
        return valueAs != null;
    }
}