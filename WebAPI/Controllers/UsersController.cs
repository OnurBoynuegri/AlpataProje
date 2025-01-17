﻿using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

		[HttpGet("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userService.GetAllUsers();
			return Ok(users);

		}

		[HttpGet("GetUserById")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await _userService.GetUserById(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[HttpPost("AddUser")]
		public async Task<IActionResult> AddUser([FromBody] User user)
		{
			if (user == null)
			{
				return BadRequest();
			}
			await _userService.AddUser(user);
			return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
		}

		[HttpPut("UpdateUser")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
		{
			if (id != user.Id)
			{
				return BadRequest();
			}
			await _userService.UpdateUser(user);
			return NoContent();
		}

		[HttpDelete("DeleteUser")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			await _userService.DeleteUser(id);
			return NoContent();
		}

		[HttpPost("Register")]
		public async Task<ActionResult<User>> Register([FromForm] UserRegisterDto userDto)
		{

			if (userDto == null)
			{
				return BadRequest();
			}
			var user = await _userService.RegisterUserAsync(userDto);
			var token = await _userService.GenerateJwtToken(user);
			return Ok(new { User = user, Token = token });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
		{
			if (userLoginDto == null)
			{
				return BadRequest();
			}

			var user = await _userService.LoginUserAsync(userLoginDto);
			if (user == null)
			{
				return Unauthorized();
			}
			var token = await _userService.GenerateJwtToken(user);

			return Ok(new { Token = token });
		}

	}
}
