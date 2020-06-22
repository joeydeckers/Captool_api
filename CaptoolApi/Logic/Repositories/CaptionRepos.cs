using Data;
using Interfaces.CaptionInterfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task deleteCaption(string id)
        {
            List<CaptionFile> captions = _context.ct_captions
                                                          .Where(c => c.VideoID == id)
                                                          .ToList();
            foreach(var caption in captions)
                _context.ct_captions.Remove(caption);
                await _context.SaveChangesAsync();
            
        }

        public async Task<CaptionFile> getCaptionsAsync(string videoid)
        {
            CaptionFile caption = null;
            List<CaptionFile> captions = await _context.ct_captions.ToListAsync();

            if (captions != null)
            {
                caption = captions.Last(v => v.VideoID == videoid);
            }

            if (caption != null)
            {
                return caption;
            }

            else return null;
        }

        public async Task<CaptionFile> updateCaption(CaptionFile newcaption)
        {
            var caption = _context.Set<CaptionFile>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(newcaption.Id));


            if (caption != null)
            {
                _context.Entry(caption).State = EntityState.Detached;
            }

            _context.Entry(newcaption).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return newcaption;
        }
    }
}
