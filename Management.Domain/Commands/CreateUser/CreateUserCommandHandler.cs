using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Bus.Domain.Bus;
using Management.Domain.Events.CreateUser;
using MediatR;

namespace Management.Domain.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IEventBus _bus;
        public CreateUserCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ
            _bus.Publish(new UserCreatedEvent(request.FirstName, request.LastName, request.Email));
            return Task.FromResult(true);
        }
    }
}
