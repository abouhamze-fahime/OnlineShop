using Application.CQRS.AuthenticateCommandQuery.Command;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public AuthenticateController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
                  _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post(LoginCommand login)
        {
           var result = await this.mediator.Send(login);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUser)
        {
          var result =  await this.mediator.Send(registerUser);
            return Ok(result);
        }


        [HttpPost("generatetoken")]
        public async Task<IActionResult> GenerateToken(GenerateTokenCommand  generateTokenCommand)
        { 
            var result = await mediator.Send(generateTokenCommand);
            return Ok(result);

        }
    }
}
