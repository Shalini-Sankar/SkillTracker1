using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillTrackerIdentity.API.Models;

namespace SkillTrackerIdentity.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        public User GetUser(string UserName,string Password)
        {
            return _context.Users.Where(e => e.UserName == UserName && e.Password == Password).FirstOrDefault();
        }
    }
}
