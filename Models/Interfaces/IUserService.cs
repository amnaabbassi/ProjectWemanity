using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interfaces
{
  public interface IUserService
  {
    IEnumerable<User> GetAllUser();
    string Login(string login,string password);
  }
}
