using Account.Domain;
using Account.Infrastructure.Contracts;
using Account.Infrastructure.Repositories;
using MediatR;

namespace Account.Application.Features.Account.Command.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, List<UserAccount>>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

        }

        public async Task<List<UserAccount>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new UserAccount()
            {
                AccountId = new Random().Next(99999),
                AccountNumber = GenerateAccountNumber(),
                Balance = 100
            };

            return await _accountRepository.CreateAccount(request.UserId, account);
        }
        private string GenerateAccountNumber()
        {
            // Generate a unique account number here
            return "ACCT-" + new Random().NextInt64();
        }
    }



}
