using AutoMapper;
using BusinessAccessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessAccessLayer.HeroService
{
    public class HeroService : IHeroService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public HeroService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<HeroDTO> GetAllHero()
        {
            var tempHero = _dbContext.Heroes.ToList();
            var resutlListHero = _mapper.Map<List<HeroDTO>>(tempHero);
            return resutlListHero;
        }
        public int CreateHero(HeroDTO hero)
        {
            var tempHero = _mapper.Map<Hero>(hero);
            _dbContext.Heroes.Add(tempHero);
            _dbContext.SaveChanges();
            return tempHero.Id;
        }

        public bool CheckNickname(HeroDTO hero)
        {
            var tempHeroList = GetAllHero();
            
            foreach(var tempHero in tempHeroList)
            {
                if(tempHero.Nickname == hero.Nickname)
                {
                    return true;
                }
            }
            return false;
        }

        public PageResult<HeroDTO> GetHero(int? page, int pagesize)
        {
            var tempHero = _dbContext.Heroes.ToList();
           
            var resutlListHero = _mapper.Map<List<HeroDTO>>(tempHero);

            var allItemCount = _dbContext.Heroes.Count();
            
            var result = new PageResult<HeroDTO>
            {
                Count = allItemCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = resutlListHero.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };

            return result;

        }

        public PageResult<HeroDTO> GetHeroSort(int? page, string paramsSort, int pagesize)
        {
            if (paramsSort == "ASC")
            {
                var tempHero = _dbContext.Heroes.OrderBy(n => n.Nickname).ToList();
                var resutlListHero = _mapper.Map<List<HeroDTO>>(tempHero);
                var allItemCount = tempHero.Count();

                var result = new PageResult<HeroDTO>
                {
                    Count = allItemCount,
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resutlListHero.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                };

                return result;
            }
            else
            {
                var tempHero = _dbContext.Heroes.OrderByDescending(n => n.Nickname).ToList();
                var resutlListHero = _mapper.Map<List<HeroDTO>>(tempHero);
                var allItemCount = tempHero.Count();

                var result = new PageResult<HeroDTO>
                {
                    Count = allItemCount,
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resutlListHero.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                };

                return result;
            }
            
        }

        public PageResult<HeroDTO> SearchHeroName(int? page, string textSearch, string paramsFilter, int pagesize)
        {
            if (paramsFilter != null && textSearch != null)
            {
                if (int.TryParse(paramsFilter, out int filter))
                {
                    var tempHero = _dbContext.Heroes
                        .Where(n => n.Nickname.Length >= filter && n.Nickname.StartsWith(textSearch))
                        .ToList();
                    
                    var resutlListHero = _mapper.Map<List<HeroDTO>>(tempHero);
                    var allItemCount = tempHero.Count();

                    var result = new PageResult<HeroDTO>
                    {
                        Count = allItemCount,
                        PageIndex = page ?? 1,
                        PageSize = pagesize,
                        Items = resutlListHero.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                    };
                    return result;
                }
                else
                {
                    return GetHero(page, pagesize);
                }
            }
            else
            {
                var tempHero = _dbContext.Heroes
                        .Where(n => n.Nickname.StartsWith(textSearch))
                        .ToList();
                
                var resutlListHero = _mapper.Map<List<HeroDTO>>(tempHero);
                var allItemCount = tempHero.Count();

                var result = new PageResult<HeroDTO>
                {
                    Count = allItemCount,
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resutlListHero.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                };
                return result;
            }
        }

        public PageResult<HeroDTO> SearchHeroTime(int? page, DateTime timeSearch, string paramsFilter, int pagesize)
        {
            if (paramsFilter != null && timeSearch != null)
            {
                if (paramsFilter == "more")
                {
                    var tempHero = _dbContext.Heroes
                        .Where(t => t.Date_Time.TimeOfDay > timeSearch.TimeOfDay)
                        .ToList();
                    var resutlListHero = _mapper.Map<List<HeroDTO>>(tempHero);
                    var allItemCount = tempHero.Count();

                    var result = new PageResult<HeroDTO>
                    {
                        Count = allItemCount,
                        PageIndex = page ?? 1,
                        PageSize = pagesize,
                        Items = resutlListHero.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                    };
                    return result;
                }
                if(paramsFilter == "less")
                {
                     var tempHero = _dbContext.Heroes
                        .Where(t => t.Date_Time.TimeOfDay < timeSearch.TimeOfDay)
                        .ToList();
                    var resutlListHero = _mapper.Map<List<HeroDTO>>(tempHero);
                    var allItemCount = tempHero.Count();

                    var result = new PageResult<HeroDTO>
                    {
                        Count = allItemCount,
                        PageIndex = page ?? 1,
                        PageSize = pagesize,
                        Items = resutlListHero.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                    };
                    return result;
                }
                else
                {
                    return GetHero(page, pagesize);
                }
            }
            else
            {
                return GetHero(page, pagesize);
            }
        }

        public PageResult<DragonDamageDTO> GetDragonDamage(int? page, int IdHero, int pagesize = 30)
        {
        List<DragonDamageDTO> tempList = new List<DragonDamageDTO>();

            var list =   from H in _dbContext.Hits
                         join D in _dbContext.Dragons on H.DragonId equals D.Id
                         where H.HeroId == IdHero
                         group H by H.Dragons.Name into d
                         select new DragonDamageDTO()
                         {
                             NameDragon = d.Key.ToString(),
                             Damage = d.Where(t => t.ImpactForce > 0).Sum(s => s.ImpactForce).ToString()
                         }; 

            foreach(var resultLinq in list)
            { 
               DragonDamageDTO damageDTO = new DragonDamageDTO();
               damageDTO.NameDragon = resultLinq.NameDragon;
               damageDTO.Damage = resultLinq.Damage;
               tempList.Add(damageDTO);
            }

            var result = new PageResult<DragonDamageDTO>
            {
                Count = tempList.Count(),
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };
            return result;
        }
        public PageResult<DragonDamageDTO> GetDragonNameSort(int? page, int IdHero, string paramsSort, int pagesize = 30)
        {
            List<DragonDamageDTO> tempList = new List<DragonDamageDTO>();

            var list =   from H in _dbContext.Hits
                         join D in _dbContext.Dragons on H.DragonId equals D.Id
                         where H.HeroId == IdHero
                         group H by H.Dragons.Name into d
                         select new DragonDamageDTO()
                         {
                             NameDragon = d.Key.ToString(),
                             Damage = d.Where(t => t.ImpactForce > 0).Sum(s => s.ImpactForce).ToString()
                         }; 

            foreach(var resultLinq in list)
            { 
               DragonDamageDTO damageDTO = new DragonDamageDTO();
               damageDTO.NameDragon = resultLinq.NameDragon;
               damageDTO.Damage = resultLinq.Damage;
               tempList.Add(damageDTO);
            }
            if (paramsSort == "ASC")
            {
                var result = new PageResult<DragonDamageDTO>
                {
                    Count = tempList.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).OrderBy(n =>n.NameDragon).ToList()
                    };
               
                return result;
            }
            else
            {
               var result = new PageResult<DragonDamageDTO>
                {
                    Count = tempList.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).OrderByDescending(n =>n.NameDragon).ToList()
                    };
               
                return result;
            }
        }
        public PageResult<DragonDamageDTO> GetDragonDamageSort(int? page, int IdHero, string paramsSort, int pagesize = 30)
        {
            List<DragonDamageDTO> tempList = new List<DragonDamageDTO>();

            var list =   from H in _dbContext.Hits
                         join D in _dbContext.Dragons on H.DragonId equals D.Id
                         where H.HeroId == IdHero
                         group H by H.Dragons.Name into d
                         select new DragonDamageDTO()
                         {
                             NameDragon = d.Key.ToString(),
                             Damage = d.Where(t => t.ImpactForce > 0).Sum(s => s.ImpactForce).ToString()
                         }; 

            foreach(var resultLinq in list)
            { 
               DragonDamageDTO damageDTO = new DragonDamageDTO();
               damageDTO.NameDragon = resultLinq.NameDragon;
               damageDTO.Damage = resultLinq.Damage;
               tempList.Add(damageDTO);
            }
            if (paramsSort == "ASC")
            {
                var result = new PageResult<DragonDamageDTO>
                {
                    Count = tempList.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).OrderBy(d => d.Damage).ToList()
                    };
               
                return result;
            }
            else
            {
               var result = new PageResult<DragonDamageDTO>
                {
                    Count = tempList.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).OrderByDescending(d => d.Damage).ToList()
                    };
               
                return result;
            }
        }
    }
}
