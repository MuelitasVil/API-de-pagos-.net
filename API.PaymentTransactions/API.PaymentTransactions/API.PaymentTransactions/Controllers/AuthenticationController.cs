﻿
using API.PaymentTransactions.API.Configuration;
using API.PaymentTransactions.API.Cryptography;
using API.PaymentTransactions.Data;
using API.PaymentTransactions.Shared;
using API.PaymentTransactions.Shared.Auth;
using API.PaymentTransactions.Shared.NewFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace API.PaymentTransactions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTConfig _jwtConfig;
        private readonly IEmailSender _emailSender;
        private readonly APIPaymentTransactionsContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private Encrypt _encrypt = new Encrypt();

        public AuthenticationController(UserManager<IdentityUser> userManager,
            IOptions<JWTConfig> jwtConfig,
            IEmailSender emailSender,
            APIPaymentTransactionsContext context,
            TokenValidationParameters validationParameters
            )
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
            _emailSender = emailSender;
            _context = context;
            _tokenValidationParameters = validationParameters;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest();

            //Check if user exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser == null)
                return BadRequest(new AuthResult
                {
                    Errors = new List<string> { "Invalid Payload" },
                    Result = false
                });

            if (!existingUser.EmailConfirmed)
                return BadRequest(new AuthResult
                {
                    Errors = new List<string> { "Email needs to be confirmed." },
                    Result = false
                });

            var checkUserAndPass = await _userManager.CheckPasswordAsync(existingUser, request.Password);

            if (!checkUserAndPass)
                return BadRequest(
                    new AuthResult
                    {
                        Errors = new List<string> { "Invalid Credentials" },
                        Result = false
                    });

            AuthResult token = new AuthResult();
            token = await GenerateTokenAsync(existingUser);

            //Console.WriteLine("----------");
            //Console.WriteLine(token.RefreshToken);
            //Console.WriteLine(token.Token);
            //Console.WriteLine(token.Result);

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrarionUserDto request)
        {

            if (!ModelState.IsValid) { return BadRequest("Ingrese todos los datos ..."); }

            var emailExists = await _userManager.FindByEmailAsync(request.Email);

            if (emailExists != null)
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email already exists"
                    }
                });

            //Create user
            var user = new IdentityUser()
            {
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = false
            };

            var isCreated = await _userManager.CreateAsync(user, request.Password);

            if (isCreated.Succeeded && user.Email != null)
            {
                await SendVerificationEmail(user);

                return Ok(new AuthResult()
                {
                    Result = true
                });
            }
            else if (user.Email == null)
            {
                Console.WriteLine("Como es esto posible ????");
                Console.WriteLine(user.Email);
                return BadRequest("xd");
            }
            else
            {
                var errors = new List<string>();
                foreach (var err in isCreated.Errors)
                    errors.Add(err.Description);

                return BadRequest(new AuthResult
                {
                    Result = false,
                    Errors = errors
                });
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                new AuthResult
                {
                    Errors = new List<string> { "Invalid Parameters" },
                    Result = false
                });
            }

            var results = await VerifyAndGenerateTokenAsync(tokenRequest);

            if (results == null)
            {

                return BadRequest(new AuthResult
                {
                    Errors = new List<string> { "Invalid Token" }
                });
            }

            return Ok(results);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code) 
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                return BadRequest( new AuthResult
                {
                    Errors = new List<string>
                    {
                        "Invalid email confirmation url"
                    },
                    Result = false
                });
            }

            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                return NotFound($"Unable to load user with Id '{userId}'");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var result = await _userManager.ConfirmEmailAsync(user, code);
            var status = result.Succeeded ? "Thank for confirm your email" : "There's has been an error confirming your email";

            return Ok(status);
        }
        private async Task<AuthResult> GenerateTokenAsync(IdentityUser user)
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
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = JwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = JwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                Token = _encrypt.getSalt(23),
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResult
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                Result = true
            };
        }

        private async Task SendVerificationEmail(IdentityUser user)
        {
            var verificationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            verificationCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(verificationCode));

            //example: https://localhost:8080/api/authentication/verifyemail/userId=exampleuserId&code=examplecode
            var callbackUrl = $@"{Request.Scheme}://{Request.Host}{Url.Action("ConfirmEmail", controller: "Authentication",
                                    new { userId = user.Id, code = verificationCode })}";
            var emailBody = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>";
            await _emailSender.SendEmailAsync(user.Email, "Confirm your email, kiubo pa :V", emailBody);
        }

        private async Task<AuthResult> VerifyAndGenerateTokenAsync(TokenRequest tokenRequest)
        {

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                _tokenValidationParameters.ValidateLifetime = false;
                var tokenBeingVefiried = jwtTokenHandler.ValidateToken(tokenRequest.Token,_tokenValidationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if(!result || tokenBeingVefiried == null)
                    {
                        throw new Exception("Invalid Token");
                    }
                }

                var utcExpiryDate = long.Parse(tokenBeingVefiried.Claims.FirstOrDefault(
                                                c => c.Type == JwtRegisteredClaimNames.Exp).Value);

                var expityDate = DateTimeOffset.FromUnixTimeSeconds(utcExpiryDate).UtcDateTime;

                if(expityDate < DateTime.UtcNow)
                {
                    throw new Exception("Token Expired");
                }

                Console.WriteLine("asdasdasd");

                var storedToken = await _context.RefreshTokens.
                        FirstOrDefaultAsync(t => t.Token == tokenRequest.RefreshToken);

                Console.WriteLine(storedToken.Token);
                Console.WriteLine("Wtf khe esta pasando :V");

                if (storedToken is null)
                {
                    throw new Exception("Invalid Token");
                }

                if (storedToken.IsUsed  || storedToken.IsRevoked )
                {
                    throw new Exception("Invalid Token");
                }

                var jti = tokenBeingVefiried.Claims.FirstOrDefault(
                    c => c.Type == JwtRegisteredClaimNames.Jti).Value;

                if(jti != storedToken.JwtId)
                {
                    throw new Exception("Invalid Token");
                }

                if(storedToken.ExpiryDate < DateTime.UtcNow)
                {
                    throw new Exception("Token Expired");
                }

                storedToken.IsUsed = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();

                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

                return await GenerateTokenAsync(dbUser);

            } catch (Exception e)
            {
                var message = e.Message == "Invalid Token" || e.Message == "Token Expired"
                    ? e.Message
                    : "Internal Server Error";

                return new AuthResult() {
                    Result = false, 
                    Errors = new List<string> { message}
                    };
            } 

        }
    }
  }

