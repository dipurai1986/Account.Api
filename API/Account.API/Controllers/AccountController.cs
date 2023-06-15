using Account.API.Middleware;
using Account.Application.Features.Account.Command.CreateAccount;
using Account.Application.Features.Account.Command.DeleteAccount;
using Account.Application.Features.Account.Command.Deposit;
using Account.Application.Features.Account.Command.Withdraw;
using Account.Application.Features.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Account.API.Controllers
{
    [ApiVersion("4.0")]
   
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /////////////200-ok
        ///400 bad request 
        ///401 Unauthorized 
        ///403 Forbidden
        ///404 Not Found
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [CustomFilter]
        [HttpPost("CreateAccount")]       
        public async Task<ActionResult> CreateAccount(CreateAccountCommand userRequest)
        {
            _logger.LogInformation("Receive Request");
            var response = await _mediator.Send(userRequest);
            return Ok(response);
        }
        [CustomFilter]
        [HttpGet("GetAccounts/{userId}")] 
        public async Task<ActionResult> GetAccounts(int userId)
        {
            var request = new GetAccountRequest() { userid = userId };
            var response = await _mediator.Send(request);
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
