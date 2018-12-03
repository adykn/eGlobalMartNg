using System.Threading.Tasks;
using eGlobalMartNg.api.Data;
using eGlobalMartNg.api.Models;
using eGlobalMartNg.api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace eGlobalMartNg.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo,IConfiguration config )
        {
            _repo = repo;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistrationDto UsrDto)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            UsrDto.Username=UsrDto.Username.ToLower();
            
            if (await _repo.UserExist(UsrDto.Username)) return BadRequest("Username already Exist") ;

            var usertoCreate=new User{
                Username=UsrDto.Username ,
                FirstName= UsrDto.Firstname,
                LastName = UsrDto.Lastname           
            };
            var createdUser= await _repo.Register(usertoCreate,UsrDto.Password);
            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto mDto)
        {
            var m= await _repo.Login(mDto.Username.ToLower(),mDto.Password);
            if(m==null) return Unauthorized();

            var claims = new []{
                new Claim(ClaimTypes.NameIdentifier,m.Id.ToString()),
                new Claim(ClaimTypes.Name,m.Username)
            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor=new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler= new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new {token= tokenHandler.WriteToken(token)});
        }
    }
}