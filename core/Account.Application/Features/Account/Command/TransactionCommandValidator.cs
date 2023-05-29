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
            protected IAccountRepository _accRepository;

            public TransactionCommandValidator(IAccountRepository accRepository)
            {
                this._accRepository = accRepository;

                RuleFor(p => GetPropertyValue(p, "AccountId"))
                    .NotEmpty().WithMessage(MessageConstant.ACCOUNT_REQUIRE_MESSAGE);
           


            RuleFor(p => GetPropertyValue(p, "Amount"))
                    .NotEmpty().WithMessage(MessageConstant.AMOUNT_REQUIRE_MESSAGE);


                RuleFor(p => p)
                    .Must(p => (decimal)GetPropertyValue(p, "Amount") > 0).WithMessage(MessageConstant.INVALID_AMOUNT_MESSAGE);
        }

            private object GetPropertyValue(T obj, string propertyName)
            {
                var property = typeof(T).GetProperty(propertyName);
                return property?.GetValue(obj);
            }
              
    }


    

}
