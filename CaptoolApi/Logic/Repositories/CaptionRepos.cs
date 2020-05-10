using Data;
using Interfaces.CaptionInterfaces;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public class CaptionRepos : ICaptionRepos
    {
        private readonly AppDbContext _context;

        public CaptionRepos(AppDbContext context)
        {
            _context = context;

        }

        public async Task<CaptionFile> addCaptionAsync(CaptionFile captions)
        {
            await _context.ct_captions.AddAsync(captions);
            await _context.SaveChangesAsync();
            return captions;
        }

        public async Task<CaptionFile> getCaptionsAsync(string id)
        {
            CaptionFile caption = await _context.ct_captions.FindAsync(id);
            var text = caption.Data;

            return new CaptionFile() { VideoID = id, Data = text };
        }
    }
}
