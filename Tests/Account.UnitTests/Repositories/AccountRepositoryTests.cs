
using Xunit;
using System.Linq;
using Account.Domain;
using Account.Infrastructure.Contracts;
using Account.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Account.UnitTests.Repositories
{
    public class AccountRepositoryTests
    {
        [Fact]
        public async Task CreateAccount_ValidUser_CreatesAccount()
        {
            // Arrange
            var repository = new AccountRepository();
            var userId = 1;
            var userAccount = new UserAccount { AccountId = 123, Balance = 100 };

            // Act
            var result = await repository.CreateAccount(userId, userAccount);

            // Assert
            var user = AccountRepository.users.FirstOrDefault(u => u.UserId == userId);
            Assert.NotNull(user);
            Assert.Contains(userAccount, user.UserAccount);
        }

        [Fact]
        public async Task GetAccountsByUserId_ExistingUser_ReturnsUserAccounts()
        {
            // Arrange
            var repository = new AccountRepository();
            var userId = 1;
            var userAccount1 = new UserAccount { AccountId = 123, Balance = 100 };
            var userAccount2 = new UserAccount { AccountId = 456, Balance = 200 };
            await repository.CreateAccount(userId, userAccount1);
            await repository.CreateAccount(userId, userAccount2);

            // Act
            var result = await repository.GetAccountsByUserId(userId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(userAccount1, result);
            Assert.Contains(userAccount2, result);
        }

        [Fact]
        public async Task DeleteAccount_ExistingAccount_RemovesAccount()
        {
            // Arrange
            var repository = new AccountRepository();
            var userId = 1;
            var userAccount1 = new UserAccount { AccountId = 123, Balance = 100 };
            var userAccount2 = new UserAccount { AccountId = 456, Balance = 200 };
            await repository.CreateAccount(userId, userAccount1);
            await repository.CreateAccount(userId, userAccount2);

            // Act
            var result = await repository.DeleteAccount(userId, userAccount1.AccountId);

            // Assert
            Assert.NotNull(result);
         
        }

        [Fact]
        public async Task GetAccountByAccountId_ExistingAccount_ReturnsAccount()
        {
            // Arrange
            var repository = new AccountRepository();
            var userId = 1;
            var accountId = 123;
            var userAccount = new UserAccount { AccountId = accountId, Balance = 100 };
            await repository.CreateAccount(userId, userAccount);

            // Act
            var result = await repository.GetAccountByAccountId(userId, accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userAccount.AccountId, result.AccountId);
        }

        [Fact]
        public async Task DepositAccount_ExistingAccount_DepositsAmount()
        {
            // Arrange
            var repository = new AccountRepository();
            var userId = 1;
            var accountId = 123;
            var initialBalance = 100;
            var depositAmount = 50;
            var userAccount = new UserAccount { AccountId = accountId, Balance = initialBalance };
            await repository.CreateAccount(userId, userAccount);

            // Act
            await repository.DepositAccount(userId, accountId, depositAmount);

            // Assert
            var updatedUserAccount = await repository.GetAccountByAccountId(userId, accountId);
            Assert.Equal(initialBalance + depositAmount, updatedUserAccount.Balance);
        }

        [Fact]
        public async Task WithdrawAccount_ExistingAccount_WithdrawsAmount()
        {
            // Arrange
            var repository = new AccountRepository();
            var userId = 1;
            var accountId = 123;
            var initialBalance = 100;
            var withdrawAmount = 50;
            var userAccount = new UserAccount { AccountId = accountId, Balance = initialBalance };
            await repository.CreateAccount(userId, userAccount);

            // Act
            await repository.WithdrawAccount(userId, accountId, withdrawAmount);

            // Assert
            var updatedUserAccount = await repository.GetAccountByAccountId(userId, accountId);
            Assert.Equal(initialBalance - withdrawAmount, updatedUserAccount.Balance);
        }
    }
}
