using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelLayer.Models
{
    public class CaptionFile
    {
        [Key]
        public int? Id { get; set; }
        public string VideoID { get; set; }
        public string Caption { get; set; }

        public CaptionFile()
        {

        }
    }
}