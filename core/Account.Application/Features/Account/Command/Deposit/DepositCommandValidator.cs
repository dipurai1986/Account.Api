﻿using Account.Application.Features.Constant;
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

            RuleFor(p => p)
                       .MustAsync((command, cancellationToken) => isValidAccountID(command.UserId, command.AccountId))
                       .WithMessage(MessageConstant.ACCOUNT_INVALID_MESSAGE);
            RuleFor(p => p.Amount).LessThanOrEqualTo(TransactionConstant.INVALID_MAX_TRANSACTION_AMOUNT).WithMessage(MessageConstant.MAX_AMOUNT_PER_TRANSACTION_LIMIT_MESSAGGE+TransactionConstant.INVALID_MAX_TRANSACTION_AMOUNT);
            }
        private async Task<bool> isValidAccountID(int userId, int accountId)
        {
            var userAccount = await _accRepository.GetAccountByAccountId(userId, accountId);
            if (userAccount == null)
            {
                return false;
            }
            return true;
        }
    }
    
}
