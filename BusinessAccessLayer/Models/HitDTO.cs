using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.Models
{
    public class HitDTO
    {
        public int? Id { get; set; }
        public int ImpactForce { get; set; }
        public DateTime ImpactTime { get; set; }
        public int HeroId { get; set; }
        public HeroDTO Heros { get; set; }
        public int DragonId { get; set; }
        public DragonDTO Dragons { get; set; }

        public HitDTO()
        {
            ImpactTime = DateTime.Now;
        }
    }
}
