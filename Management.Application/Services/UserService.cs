using Core.Bus.Domain.Bus;
using Management.Application.Dto;
using Management.Application.Interfaces;
using Management.Domain.Commands.CreateUser;
using Management.Domain.Interfaces;
using System.Threading.Tasks;

namespace Management.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _bus;

        public UserService(IUserRepository userRepository, IEventBus bus)
        {
            _userRepository = userRepository;
            _bus = bus;
        }
        
        public async Task Create(UserDto user)
        {
            var createTransferCommand = new CreateUserCommand(
                user.FirstName,
                user.LastName,
                user.Email
            );

            await _bus.SendCommand(createTransferCommand);
        }
    }
}
