﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelLayer.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }

        public string Token { get; set; }



        public User()
        {

        }
    }
}
