using Interfaces.CaptionInterfaces;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic.Logic
{
    public class CaptionLogic : ICaptionLogic
    {
        public void createStaticFile(string id, string filePath, CaptionFile caption)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            string text = $"WEBVTT - This file has cues.\n\n{caption.Caption}";

            using (FileStream fs = System.IO.File.Create(filePath))
            {
                // Add some text to file    
                Byte[] data = new UTF8Encoding(true).GetBytes(text);
                fs.Write(data, 0, text.Length);
            }
        }
    }
}
