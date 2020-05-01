using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Bus.Domain.Bus;
using Management.Domain.Events.CreateEmployer;
using MediatR;

namespace Management.Domain.Commands.CreateEmployer
{
    public class CreateEmployerCommandHandler : IRequestHandler<CreateEmployerCommand, bool>
    {
        private readonly IEventBus _bus;
        public CreateEmployerCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateEmployerCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ
            _bus.Publish(new EmployerCreatedEvent(request.FirstName, request.LastName, request.Email));
            return Task.FromResult(true);
        }
    }
}
