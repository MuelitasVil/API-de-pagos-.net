
using API.PaymentTransactions.API.Configuration;
using API.PaymentTransactions.Shared.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.PaymentTransactions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTConfig _jwtConfig;

        public AuthenticationController(UserManager<IdentityUser> userManager, IOptions<JWTConfig> jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest("insufficient credentials ...");

            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if(existingUser == null)
            {
                return BadRequest(new AuthResult
                {
                    Errors = new List<string>()
                    {
                        "Invalid Payload"
                    },
                    Result = false
                });
            }

            var chekUserAndPass = await _userManager.CheckPasswordAsync(
                existingUser,
                request.Password);

            if (!chekUserAndPass)
            {
                return BadRequest(
                    new AuthResult()
                    {
                        Errors = new List<string>()
                    {
                        "Invalid Credentials"
                    },
                        Result = false

                    });
            }

            var token = GenerateToken(existingUser);

            return Ok(new AuthResult
            {
                Token = token,
                Result = true,
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrarionUserDto request)
        {
            if (!ModelState.IsValid) { return BadRequest("Ingrese todos los datos ..."); }


            var emailExist = await _userManager.FindByEmailAsync(request.Email);

            if (emailExist != null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email alredy exist"
                    }
                });
            }

            var user = new IdentityUser()
            {
                Email = request.Email,
                UserName = request.Email
            };

            var isCreated = await _userManager.CreateAsync(user, request.Password);

            if (isCreated.Succeeded)
            {
                var token = GenerateToken(user);
                return Ok(new AuthResult()
                {
                    Result = true,
                    Token = token
                });

            }

            var errors = new List<string>();
            foreach (var err in isCreated.Errors)
            {
                errors.Add(err.Description);
            }

            return BadRequest(new AuthResult
            {
                Result = false,
                Errors = errors
            });
        }

        private string GenerateToken(IdentityUser user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new ClaimsIdentity(
                    new[]
                    {
                        new Claim("Id", user.Id),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                    })),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = JwtTokenHandler.CreateToken(tokenDescriptor);

            return JwtTokenHandler.WriteToken(token);

        }
    }

}

