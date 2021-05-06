using AutoMapper;
using BusinessAccessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessAccessLayer.HitService
{
    public class HitService : IHitService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public HitService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public string HitDragon(HitDTO tempHitDTO)
        {
            var tempHero = _dbContext.Heroes.ToList();
            var tempDragon = _dbContext.Dragons.ToList();
            var tempHit = _dbContext.Hits.ToList();
            int tempHeroGun = 0;
            
            foreach(var hero in tempHero)
            {
                if(hero.Id == tempHitDTO.HeroId)
                {
                    tempHeroGun = hero.Gun;
                    break;
                }
            }
            int resultHP = 0;
            foreach (var dragon in tempDragon)
            {
                if (dragon.Id == tempHitDTO.DragonId)
                {
                    if (tempHit.FirstOrDefault(i => i.DragonId == tempHitDTO.DragonId) != null)
                    {
                        int remnantDragon = dragon.HP;

                        foreach (var dragonHit in tempHit)
                        {
                            if (dragonHit.DragonId == dragon.Id)
                            {
                                if (remnantDragon >= dragonHit.ImpactForce)
                                {
                                    remnantDragon -= dragonHit.ImpactForce;
                                }
                                else
                                {
                                    remnantDragon = 0;
                                }
                            }
                        }
                        resultHP = remnantDragon;
                        break;
                    }
                    resultHP = dragon.HP;
                    break;
                }
            }
            if(resultHP > 0)
            {
                HitDTO hitDTO = new HitDTO
                {
                    ImpactForce = ResultImpactForces(tempHeroGun),
                    DragonId = tempHitDTO.DragonId,
                    HeroId = tempHitDTO.HeroId
                };
                var result = _mapper.Map<Hit>(hitDTO);
                _dbContext.Hits.Add(result);
                _dbContext.SaveChanges();
                return $"Hit -{ResultImpactForces(tempHeroGun)} XP";
            }
            return "The dragon is dead";
        }
        public int ResultImpactForces(int gun)
        {
            Random random = new Random();
            return gun += random.Next(1, 3);
        }
    }
}
