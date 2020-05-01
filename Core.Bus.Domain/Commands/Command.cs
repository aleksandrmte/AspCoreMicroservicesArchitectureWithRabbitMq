using System;
using Core.Bus.Domain.Events;

namespace Core.Bus.Domain.Commands
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }

        protected Command()
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}
