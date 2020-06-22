using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.CaptionInterfaces
{
    public interface ICaptionLogic
    {
        string createStaticFile(string id, string filePath, CaptionFile caption);
    }
}
