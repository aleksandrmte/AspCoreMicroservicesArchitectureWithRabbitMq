using System;
using System.Collections.Generic;
using System.Text;
using Core.Bus.Domain.Commands;

namespace Management.Domain.Commands.CreateUser
{
    public class CreateUserCommand: Command
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public CreateUserCommand(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
