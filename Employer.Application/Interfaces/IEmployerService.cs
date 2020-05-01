using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employer.Application.Interfaces
{
    public interface IEmployerService
    {
        Task<IEnumerable<Domain.Models.Employer>> GetEmployers();
    }
}
