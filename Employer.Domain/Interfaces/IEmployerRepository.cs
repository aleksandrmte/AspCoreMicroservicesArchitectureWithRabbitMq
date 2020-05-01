using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employer.Domain.Interfaces
{
    public interface IEmployerRepository
    {
        Task<IEnumerable<Models.Employer>> GetAll();
        Task<int> Create(Models.Employer entity);
    }
}
