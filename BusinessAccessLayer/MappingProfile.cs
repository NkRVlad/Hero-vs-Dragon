using AutoMapper;
using BusinessAccessLayer.Models;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Hero, HeroDTO>();
            CreateMap<HeroDTO, Hero>();

            CreateMap<Dragon, DragonDTO>();
            CreateMap<DragonDTO, Dragon >();
            
            CreateMap<Hit, HitDTO>();
            CreateMap<HitDTO, Hit>();
        }
        
    }
}
