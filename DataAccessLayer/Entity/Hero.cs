using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Entity
{
    public class Hero
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public DateTime Date_Time { get; set; }
        public int Gun { get; set; }
        public ICollection<Hit> Hits { get; set; }

    }
}
