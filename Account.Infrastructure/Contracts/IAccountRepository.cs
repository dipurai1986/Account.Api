using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Account.Domain;

namespace Account.Infrastructure.Contracts
{
    public interface IAccountRepository
    {
        Task<List<UserAccount>> CreateAccount(int userid, UserAccount userAccount);
        Task<List<UserAccount>> GetAccountsByUserId(int userid);
        Task<UserAccount> GetAccountByAccountId(int userid,int accountId);
        Task DepositAccount(int userid, int accountId, decimal amount);
        Task WithdrawAccount(int userid, int accountId, decimal amount);
        Task<List<UserAccount>> DeleteAccount(int userid, int accountId);

    }
}
