﻿using Newtonsoft.Json;
using OnlineShop.Messaging.Service.Events;
using OnlineShop.Messaging.Service.Storage;
using OnlineShop.Messaging.Service.Utils;
using RabbitMQ.Client;
using System.Text;

namespace OnlineShop.Messaging.Service.Models;

public class PublishManager<TMessage> : IDisposable
{
    private PublisherStorage<TMessage> _publisherStorage;
    private readonly IConnectionProvider _connectionProvider;
    private IConnection _connection;

    private SimpleLock simpleLock = new SimpleLock();

    public PublishManager(PublisherStorage<TMessage> publisherStorage, IConnectionProvider connectionProvider)
    {
        _publisherStorage = publisherStorage;
        _publisherStorage.MessageReceived += PublishMessages;
        _connectionProvider = connectionProvider;
        _connectionProvider.ConnectionCreated += OnConnectionCreated;
        _connection = connectionProvider.Connection;
    }

    private void OnConnectionCreated(object? sender, ConnectionCreatedEventArgs e)
    {
        _connection = e.Connection;
    }

    private void PublishMessages(object? sender, EventArgs args)
    {
        if (!simpleLock.TryEnter())
        {
            return;
        }

        while (_publisherStorage.TryPeek(out var message))
        {
            try
            {
                PublishMessage(message);
            }
            catch
            {
                break;
            }

            Dequeue();
        }

        simpleLock.Exit();
    }

    private void Dequeue()
    {
        while (!_publisherStorage.TryDequeue(out var _))
        {
        }
    }

    private void PublishMessage(TMessage message)
    {
        var str = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(str);
        var queue = message.GetType().Name;

        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.BasicPublish(string.Empty, queue, true, null, body);
    }
    
    public void Dispose()
    {
        _connectionProvider.ConnectionCreated -= OnConnectionCreated;
        _connection?.Dispose();
    }

    
}