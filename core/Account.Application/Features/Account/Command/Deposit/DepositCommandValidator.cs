using Account.Application.Features.Constant;
using Account.Infrastructure.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Account.Command.Deposit
{


        public class DepositCommandValidator : TransactionCommandValidator<DepositCommand>
        {
        private IAccountRepository userRepository;
        public DepositCommandValidator(IAccountRepository userRepository) : base(userRepository)
            {
         
                RuleFor(p => p.Amount).LessThanOrEqualTo(TransactionConstant.INVALID_MAX_TRANSACTION_AMOUNT).WithMessage("Invalid Amount it Must not b More Than "+TransactionConstant.INVALID_MAX_TRANSACTION_AMOUNT);
            }
    }
    
}
