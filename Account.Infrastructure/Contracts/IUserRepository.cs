using Account.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Contracts
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<User> GetUserByuid (int uid);
    }
}
