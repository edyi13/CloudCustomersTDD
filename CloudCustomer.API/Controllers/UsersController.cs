using CloudCustomer.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        //private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }


        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _usersService.GetAllUsers();
            if(users.Any())
                return Ok(users);

            return NotFound();
        }
    }
}