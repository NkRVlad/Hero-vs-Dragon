using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.Models
{
    public class ApiNameGenerated
    {
        [JsonProperty("name")]
        public string name { get; set; }
    }
}
