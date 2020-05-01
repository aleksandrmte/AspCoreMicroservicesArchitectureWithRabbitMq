using System.Threading.Tasks;
using Core.Bus.Domain.Commands;
using Core.Bus.Domain.Events;

namespace Core.Bus.Domain.Bus
{
    public interface IEventBus
    {
        Task SendCommand<TCommand>(TCommand command) where TCommand : Command;
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
        void Subscribe<TEvent, THandler>()
            where TEvent : Event
            where THandler : IEventHandler;
    }
}
