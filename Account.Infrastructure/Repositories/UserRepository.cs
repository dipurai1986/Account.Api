using Account.Domain;

using Account.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {

        public UserRepository(AccountDBContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            _context.user.Add(user);
            await Task.CompletedTask;
        }

        public async Task<User> GetUserByuid(int uid)
        {

            return  await Task.FromResult(_context.user.FirstOrDefault(u => u.UserId == uid));
        
        }
    }
}
