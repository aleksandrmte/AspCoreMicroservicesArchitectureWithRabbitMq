﻿using Core.Bus.Domain.Bus;
using Employer.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Employer.Domain.Events.CreateEmployer
{
    public class EmployerCreatedEventHandler : IEventHandler<EmployerCreatedEvent>
    {
        private readonly IEmployerRepository _employerRepository;

        public EmployerCreatedEventHandler(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public  Task Handle(EmployerCreatedEvent @event)
        {
            _employerRepository.Create(new Models.Employer
            {
                Email = @event.Email,
                FirstName = @event.FirstName,
                LastName = @event.LastName,
                BeginningOfWork = DateTime.UtcNow
            });

            return Task.CompletedTask;
        }
    }
}
