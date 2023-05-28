using Account.Application.Features.Constant;
using Account.Infrastructure.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Account.Command
{
   
        public abstract class TransactionCommandValidator<T> : AbstractValidator<T>
        {
            protected IAccountRepository userRepository;

            public TransactionCommandValidator(IAccountRepository userRepository)
            {
                this.userRepository = userRepository;

                RuleFor(p => GetPropertyValue(p, "AccountId"))
                    .NotEmpty().WithMessage("Account Id must be required");


                RuleFor(p => GetPropertyValue(p, "Amount"))
                    .NotEmpty().WithMessage("Amount must be required");


                RuleFor(p => p)
                    .Must(p => (decimal)GetPropertyValue(p, "Amount") > 0).WithMessage("Invalid Amount");
        }

            private object GetPropertyValue(T obj, string propertyName)
            {
                var property = typeof(T).GetProperty(propertyName);
                return property?.GetValue(obj);
            }
        }

    

}
