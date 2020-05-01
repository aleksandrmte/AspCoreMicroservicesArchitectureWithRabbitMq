using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.Models;
using Web.Models.Dto;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserVm model)
        {
            var dto = new UserDto
            {
                Email = model.UserEmail,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            await _userService.Create(dto);
            return View("Index");
        }
    }
}
