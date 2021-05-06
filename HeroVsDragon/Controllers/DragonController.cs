using BusinessAccessLayer.DragonService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroVsDragon.Models;
using Microsoft.AspNetCore.Authorization;
namespace HeroVsDragon.Controllers
{
    [Route("api/dragon")]
    [ApiController]
    [Authorize]
    public class DragonController : ControllerBase
    {
        private readonly IDragonService _dragonService;
        public DragonController(IDragonService dragonService)
        {
            _dragonService = dragonService;
        }
        [HttpPost]
        [Route("create")]
        public IActionResult CreateDragon(QuantityDragon quantityDragon)
        {
            _dragonService.CreateDaragon(quantityDragon.quantityDragon);
            return Ok();
        }

        [HttpGet]
        [Route("get-dragon")]
        public IActionResult GetDragon(int? page, int pagesize = 30)
        {
            var result = _dragonService.GetDragon(page, pagesize);
            return Ok(result);
        }
         [HttpGet]
        [Route("get-search-id")]
        public IActionResult GetDragonSearchId(int? page, string idDragon, int pagesize = 30)
        {
            var result = _dragonService.GetSearchId(page, idDragon, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("get-dragon-sort-asc")]
        public IActionResult GetDragonSortAsc(int? page, string paramsOrder, int pagesize = 30)
        {
            var result = _dragonService.GetDragonSort(page, paramsOrder = "ASC", pagesize);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-dragon-sort-desc")]
        public IActionResult GetDragonSortDesc(int? page, string paramsOrder, int pagesize = 30)
        {
            var result = _dragonService.GetDragonSort(page, paramsOrder = "Desc", pagesize);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-search-name")]
        public IActionResult GetDragonSearchName(int? page, string textSearch, string paramsFilter, int pagesize = 30)
        {
            var result = _dragonService.SearchName(page, textSearch, paramsFilter, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("get-search-hp")]
        public IActionResult GetDragonSearchHP(int? page, string hpSearch, string paramsFilter, int pagesize = 30)
        {
            var result = _dragonService.SearchHP(page, hpSearch, paramsFilter, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("get-search-remnant")]
        public IActionResult GetDragonSearchRemnant(int? page, string textSearch, string paramsFilter, int pagesize = 30)
        {
            var result = _dragonService.SearchRemnant(page, textSearch, paramsFilter, pagesize);
            return Ok(result);
        }
    }
}
