using BusinessAccessLayer.HeroService;
using HeroVsDragon.Models;
using HeroVsDragon.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HeroVsDragon.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthenticationController : ControllerBase
    {
      
        private readonly CustomSettings _settings;
        private readonly IHeroService _heroService;
        public AuthenticationController(IHeroService heroService, IConfiguration configuration)
        {
            _heroService = heroService;
            _settings = new CustomSettings();
            configuration.Bind(_settings);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login nickname)
        {
            if (nickname == null)
            {
                return BadRequest(new { Message = "Invalid data" });
            }
            var hero = _heroService.GetAllHero();
            
            bool flagStatusOk = false;
            int? IdUser = -1;      
            foreach (var tempHero in hero)
            {
                if (nickname.Nickname == tempHero.Nickname)
                {
                    IdUser = tempHero.Id;
                    flagStatusOk = true;
                    break;
                }
            }

            if (flagStatusOk)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWTSettings.SecretKey));

                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOption = new JwtSecurityToken(

                    issuer: _settings.JWTSettings.Host,
                    audience: _settings.JWTSettings.Host,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

                return Ok(new { Token = tokenString, IdUser = IdUser });
            }
            else
            {
                return BadRequest(new { Message = "There is no such user" });
            }
        }
    }
}
