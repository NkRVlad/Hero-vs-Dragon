using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.Models
{
    public class HeroDTO
    {
        public int? Id { get; set; }
        public string Nickname { get; set; }
        public DateTime Date_Time { get; set; }
        public int Gun { get; set; }
        public ICollection<HitDTO> Hits { get; set; }
        public HeroDTO()
        {
            Random random = new Random();
            Gun = random.Next(1, 6);

            Date_Time = DateTime.Now;
        }
    }
}
