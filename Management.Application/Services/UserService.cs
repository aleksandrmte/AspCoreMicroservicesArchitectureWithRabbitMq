using Core.Bus.Domain.Bus;
using Management.Application.Dto;
using Management.Application.Interfaces;
using System.Threading.Tasks;
using Management.Domain.Commands.CreateEmployer;

namespace Management.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IEventBus _bus;

        public UserService(IEventBus bus)
        {
            _bus = bus;
        }
        
        public async Task Create(UserDto user)
        {
            var createUserCommand = new CreateEmployerCommand(
                user.FirstName,
                user.LastName,
                user.Email
            );

            await _bus.SendCommand(createUserCommand);
        }
    }
}
