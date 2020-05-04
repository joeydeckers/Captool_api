﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Interfaces.CaptionInterfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    public class CaptionsController : ControllerBase 
    { 
        private readonly ICaptionRepos _captionsRepos;

        public CaptionsController(ICaptionRepos userRepos)
        {
            _captionsRepos = userRepos;
        }

        // GET: api/Captions/id
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetCaptions(string id)
        {
            CaptionFile caption = await _captionsRepos.getCaptionsAsync(id);
            string text = caption.Text;
            
            if (caption == null) return NotFound();

            return text;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CaptionFile>> PostUser([FromBody]CaptionFile caption)
        { 
            return await _captionsRepos.addCaptionAsync(caption);
        }
    }
}