using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Domain;

public interface IBusPublisher
{
    public void PublishItemChanged(Item item, DateTime changed);
}