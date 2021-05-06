using BusinessAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.HeroService
{
    public interface IHeroService
    {
        List<HeroDTO> GetAllHero();
        PageResult<HeroDTO> GetHero(int? page, int pagesize = 30);
        PageResult<HeroDTO> GetHeroSort(int? page, string paramsSort, int pagesize = 30);
        PageResult<HeroDTO> SearchHeroName(int? page, string textSearch, string paramsFilter, int pagesize = 30);
        PageResult<HeroDTO> SearchHeroTime(int? page, DateTime timeSearch, string paramsFilter, int pagesize = 30);
        PageResult<DragonDamageDTO> GetDragonDamage(int? page, int IdHero, int pagesize = 30);
        PageResult<DragonDamageDTO> GetDragonNameSort(int? page, int IdHero, string paramsSort, int pagesize = 30);
        PageResult<DragonDamageDTO> GetDragonDamageSort(int? page, int IdHero, string paramsSort, int pagesize = 30);
        public bool CheckNickname(HeroDTO hero);
        public int CreateHero(HeroDTO hero);
    }
}
