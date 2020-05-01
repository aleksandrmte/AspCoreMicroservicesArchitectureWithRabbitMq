using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Employer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employer.Infrastructure.Data.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly EmployerContext _context;
        public EmployerRepository(EmployerContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Domain.Models.Employer>> GetAll()
        {
            var data = await _context.Employers.ToListAsync();
            return data;
        }

        public async Task<int> Create(Domain.Models.Employer entity)
        {
            try
            {
                await _context.Employers.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                var p = ex;
            }

            return 0;
        }
    }
}
