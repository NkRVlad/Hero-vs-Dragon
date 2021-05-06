using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Entity
{
    public class Dragon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public DateTime Date_Time { get; set; }
        public ICollection<Hit> Hits { get; set; }
    }
}
