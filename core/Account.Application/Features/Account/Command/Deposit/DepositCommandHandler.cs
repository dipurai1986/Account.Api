using Account.Application.Exceptions;
using Account.Domain;
using Account.Infrastructure.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Account.Command.Deposit
{
    public class DipositCommandHandler : IRequestHandler<DepositCommand, Unit>
    {
        private readonly IAccountRepository _accountRepository;

        public DipositCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

        }



        public async Task<Unit> Handle(DepositCommand request, CancellationToken cancellationToken)
        {

            var validator = new DepositCommandValidator(_accountRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Transaction", validationResult);
            _accountRepository.DepositAccount(request.UserId, request.AccountId, request.Amount);
            return Unit.Value;
        }
    }
}

