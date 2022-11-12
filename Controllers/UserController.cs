using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;

        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Token>> Login(User user)
        {
            string? email = await userRepository.Login(user);
            if (email != null)
            {
                string token = tokenService.GenerateToken(user.Email, "User");
                var TokenKey = new Token
                {
                    Key = token
                };
                return Ok(TokenKey);
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await userRepository.Register(user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
