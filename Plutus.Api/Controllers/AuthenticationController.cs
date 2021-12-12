using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plutus.Application.Users;
using Plutus.Application.Users.Queries;

namespace Plutus.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("/api/users")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("authenticate", Name = "Authenticate")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserResponse))]
    public async Task<ActionResult<UserResponse>> AuthenticateAsync(
        [FromBody] AuthenticateQuery query)
    {
        var user = await _mediator.Send(query);
        return Ok(user);
    }
}