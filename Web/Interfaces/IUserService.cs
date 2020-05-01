using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Dto;

namespace Web.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDto userDto);
    }
}
