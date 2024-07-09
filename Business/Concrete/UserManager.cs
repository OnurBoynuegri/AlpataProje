﻿using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserManager : IUserService
	{

		private readonly IUserDal _userDal;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IEmailService _emailService;
		public UserManager(IUserDal userDal, IPasswordHasher<User> passwordHasher, IEmailService emailService)
		{
			_userDal = userDal;
			_passwordHasher = passwordHasher;
			_emailService = emailService;
		}

		public async Task AddUser(User user)
		{
			user.Password = _passwordHasher.HashPassword(user, user.Password);
			await _userDal.Add(user);

			string subject = "Hoş Geldiniz...";
			string message= $"Merhaba {user.Name}, kaydolduğunuz için teşekkür ederiz!";
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

		public async Task<User> GetUserById(int id)
		{
			return await _userDal.GetById(id);
		}

		public async Task UpdateUser(User user)
		{
			await _userDal.Update(user);
		}
	}
}
