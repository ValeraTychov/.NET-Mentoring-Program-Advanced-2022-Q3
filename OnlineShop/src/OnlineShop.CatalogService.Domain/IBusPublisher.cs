using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Domain;

public interface IBusPublisher<TMessage>
{
    public void PublishItemChanged(Item item);
}