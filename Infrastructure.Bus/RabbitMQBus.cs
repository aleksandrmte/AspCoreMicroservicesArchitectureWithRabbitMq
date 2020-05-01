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
        private readonly IConnection _connection;

        private IModel _channel;

        public IModel Channel
        {
            get { return _channel ??= _connection.CreateModel(); }
        }
        
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMqBus(IMediator mediator, IServiceScopeFactory serviceProvider, IConfiguration config)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceProvider;

            var connectionFactory = new ConnectionFactory
            {
                HostName = config["EventBus:HostName"],
                Port = int.Parse(config["EventBus:Port"]),
                UserName = config["EventBus:UserName"],
                Password = config["EventBus:Password"],
                DispatchConsumersAsync = true
            };
            _connection = connectionFactory.CreateConnection();

        }

        public Task SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var exchangeName = @event.GetType().Name;
            CreateExchangeIfNotExists(exchangeName);

            var message = JsonConvert.SerializeObject(@event);
            var bytesEvent = Encoding.UTF8.GetBytes(message);

            Channel.BasicPublish(exchangeName, string.Empty, body: bytesEvent);
        }

        private void CreateExchangeIfNotExists(string exchangeName)
        {
            Channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true);
        }

        public void Subscribe<TEvent, THandler>(string subscriberName)
            where TEvent : Event
            where THandler : IEventHandler
        {
            var exchangeName = typeof(TEvent).Name;
            BindQueue(exchangeName, subscriberName);

            var consumer = new AsyncEventingBasicConsumer(Channel);
            StartBasicConsume<TEvent>(consumer, subscriberName);
        }

        private void BindQueue(string exchangeName, string subscriberName)
        {
            CreateExchangeIfNotExists(exchangeName);

            Channel.QueueDeclare(subscriberName, true, false, autoDelete: false);
            Channel.QueueBind(subscriberName, exchangeName, string.Empty);
        }

        private void StartBasicConsume<TEvent>(AsyncEventingBasicConsumer consumer, string subscriberName) where TEvent : Event
        {
            consumer.Received += Consumer_Received<TEvent>;
            Channel.BasicConsume(subscriberName, false, consumer);
        }

        private async Task Consumer_Received<TEvent>(object sender, BasicDeliverEventArgs args) where TEvent : Event
        {
            var jsonMessage = Encoding.UTF8.GetString(args.Body.ToArray());
            var message = JsonConvert.DeserializeObject<TEvent>(jsonMessage);

            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<TEvent>>();
                await handler.Handle(message);
                Channel.BasicAck(args.DeliveryTag, false);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
