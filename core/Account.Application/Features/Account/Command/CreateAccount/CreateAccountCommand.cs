using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Domain;
using MediatR;

namespace Account.Application.Features.Account.Command.CreateAccount
{
    public class CreateAccountCommand : IRequest<List<UserAccount>>
    {
        public int UserId { get; set; }
    }
}
