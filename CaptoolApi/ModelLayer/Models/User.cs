using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelLayer.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public string Playlist { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string Token { get; set; }
    }
}
