using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

		[HttpGet]
		public async Task <IActionResult> GetAllUsers()
		{
			var users = await _userService.GetAllUsers();
			return Ok(users);

		}
	

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await _userService.GetUserById(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> AddUser([FromBody] User user)
		{
			if (user == null)
			{
				return BadRequest();
			}
			await _userService.AddUser(user);
			return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateUser(int id, [FromBody] User user)
		{
			if (id != user.Id)
			{
				return BadRequest();
			}
			_userService.UpdateUser(user);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(int id)
		{
			_userService.DeleteUser(id);
			return NoContent();
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(User user)
		{
			await _userService.AddUser(user);

			return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
		}
	
	}
}
