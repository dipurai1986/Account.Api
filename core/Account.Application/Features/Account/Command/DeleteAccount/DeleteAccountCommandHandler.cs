using Account.Domain;
using Account.Infrastructure.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Account.Command.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, List<UserAccount>>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

        }

        public async Task<List<UserAccount>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {

            if (request == null) throw new ArgumentNullException(nameof(request));
       

            return await _accountRepository.DeleteAccount(request.UserId, request.AccountId);
        }


    }
}
