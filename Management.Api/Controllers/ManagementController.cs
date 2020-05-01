using Management.Application.Dto;
using Management.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagementController : ControllerBase
    {
        private readonly IUserService _userService;

        public ManagementController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto dto)
        {
            await _userService.Create(dto);
            return Ok();
        }
    }
}
