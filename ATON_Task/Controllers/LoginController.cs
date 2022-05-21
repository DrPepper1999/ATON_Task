using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Login.Queries.GetToken;
using Users.WebApi.Models;

namespace Users.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration) =>
            _configuration = configuration;

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TokenVm>> Login(string login, string password)
        {
            var query = new GetTokenQuery
            {
                Login = login,
                Password = password,
                JwtKey = _configuration["Jwt:Key"],
                JwtIssuer = _configuration["Jwt:Issuer"],
                JwtAudience = _configuration["Jwt:Audience"]
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
