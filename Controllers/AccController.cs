using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Njal_back.DTOS;
using Njal_back.Interfaces;
using Njal_back.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Njal_back.Controllers
{
    public class AccController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration cfg;

        public AccController(IUnitOfWork uow,IConfiguration cfg )
        {
            this.uow = uow;
            this.cfg = cfg;
        }

        // api/Acc/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate
                (loginReq.Username, loginReq.Password);
            if(user == null)
            {
                return Unauthorized();
            }

            var loginRes = new LoginResDto();
            loginRes.Username = user.Username;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }

        //api/Acc/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if (await uow.UserRepository.UserAlreadyExist(loginReq.Username))
                return BadRequest("User already exists");
            uow.UserRepository.Register(loginReq.Username, loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        
           

            // symetric encryption
            private string CreateJWT(User user)
        {
            var secretKey = cfg.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF32
              .GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(0.5),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
