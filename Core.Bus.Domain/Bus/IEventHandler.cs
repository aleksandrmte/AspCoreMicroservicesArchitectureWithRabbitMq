using System.Threading.Tasks;
using Core.Bus.Domain.Events;

namespace Core.Bus.Domain.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler { }

}
