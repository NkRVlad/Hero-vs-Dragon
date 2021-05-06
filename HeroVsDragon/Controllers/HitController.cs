using BusinessAccessLayer.DragonService;
using BusinessAccessLayer.HitService;
using BusinessAccessLayer.Models;
using HeroVsDragon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace HeroVsDragon.Controllers
{
    [Route("api/hit")]
    [ApiController]
    [Authorize]
    public class HitController : ControllerBase
    {
        private readonly IDragonService _dragonService;
        private readonly IHitService _hitService;
        public HitController(IDragonService dragonService, IHitService hitService)
        {
            _dragonService = dragonService;
            _hitService = hitService;
        }
        [HttpPost]
        [Route("hit-dragon")]
        public IActionResult CreateDragon(HitDragon hitDragon)
        {
            HitDTO hitDTO = new HitDTO();

            if (int.Parse(hitDragon.idDragon) >= 0 && int.Parse(hitDragon.idHero) >= 0)
            {
                hitDTO.HeroId = int.Parse(hitDragon.idHero);
                hitDTO.DragonId = int.Parse(hitDragon.idDragon);
                var result = _hitService.HitDragon(hitDTO);
                return Ok(new { Message = result });
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
