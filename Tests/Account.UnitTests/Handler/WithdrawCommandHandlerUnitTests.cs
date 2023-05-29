using Account.Application.Exceptions;
using Account.Application.Features.Account.Command.Deposit;
using Account.Application.Features.Account.Command.Withdraw;
using Account.Domain;
using Account.Infrastructure.Contracts;
using FluentValidation;
using FluentValidation.TestHelper;
using MediatR;
using NSubstitute;
using System.Threading;
using Xunit;


namespace Account.UnitTests.Handler
{





    public class WithdrawCommandHandlerTests
    {
        private readonly WithdrawCommandHandler _handler;
        private readonly IAccountRepository _accountRepositoryMock;

        public WithdrawCommandHandlerTests()
        {
            _accountRepositoryMock = Substitute.For<IAccountRepository>();
            _handler = new WithdrawCommandHandler(_accountRepositoryMock);
        }

        [Fact]
        public async Task Handle_InvalidTransaction_ThrowsBadRequestException()
        {
            // Arrange
            var command = new WithdrawCommand { UserId = 0, AccountId = 100, Amount = 0 };
            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("", "Invalid Transaction"));
            _accountRepositoryMock.GetAccountByAccountId(command.UserId, command.AccountId)
                .Returns(Task.FromResult(new UserAccount()));

            var cancellationToken = CancellationToken.None;

      
            await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, cancellationToken));
        }

        [Fact]
        public async Task Handle_ValidTransaction_WithdrawsAccount()
        {
            // Arrange
            var command = new WithdrawCommand { UserId = 0, AccountId = 100, Amount = 100 };
        
            _accountRepositoryMock.GetAccountByAccountId(command.UserId, command.AccountId)
                .Returns(Task.FromResult(new UserAccount() { AccountId= 100 ,Balance=200,AccountNumber="ACC-12345"}));
         
            var cancellationToken = CancellationToken.None;

            var result = await _handler.Handle(command, cancellationToken);

           
            Assert.Equal(Unit.Value, result);
            await _accountRepositoryMock.Received(1).WithdrawAccount(command.UserId, command.AccountId, command.Amount);
        }


        [Fact]
        public async Task Handle_NegativeWithdrawCommand_ValidationErrors()
        {
            // Arrange
            var accountRepository = Substitute.For<IAccountRepository>();
            var handler = new WithdrawCommandHandler(accountRepository);
            var command = new WithdrawCommand
            {
                UserId = 1,
                AccountId = 123,
                Amount = -100
            };
            accountRepository.GetAccountByAccountId(command.UserId, command.AccountId)
           .Returns(Task.FromResult(new UserAccount() { AccountId = 100, Balance = 200, AccountNumber = "ACC-12345" }));
            //accountRepository.DepositAccount(command.)
            // Act
            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));

            // Assert

        }
    }


}

