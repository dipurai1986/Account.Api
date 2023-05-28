using Account.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Account.Command.DeleteAccount
{
    public class DeleteAccountCommand : IRequest<List<UserAccount>>
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
    }
}
