using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IUserService
	{
		Task<IEnumerable<User>> GetAllUsers();
		Task<User> GetUserById(int id);
		Task AddUser(User user);
		Task UpdateUser(User user);
		Task DeleteUser(int id);
	}
}
