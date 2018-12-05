using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
  public class UserRepository :EfRepository<User>,IUserRepository
  {
    public UserRepository(SondageDbcontext dbContext) : base(dbContext)
    {
    }

    public User GetUserByID(int id)
    {
      return _dbContext.USers.Find(id);
    }

    public IEnumerable<User> GetUsers()
    {
      return _dbContext.USers;
    }
  }
}
