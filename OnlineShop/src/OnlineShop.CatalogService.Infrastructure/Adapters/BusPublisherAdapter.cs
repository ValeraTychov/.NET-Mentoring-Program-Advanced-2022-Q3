using AutoMapper;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Abstraction.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Adapters;

public class BusPublisherAdapter : IBusPublisher
{
    private readonly IMessagingService _messagingService;
    private readonly IMapper _mapper;

    public BusPublisherAdapter(IMessagingService messagingService, IMapper mapper)
    {
        _messagingService = messagingService;
        _mapper = mapper;
    }

    public void PublishItemChanged(Item item, DateTime changed)
    {
        var itemChangedParameters = _mapper.Map<ItemChangedParameters>(item);
        itemChangedParameters.Timestamp = changed;
        _messagingService.Publish(itemChangedParameters);
    }
}