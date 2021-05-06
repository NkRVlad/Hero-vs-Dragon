using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Entity
{
    public class Hit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ImpactForce { get; set; }
        public DateTime ImpactTime { get; set; }
        public int HeroId { get; set; }
        public Hero Heros { get; set; }
        public int DragonId { get; set; }
        public Dragon Dragons { get; set; }
    }
}
