using Employer.Application.Interfaces;
using Employer.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employer.Application.Services
{
    public class EmployerService: IEmployerService
    {
        private readonly IEmployerRepository _employerRepository;
        
        public EmployerService(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }
        
        public async Task<IEnumerable<Domain.Models.Employer>> GetEmployers()
        {
            return await _employerRepository.GetAll();
        }
    }
}
