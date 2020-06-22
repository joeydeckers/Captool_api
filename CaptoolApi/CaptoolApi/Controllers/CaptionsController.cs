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
        private readonly ICaptionLogic _captionLogic;

        public CaptionsController(ICaptionRepos captionrepos, IAuthLogic authLogic, ICaptionLogic captionLogic)
        {
            _captionsRepos = captionrepos;
            _authLogic = authLogic;
            _captionLogic = captionLogic;
        }

        // GET: api/Captions/id
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CaptionFile>> GetCaptions(string id)
        {
            var user = await _authLogic.GetUserFromToken(HttpContext.User.Identity as ClaimsIdentity);
            if (user == null) return Unauthorized();

            CaptionFile caption = await _captionsRepos.getCaptionsAsync(id);

            if (caption == null)
                return NotFound();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot", "StaticFiles", $"{id}.vtt");

            caption.Caption = _captionLogic.createStaticFile(id, filePath, caption);


            return caption;
        }
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("[action]")]
        public async Task<ActionResult<CaptionFile>> PostCaption([FromBody]CaptionFile caption)
        {
            var user = await _authLogic.GetUserFromToken(HttpContext.User.Identity as ClaimsIdentity);
            if (user == null) return Unauthorized();

            return await _captionsRepos.addCaptionAsync(caption);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("[action]")]
        public async Task<ActionResult<CaptionFile>> UpdateCaption([FromBody]CaptionFile caption)
        {
            var user = await _authLogic.GetUserFromToken(HttpContext.User.Identity as ClaimsIdentity);
            if (user == null) return Unauthorized();

            await _captionsRepos.updateCaption(caption);

            return Redirect($"{Request.Host}/StaticFiles/{caption.VideoID}.vtt");
        }


        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteCaption(string id)
        {
            var user = await _authLogic.GetUserFromToken(HttpContext.User.Identity as ClaimsIdentity);
            if (user == null) return Unauthorized();

            await _captionsRepos.deleteCaption(id);
            return NoContent();
        }
    }
}