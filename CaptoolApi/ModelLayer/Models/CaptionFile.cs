using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Models
{
    public class CaptionFile
    {
        [Key]
        public string VideoID { get; set; }
        public string Data { get; set; }

        public CaptionFile()
        {

        }
    }
}