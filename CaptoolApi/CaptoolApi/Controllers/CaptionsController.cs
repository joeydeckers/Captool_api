using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    public class CaptionsController : ControllerBase 
    { 
        private readonly ICaptionRepos _captionsRepos;
        private readonly IAuthLogic _authLogic;

        public CaptionsController(ICaptionRepos captionrepos, IAuthLogic authLogic)
        {
            _captionsRepos = captionrepos;
            _authLogic = authLogic;
        }

        // GET: api/Captions/id
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetCaptions(string id)
        {
            var user = await _authLogic.GetUserFromToken(HttpContext.User.Identity as ClaimsIdentity);
            if (user == null) return Unauthorized();

            CaptionFile caption = await _captionsRepos.getCaptionsAsync(id);
            
            var contentType = "text/vtt";
            var fileName = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot", "StaticFiles", $"{caption.VideoID}.vtt");


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

            return caption.Data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CaptionFile>> PostCaption([FromBody]CaptionFile caption)
        { 
            return await _captionsRepos.addCaptionAsync(caption);
        }
    }
}