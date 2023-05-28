using Account.Domain;
using MediatR;

namespace Account.Application.Features.Account.Queries
{
    public class GetAccountRequest:IRequest<List<UserAccount>>
    {
        public int userid { get; set; }
    }
}