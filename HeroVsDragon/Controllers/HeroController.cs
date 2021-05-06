using BusinessAccessLayer.HeroService;
using BusinessAccessLayer.Models;
using HeroVsDragon.Settings;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/hero")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;
        private readonly CustomSettings _settings;
        public HeroController(IHeroService heroService, IConfiguration configuration)
        {
            _heroService = heroService;
            _settings = new CustomSettings();
            configuration.Bind(_settings);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(HeroDTO hero)
        {
            if (hero != null)
            {
                hero.Nickname = hero.Nickname.Trim(); 
                bool flag = true;
                foreach(var charSrt in hero.Nickname)
                {
                    if(char.IsDigit(charSrt))
                    {
                        flag = false;
                        break;
                    }
                    if(charSrt >= '0' && charSrt <= '9')
                    {
                        flag = false;
                        break;
                    }
                }
                if(flag)
                {
                    if(hero.Nickname.Length >= 4 && hero.Nickname.Length <= 20)
                    {
                        
                        bool resultCheckNickname = _heroService.CheckNickname(hero);
                        
                        if (!resultCheckNickname)
                        {
                            int IdUser = _heroService.CreateHero(hero);
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
                            return BadRequest(new { Message = "A user with this nickname already exists" });
                        }

                    } return BadRequest(new { Message = "Incorrect data entry !" });

                } return BadRequest(new { Message = "Incorrect data entry !" });
            }
            else
            {
                return BadRequest(new { Message = "Еmpty nickname !" });
            }
            
        }
        [Authorize]
        [HttpGet]
        [Route("get-hero")]
        public IActionResult GetHero(int? page, int pagesize = 30)
        {
            var result = _heroService.GetHero(page, pagesize);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get-hero-sort-asc")]

        public IActionResult GetHeroSortAsc(int? page, string paramsOrder, int pagesize = 30)
        {
            var result = _heroService.GetHeroSort(page, paramsOrder = "ASC" ,pagesize);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get-hero-sort-desc")]
        public IActionResult GetHeroSortDesc(int? page, string paramsOrder, int pagesize = 30)
        {
            var result = _heroService.GetHeroSort(page, paramsOrder = "Desc", pagesize);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get-search-nickname")]
        public IActionResult GetHeroSearchNickname(int? page, string textSearch, string paramsFilter, int pagesize = 30)
        {
            var result = _heroService.SearchHeroName(page, textSearch, paramsFilter, pagesize);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get-search-time")]
        public IActionResult GetHeroSearchTime(int? page, DateTime timeSearch, string paramsFilter, int pagesize = 30)
        {
            var result = _heroService.SearchHeroTime(page, timeSearch, paramsFilter, pagesize);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get-hit-damage-dragon")]
        public IActionResult GetHeroDragonDamage(int? page, string IdHero, int pagesize = 30)
        {
            if (int.TryParse(IdHero, out int id))
            {
                var result = _heroService.GetDragonDamage(page, id, pagesize);
                return Ok(result);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet]
        [Route("get-name-dragon-sort-asc")]
         public IActionResult GetDragonNameSortAsc(int? page,string IdHero, string paramsOrder, int pagesize = 30)
        {
            if (int.TryParse(IdHero, out int id))
            {
                var result = _heroService.GetDragonNameSort(page, id, paramsOrder = "ASC" ,pagesize);
                return Ok(result);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet]
        [Route("get-name-dragon-sort-desc")]
         public IActionResult GetDragonNameSortDesc(int? page,string IdHero, string paramsOrder, int pagesize = 30)
        {
            if (int.TryParse(IdHero, out int id))
            {
                var result = _heroService.GetDragonNameSort(page, id, paramsOrder = "DESC" ,pagesize);
                return Ok(result);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet]
        [Route("get-damage-dragon-sort-asc")]
         public IActionResult GetDragonDamageSortAsc(int? page,string IdHero, string paramsOrder, int pagesize = 30)
        {
            if (int.TryParse(IdHero, out int id))
            {
                var result = _heroService.GetDragonDamageSort(page, id, paramsOrder = "ASC" ,pagesize);
                return Ok(result);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet]
        [Route("get-damage-dragon-sort-desc")]
         public IActionResult GetDragonDamageSortDesc(int? page,string IdHero, string paramsOrder, int pagesize = 30)
        {
            if (int.TryParse(IdHero, out int id))
            {
                var result = _heroService.GetDragonDamageSort(page, id, paramsOrder = "DESC" ,pagesize);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
