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

        public async Task deleteCaption(int id)
        {
            var caption = await _context.ct_captions.FindAsync(id);
            if (caption != null)
            {
                _context.ct_captions.Remove(caption);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CaptionFile>> getCaptionsAsync(string videoid)
        {
            
            List<CaptionFile> captions = _context.ct_captions
                .Where(v => v.VideoID == videoid).ToList();

            if (captions != null)
            {
                return captions;
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
