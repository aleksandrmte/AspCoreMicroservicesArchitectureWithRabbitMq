using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Management.Domain.Models;

namespace Management.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> Create(User entity);
    }
}
