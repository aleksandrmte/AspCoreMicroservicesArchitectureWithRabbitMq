using Core.Bus.Domain.Bus;
using Core.Bus.Domain.Commands;
using Core.Bus.Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Bus
{
    public sealed class RabbitMqBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConnectionFactory _connectionFactory;

        public RabbitMqBus(IMediator mediator, IServiceScopeFactory serviceProvider, IConfiguration config)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceProvider;

            _connectionFactory = new ConnectionFactory
            {
                HostName = config["EventBus:HostName"],
                Port = int.Parse(config["EventBus:Port"]),
                UserName = config["EventBus:UserName"],
                Password = config["EventBus:Password"],
                DispatchConsumersAsync = true
            };
        }

        public Task SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            var exchangeName = @event.GetType().Name;
            CreateExchangeIfNotExists(channel, exchangeName);

            var message = JsonConvert.SerializeObject(@event);
            var bytesEvent = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchangeName, string.Empty, body: bytesEvent);
        }

        private static void CreateExchangeIfNotExists(IModel channel, string exchangeName)
        {
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true);
        }

        public void Subscribe<TEvent, THandler>(string subscriberName)
            where TEvent : Event
            where THandler : IEventHandler
        {
            var exchangeName = typeof(TEvent).Name;
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            BindQueue(channel, exchangeName, subscriberName);

            var consumer = new AsyncEventingBasicConsumer(channel);
            StartBasicConsume<TEvent>(channel, consumer, subscriberName);
        }

        private static void BindQueue(IModel channel, string exchangeName, string subscriberName)
        {
            CreateExchangeIfNotExists(channel, exchangeName);

            channel.QueueDeclare(subscriberName, true, false, autoDelete: false);
            channel.QueueBind(subscriberName, exchangeName, string.Empty);
        }

        private void StartBasicConsume<TEvent>(IModel channel, AsyncEventingBasicConsumer consumer, string subscriberName) where TEvent : Event
        {
            consumer.Received += async (obj, args) =>
            {
                var jsonMessage = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonConvert.DeserializeObject<TEvent>(jsonMessage);

                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<TEvent>>();
                    await handler.Handle(message);
                    channel.BasicAck(args.DeliveryTag, false);
                }
                catch (Exception)
                {
                    // ignored
                }
            };
            channel.BasicConsume(subscriberName, false, consumer);
        }
    }
}
