using Management.Application.Dto;
using System.Threading.Tasks;

namespace Management.Application.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDto transfer);
    }
}
