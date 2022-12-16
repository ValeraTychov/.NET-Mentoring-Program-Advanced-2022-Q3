using AutoMapper;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.Messaging.Abstraction;

namespace OnlineShop.CatalogService.Infrastructure.Adapters;

public class BusPublisherAdapter<TMessage> : IBusPublisher<TMessage>
{
    private readonly IPublisher<TMessage> _publisher;
    private readonly IMapper _mapper;

    public BusPublisherAdapter(IPublisher<TMessage> publisher, IMapper mapper)
    {
        _publisher = publisher;
        _mapper = mapper;
    }

    public void PublishItemChanged(Item item)
    {
        var itemChangedParameters = _mapper.Map<TMessage>(item);
        _publisher.Publish(itemChangedParameters);
    }
}