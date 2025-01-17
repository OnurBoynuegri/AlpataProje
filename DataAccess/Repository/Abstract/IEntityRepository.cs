﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Abstract
{
	public interface IEntityRepository<T> where T: class
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> GetById(int id);
		Task Add(T entity);
		Task Update(T entity);
		Task Delete(T entity);

	}
}
