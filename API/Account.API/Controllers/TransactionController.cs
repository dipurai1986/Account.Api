using Account.Application.Features.Account.Command.Deposit;
using Account.Application.Features.Account.Command.Withdraw;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(DepositCommand depositCommand)
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
    }
}
