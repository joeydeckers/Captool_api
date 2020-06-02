using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Interfaces.CaptionInterfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net;
using Helper;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    public class CaptionsController : ControllerBase 
    { 
        private readonly ICaptionRepos _captionsRepos;
        private readonly IAuthLogic _authLogic;
        private readonly IWebHostEnvironment _webRoot;

        public CaptionsController(ICaptionRepos captionrepos, IAuthLogic authLogic, IWebHostEnvironment webroot)
        {
            _captionsRepos = captionrepos;
            _authLogic = authLogic;
            _webRoot = webroot;
        }

        // GET: api/Captions/id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCaptions(string id)
        {
            var user = await _authLogic.GetUserFromToken(HttpContext.User.Identity as ClaimsIdentity);
            if (user == null) return Unauthorized();

            CaptionFile caption = await _captionsRepos.getCaptionsAsync(id);

            var contentType = "text/plain";
            var fileName = $"/captions.txt";

            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            using (FileStream fs = System.IO.File.Create(fileName))
            {
                // Add some text to file    
                Byte[] data =  new UTF8Encoding(true).GetBytes(caption.Data);
                fs.Write(data, 0, caption.Data.Length);
            }

            return File(fileName, contentType, $"{caption.VideoID}.vtt");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CaptionFile>> PostCaption([FromBody]CaptionFile caption)
        { 
            return await _captionsRepos.addCaptionAsync(caption);
        }
    }
}