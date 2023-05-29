using Account.Domain;

using Account.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Account.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        public static List<User> users = new List<User>();

        public Task<List<UserAccount>> CreateAccount(int userid, UserAccount userAccount)
        {
            if (users.FirstOrDefault(u => u.UserId == userid) != null)
            {
                users.FirstOrDefault(u => u.UserId == userid).UserAccount.Add(userAccount);
            }
            else
            {
                users.Add(new User() { UserId = userid, UserAccount = new List<UserAccount> { userAccount } });
            }

            return Task.FromResult(users.FirstOrDefault(u => u.UserId == userid).UserAccount.ToList());
        }
        public Task<List<UserAccount>> GetAccountsByUserId(int userid)
        {

            return Task.FromResult(users.FirstOrDefault(u => u.UserId == userid)?.UserAccount);

        }
        public Task<List<UserAccount>> DeleteAccount(int userid, int accountId)
        {
            if (users.FirstOrDefault(u => u.UserId == userid).UserAccount.Count > 1)
            {
                 users.FirstOrDefault(u=>u.UserId==userid).UserAccount.Remove(users.FirstOrDefault(u => u.UserId == userid).UserAccount.FirstOrDefault(acc => acc.AccountId == accountId));
            }
            else
            {
                users.Remove(users.FirstOrDefault(u => u.UserId == userid));
            }
           
         return this.GetAccountsByUserId(userid);
       }
     

        public Task<UserAccount> GetAccountByAccountId(int userid, int accountId)
        {
            return Task.FromResult(users.FirstOrDefault(u => u.UserId == userid)?.UserAccount?.FirstOrDefault(useracc => useracc.AccountId == accountId));
        }

        public Task DepositAccount(int userid, int accountId, decimal amount)
        {
          users.FirstOrDefault(u=>u.UserId==userid).UserAccount.FirstOrDefault(useracc=>useracc.AccountId == accountId).Balance += amount;
            return Task.CompletedTask;


        }
        //public User GetUser(int userId)
        //{
        //    return users.FirstOrDefault(u => u.UserId == userId);
        //}
        //private UserAccount GetUserAccount(int userId,int accountId)
        //{
        //    return users.FirstOrDefault(u => u.UserId == userId).UserAccount.FirstOrDefault(x=>x.AccountId== accountId);
        //}

        public Task WithdrawAccount(int userid, int accountId, decimal amount)
        {
            users.FirstOrDefault(u => u.UserId == userid).UserAccount.FirstOrDefault(useracc => useracc.AccountId == accountId).Balance -= amount;
            return Task.CompletedTask;
        }
    }
}
