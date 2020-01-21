using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using olx.api.Data ;
using Microsoft.EntityFrameworkCore ;
using olx.api.Models;
using olx.api.Dto ;
using Microsoft.Extensions.Configuration;
using System.Security.Claims ;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace olx.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _repo ;
        private readonly ILogger<AuthController> _logger;

        public AuthController ( IAuthRepository repo , IConfiguration config, ILogger<AuthController> logger) 
        {
            _config = config ;
            _repo = repo ;
            _logger = logger ;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ( [FromBody] UserForRegistrationDto userForRegistration  ) 
        {
            if ( !ModelState.IsValid)
            {
                return BadRequest(ModelState) ;
            }

            userForRegistration.Username = userForRegistration.Username.ToLower() ;
            if ( await _repo.UserExists(userForRegistration.Username) ) 
            {
                return BadRequest("Username already Exists ") ;
            }

            var userToCreate = new User 
            {
                Username = userForRegistration.Username
            } ;

            var createdUser = await _repo.Register(userToCreate, userForRegistration.Password) ;

            return StatusCode(201) ; 

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login ( UserForLoginDto userForLoginDto )
        {
            // _logger.LogInformation(userForLoginDto.ToString()) ;
            // throw new Exception("Computer says no") ;

            var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password) ;

            //_logger.LogInformation(userFromRepo.ToString()) ;

            if ( userFromRepo == null )
            {
                return Unauthorized() ;
            }

            // var claims = new[]
            // {
            //     new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
            //     new Claim(ClaimTypes.Name, userFromRepo.Username)
            // } ;

            // var key = new SymmetricSecurityKey(Encoding.UTF8
            //     .GetBytes(_config.GetSection("AppSettings:Token").Value)) ;

            // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature) ;

            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(claims),
            //     Expires = DateTime.Now.AddDays(1),
            //     SigningCredentials = creds 
            // } ;

            // var tokenHandler = new JwtSecurityTokenHandler() ;

            // var token = tokenHandler.CreateToken(tokenDescriptor) ;

            // return Ok(new {token = tokenHandler.WriteToken(token)}) ;
            return Ok(userFromRepo ) ;
         }
    }
}