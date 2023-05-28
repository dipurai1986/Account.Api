using Account.Application.Features.Account.Command.CreateAccount;
using Account.Application.Features.Account.Command.DeleteAccount;
using Account.Application.Features.Account.Command.Deposit;
using Account.Application.Features.Account.Command.Withdraw;
using Account.Application.Features.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateAccount")] 
        public async Task<ActionResult> CreateAccount(CreateAccountCommand userRequest)
        {
            var response = await _mediator.Send(userRequest);
            return Ok(response);
        }

        [HttpGet("GetAccounts/{userId}")]
        public async Task<ActionResult> GetAccounts(int userId)
        {
            var request = new GetAccountRequest() { userid = userId };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(DepositCommand depositCommand )
        {         
            var response = await _mediator.Send(depositCommand);
            return Ok(response);
        }
        [HttpPost("Withdraw")]
        public async Task<ActionResult> Withdraw(WithdrawCommand withdrawCommand)
        {
            var response = await _mediator.Send(withdrawCommand);
            return Ok(response);
        }
        [HttpDelete("DeleteAccount/{userId}/{AccountId}")]
        public async Task<ActionResult> DeleteAccount(int userId,int AccountId)
        {
            var request = new DeleteAccountCommand() { UserId = userId, AccountId = AccountId };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
       
    }
}
