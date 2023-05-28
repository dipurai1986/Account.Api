using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Account.Command.Deposit
{

    public class DepositCommand : IRequest
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }

}
