using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace BusinessAccessLayer.Models
{
    public class DragonDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public DateTime Date_Time { get; set; }
        public int Remnant { get; set; }
     
        public DragonDTO()
        {
            Random random = new Random();
            HP = random.Next(80, 100);
            
            ApiNameGenerated nameRandom = new ApiNameGenerated();
            string info = new WebClient().DownloadString("https://api.namefake.com/");
            nameRandom = JsonConvert.DeserializeObject<ApiNameGenerated>(info);
            Name = nameRandom.name;

            Date_Time = DateTime.Now;
        }
    }
}
