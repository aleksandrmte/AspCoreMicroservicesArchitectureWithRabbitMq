using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Management.Domain.Interfaces;
using Management.Domain.Models;

namespace Management.Infrastructure.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<int> Create(User entity)
        {
            await _context.Users.AddAsync(entity);
            return entity.Id;
        }
    }
}
