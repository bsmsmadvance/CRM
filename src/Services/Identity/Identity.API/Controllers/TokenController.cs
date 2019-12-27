using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Params.Inputs;
using Microsoft.AspNetCore.Mvc;
using Auth;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ErrorHandling;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenService TokenService;

        public TokenController(ITokenService tokenService)
        {
            this.TokenService = tokenService;
        }

        [HttpPost("ClientLogin")]
        [ProducesResponseType(200, Type = typeof(JsonWebToken))]
        public async Task<IActionResult> Login([FromBody]ClientLoginParam input)
        {
            try
            {
                var result = await TokenService.ClientLoginAsync(input);
                return Ok(result);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(JsonWebToken))]
        public async Task<IActionResult> Login([FromBody]LoginParam input)
        {
            try
            {
                var result = await TokenService.LoginAsync(input);
                return Ok(result);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody]LogoutParam input)
        {
            var userID = HttpContext.User.Claims.Where(x => x.Type == "userid").SingleOrDefault();
            await TokenService.LogoutAsync(input);
            return Ok();
        }
    }
}
