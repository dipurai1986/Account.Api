using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.UnitTests.Handler
{
    using Xunit;
    using NSubstitute;
    using FluentValidation;
    using FluentValidation.Results;
    using System.Threading;
    using System.Threading.Tasks;
    using Account.Infrastructure.Contracts;
    using Account.Domain;
    using Account.Infrastructure.Repositories;
    using Account.Application.Exceptions;
    using Account.Application.Features.Account.Command.Deposit;

    public class DepositCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidDepositCommand_NoValidationErrors()
        {
            // Arrange
            var accountRepository = Substitute.For<IAccountRepository>();
            var handler = new DipositCommandHandler(accountRepository);
            var command = new DepositCommand
            {
                UserId = 1,
                AccountId = 123,
                Amount = 100
            };
            accountRepository.GetAccountByAccountId(command.UserId, command.AccountId)
           .Returns(Task.FromResult(new UserAccount() { AccountId = 100, Balance = 200, AccountNumber = "ACC-12345" }));
            //accountRepository.DepositAccount(command.)
            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await accountRepository.Received(1).DepositAccount(command.UserId, command.AccountId, command.Amount);
        }

        [Fact]
        public async Task Handle_InvalidDepositCommand_ThrowsBadRequestException()
        {
            // Arrange
            var accountRepository = Substitute.For<IAccountRepository>();
           
            var handler = new DipositCommandHandler(accountRepository);
            var command = new DepositCommand
            {
                UserId = 1,
                AccountId = 123,
                Amount = 0
            };
          

            // Act/assert
            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
