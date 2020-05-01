using Employer.Application.Dto;
using Employer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;

        public EmployerController(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployerDto>> Get()
        {
            var data = await _employerService.GetEmployers();
            return data.Select(e => new EmployerDto
            {
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BeginningOfWork = e.BeginningOfWork
            });
        }
    }
}
