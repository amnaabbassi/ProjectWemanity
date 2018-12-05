using Models;
using Models.Interfaces;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;
using System.Linq;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private Dictionary<string, string> _loggedUser;
       // private readonly IUserRepository _userRepository;
        public UserService()
        {
            _loggedUser = new Dictionary<string, string>();
           // _userRepository = userRepository;
        }
        public IEnumerable<User> GetAllUser()
        {
            List<User> Users = new List<User>();
            using (StreamReader r = new StreamReader(@"C:\Users\abbassi\Downloads\test-master\sondage\SondageWebApi\Models\Data\Usersjson.json"))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<User>>(json);
                foreach (var item in items)
                {
                    User newuser = new User();
                    newuser.Id = item.Id;
                    newuser.Name = item.Name;
                    newuser.LastNAme = item.LastNAme;
                    newuser.Login = item.Login;
                    newuser.Password = item.Password;
                    Users.Add(newuser);
                }
            }

            return Users;
        }

        public string HashPassword(string Password)
        {
            byte[] salt = Encoding.ASCII.GetBytes("Hello I'm here!!!!");

            string result = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return result;
        }

        public string Login(string login, string password)
        {
            string token = string.Empty;
            string hashpassword = HashPassword(password);

            User user = GetAllUser().Where(s => s.Login == login && s.Password == hashpassword).FirstOrDefault();
            if (user != default(User))
            {
                foreach (KeyValuePair<string, string> item in _loggedUser)
                {
                    if (item.Value == login)
                        token = item.Key;
                }

                if (string.IsNullOrEmpty(token))
                {
                    token = Guid.NewGuid().ToString();
                    _loggedUser.Add(token, login);
                }
            }

            return token;
        }

    }
}
