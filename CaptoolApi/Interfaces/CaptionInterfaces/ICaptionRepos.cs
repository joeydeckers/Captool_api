using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.CaptionInterfaces
{
    public interface ICaptionRepos
    {
        Task<CaptionFile> getCaptionsAsync(string id);
        Task<CaptionFile> addCaptionAsync(CaptionFile captions);
        Task<CaptionFile> updateCaption(CaptionFile captions);
        Task deleteCaption(string id);

    }
}
