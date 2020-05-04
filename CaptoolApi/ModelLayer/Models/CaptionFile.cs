using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Models
{
    public class CaptionFile
    {
        [Key]
        public string VideoId { get; set; }
        public string Text { get; set; }

        public CaptionFile()
        {

        }
    }
}