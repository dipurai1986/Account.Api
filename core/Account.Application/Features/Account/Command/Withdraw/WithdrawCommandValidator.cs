﻿using Account.Application.Features.Account.Command.Withdraw;
using Account.Application.Features.Constant;
using Account.Infrastructure.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Account.Command.Withdraw
{

  

    public class WithdrawCommandValidator : TransactionCommandValidator<WithdrawCommand>
    {
        private IAccountRepository _userRepository;
        public WithdrawCommandValidator(IAccountRepository userRepository) : base(userRepository)
        {
            this._userRepository = userRepository;
            RuleFor(p => p)
                .MustAsync((command, cancellationToken) => IsAmountGreaterThanBalance(command.UserId, command.AccountId, command.Amount))
                .WithMessage("Insufficient balance for withdrawal");
            RuleFor(p => p)
              .MustAsync((command, cancellationToken) => IsWithdrawAmountValid(command.UserId, command.AccountId, command.Amount))
              .WithMessage("You Cant Withdraw this much Amount");

            RuleFor(p => p.Amount)
                .Must(amount => amount > 0).WithMessage("Invalid Amount");
        }

        private async Task<bool> IsAmountGreaterThanBalance(int userId, int accountId, decimal amount)
        {
            var userAccount = await _userRepository.GetAccountByAccountId(userId, accountId);
            if (userAccount == null)
            {
                return false;
            }
            return userAccount.Balance - TransactionConstant.MIN_BALANCE >= amount;
        }
        private async Task<bool> IsWithdrawAmountValid(int userId, int accountId, decimal amount)
        {
            var userAccount = await _userRepository.GetAccountByAccountId(userId, accountId);
            if (userAccount == null)
            {
                return false;
            }
            var allowedLimit = userAccount.Balance * TransactionConstant.MAX_WITHDRAW_LIMIT;
            return amount <= allowedLimit;
        }
    }

}