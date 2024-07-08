using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
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
        public UserManager(IUserDal userDal)
        {
				_userDal = userDal;
        }

		public async Task AddUser(User user)
		{
			await _userDal.Add(user);
		}

		public async void DeleteUser(int id)
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

		public async void UpdateUser(User user)
		{
			await _userDal.Update(user);
		}
	}
}
