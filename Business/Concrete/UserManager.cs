﻿using Business.Abstract;
using Business.Concrete.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserManager : IUserService
	{

		private readonly IUserDal _userDal;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IEmailService _emailService;
		private readonly JwtSettings _jwtSettings;
		public UserManager(IUserDal userDal, IPasswordHasher<User> passwordHasher, IEmailService emailService, IOptions<JwtSettings> jwtSettings)
		{
			_userDal = userDal;
			_passwordHasher = passwordHasher;
			_emailService = emailService;
			_jwtSettings = jwtSettings.Value;
		}

		public async Task AddUser(User user)
		{
			await _userDal.Add(user);
			string subject = "Hoş Geldiniz...";
			string message = $"Merhaba {user.Name}, kaydolduğunuz için teşekkür ederiz!";
			await _emailService.SendEmailAsync(user.Email, subject, message);

		}

		public async Task DeleteUser(int id)
		{
			var user = await _userDal.GetById(id);
			if (user != null)
			{
				await _userDal.Delete(user);
			}
		}

		public async Task<IEnumerable<User>> GetAllUsers()
		{
			return await _userDal.GetAll();
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await _userDal.GetByEmail(email);
		}

		public async Task<User> GetUserById(int id)
		{
			return await _userDal.GetById(id);
		}

		public async Task<User> LoginUserAsync(UserLoginDto userLoginDto)
		{
			var user = await GetUserByEmail(userLoginDto.Email);
			if (user == null)
			{
				return null;
			}

			var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, userLoginDto.Password);
			if (passwordVerificationResult == PasswordVerificationResult.Failed)
			{
				return null;
			}
			else
			{
				return user;
			}

		}

		public async Task<User> RegisterUserAsync(UserRegisterDto userDto)
		{
			var user = new User
			{
				Name = userDto.Name,
				Surname = userDto.Surname,
				PhoneNumber = userDto.PhoneNumber,
				Password = userDto.Password,
				Email = userDto.Email
			};

			user.Password = _passwordHasher.HashPassword(user, userDto.Password);

			if (userDto.Image != null)
			{
				var imagePath = Path.Combine("wwwroot/images", $"{Guid.NewGuid()}{Path.GetExtension(userDto.Image.FileName)}");
				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					await userDto.Image.CopyToAsync(stream);
				}
				user.Image = imagePath;
			}

			await AddUser(user);
			return user;
		}

		public async Task UpdateUser(User user)
		{
			await _userDal.Update(user);
		}


		public async Task<string> GenerateJwtToken(User user)
		{
			var claims = new[]
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(_jwtSettings.Expires),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}


	}
}
