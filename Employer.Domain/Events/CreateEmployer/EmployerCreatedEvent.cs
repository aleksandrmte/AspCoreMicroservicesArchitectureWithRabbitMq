using System;
using System.Collections.Generic;
using System.Text;
using Core.Bus.Domain.Events;

namespace Employer.Domain.Events.CreateEmployer
{
    public class EmployerCreatedEvent: Event
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public EmployerCreatedEvent(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
