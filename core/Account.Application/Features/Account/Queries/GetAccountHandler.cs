
using Account.Domain;
using Account.Infrastructure.Contracts;

using MediatR;
using System.Security.Principal;


namespace Account.Application.Features.Account.Queries
{
    public class GetAccountHandler : IRequestHandler<GetAccountRequest, List<UserAccount>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

        }
        public async Task<List<UserAccount>> Handle(GetAccountRequest request, CancellationToken cancellationToken)
        {
            var useraccount = await _accountRepository.GetAccountsByUserId(request.userid);

            if (useraccount == null)
            {
                //throw new NotFoundException("User not found.");
            }

            return useraccount;

        }

    }
}
