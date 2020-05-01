using System;
using System.Collections.Generic;
using System.Text;
using Core.Bus.Domain.Events;

namespace Management.Domain.Events.CreateUser
{
    public class UserCreatedEvent: Event
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserCreatedEvent(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
